using System.Collections.ObjectModel;
using Banks.Accounts;
using Banks.Commands;
using Banks.Exceptions;
using Banks.Observer;

namespace Banks
{
    public class Bank : IBankObservable, IBankObserver
    {
        private readonly string name;
        private readonly CentralBank centralBank;

        private Dictionary<Client, ClientInfo> clients;
        private Dictionary<Client, List<ITransaction>> transactions;

        private AccountManager accountManager;
        private TransactionManager transactionManager;

        private Builder.Builder builder;
        private Dictionary<AccountType, List<IClientObserver>> clientObservers;

        public Bank(string name, int fixedPercentage, IDepositCalc depositCalc, int creditLimit, int fees, CentralBank centralBank, Builder.Builder builder)
        {
            this.name = name;
            this.centralBank = centralBank;
            this.FixedPercentage = fixedPercentage;
            this.CreditLimit = creditLimit;
            this.DepositCalc = depositCalc;
            this.Fees = fees;

            this.builder = builder;

            clients = new Dictionary<Client, ClientInfo>();
            clientObservers = new Dictionary<AccountType, List<IClientObserver>>();
            clientObservers.Add(AccountType.Credit, new List<IClientObserver>());
            clientObservers.Add(AccountType.Debit, new List<IClientObserver>());
            clientObservers.Add(AccountType.Deposit, new List<IClientObserver>());

            accountManager = new AccountManager();
            accountManager.AddAccountFactory(AccountType.Credit, new CreditAccountFactory(this));
            accountManager.AddAccountFactory(AccountType.Debit, new DebitAccountFactory(this));
            accountManager.AddAccountFactory(AccountType.Deposit, new DepositAccountFactory(this));

            transactionManager = new TransactionManager();

            transactions = new Dictionary<Client, List<ITransaction>>();
        }

        public string Name { get { return name; } }
        public int FixedPercentage { get; private set; }
        public IDepositCalc DepositCalc { get; private set; }
        public int CreditLimit { get; private set; }
        public int Fees { get; private set; }

        public void SetNewFixedPercentage(int fixedPercentage)
        {
            this.FixedPercentage = fixedPercentage;

            NotifyObservers(AccountType.Debit, "Новый фиксированный процент " + fixedPercentage);
        }

        public void SetNewDepositCalc(IDepositCalc depositCalc)
        {
            this.DepositCalc = depositCalc;

            NotifyObservers(AccountType.Deposit, "Новый процент для открытия счета");
        }

        public void SetCreditLimit(int creditLimit)
        {
            this.CreditLimit = creditLimit;

            NotifyObservers(AccountType.Credit, "Новый лимит " + creditLimit);
        }

        public void SetNewFees(int fees)
        {
            this.Fees = fees;

            NotifyObservers(AccountType.Credit, "New Fees");
        }

        public void AddClientInfo(Client client)
        {
            builder.BuildFromClient(client);
            if (string.IsNullOrEmpty(client.Address))
            {
                builder.BuildAddress();
            }

            if (string.IsNullOrEmpty(client.Passport))
            {
                builder.BuildPassport();
            }
        }

        public Client AddNewClientTest(Client client)
        {
            clients.Add(client, new ClientInfo
            {
                Registered = DateTime.Now,
            });

            transactions.Add(client, new List<ITransaction>());

            return client;
        }

        public Client AddNewClient()
        {
            builder.BuildEmptyClient();
            builder.BuildFullName();
            builder.BuildAddress();
            builder.BuildPassport();

            Client client = builder.GetClient();

            clients.Add(client, new ClientInfo
            {
                Registered = DateTime.Now,
            });

            transactions.Add(client, new List<ITransaction>());

            return client;
        }

        public void OpenNewCreditAccount(Client client, double money, DateTime openDate)
            => CreateNewAccount(AccountType.Credit, client, money, openDate);

        public void OpenNewDebitAccount(Client client, double money, DateTime openDate)
            => CreateNewAccount(AccountType.Debit, client, money, openDate);

        public void OpenNewDepositAccount(Client client, double money, DateTime openDate)
            => CreateNewAccount(AccountType.Deposit, client, money, openDate);

        public void CalcPercentageAndFees(DateTime dateTime)
        {
            foreach (var client in clients)
            {
                if (client.Key.IsValid())
                {
                    client.Value.Account.CalcBankShowOperation(dateTime);
                }
                else
                {
                    throw new ClientNotValid();
                }
            }
        }

        public void Withdraw(Client client, double money, DateTime dateTime)
        {
            if (client.IsValid())
            {
                if (!clients.ContainsKey(client))
                    throw new NotFoundClient();

                Account account = clients[client].Account;
                ITransaction transaction = transactionManager.GetTransactionWithdraw(account, money, dateTime);
                transactions[client].Add(transaction);
                transaction.Execute();
            }
            else
            {
                throw new ClientNotValid();
            }
        }

        public void Deposit(Client client, double money, DateTime dateTime)
        {
            if (client.IsValid())
            {
                if (!clients.ContainsKey(client))
                    throw new NotFoundClient();

                Account account = clients[client].Account;
                ITransaction transaction = transactionManager.GetTransactionDeposit(account, money, dateTime);
                transactions[client].Add(transaction);
                transaction.Execute();
            }
            else
            {
                throw new ClientNotValid();
            }
        }

        public void Transfer(Client fromClient, Client toClient, double money, string toNameBank, DateTime dateTime)
        {
            if (fromClient.IsValid())
            {
                if (!clients.ContainsKey(fromClient))
                    throw new NotFoundClient();

                Account account = clients[fromClient].Account;
                ITransaction transaction =
                    transactionManager.GetTransactionTransfer(account, money, dateTime, centralBank, toClient, toNameBank);
                transactions[fromClient].Add(transaction);
                transaction.Execute();
            }
            else
            {
                throw new ClientNotValid();
            }
        }

        public void UndoTransaction(Client client, int number)
        {
            if (!clients.ContainsKey(client))
                throw new NotFoundClient();

            List<ITransaction> clientTransactions = transactions[client];

            try
            {
                if (number < 0 || number > clientTransactions.Count)
                    throw new InValidTransactionNumber();

                ITransaction transaction = clientTransactions[number - 1];
                transaction.Undo();
            }
            catch (InValidTransactionNumber ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundClient ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ReadOnlyCollection<TransactionInfo> GetTransactions(Client client)
        {
            try
            {
                if (!clients.ContainsKey(client))
                    throw new NotFoundClient();

                List<ITransaction> clientTransactions = transactions[client];

                return clientTransactions.Select((s, index) => new TransactionInfo
                {
                    Amount = s.Amount,
                    Number = index + 1,
                }).ToList().AsReadOnly();
            }
            catch (NotFoundClient ex)
            {
                Console.WriteLine(ex);
            }

            return new ReadOnlyCollection<TransactionInfo>(Enumerable.Empty<TransactionInfo>().ToList());
        }

        public void RemoveNotifyClient(IClientObserver client)
        {
            clientObservers[clients[(Client)client].Account.AccountType].Remove(client);
        }

        public void NotifyObservers(string massage)
        {
            foreach (var key in clientObservers)
            {
                foreach (var client in key.Value)
                {
                    client.Notification(massage);
                }
            }
        }

        public void NotifyObservers(AccountType accountType, string message)
        {
            foreach (var client in clientObservers[accountType])
            {
                client.Notification(message);
            }
        }

        public void Notification(string message)
        {
            CalcPercentageAndFees(DateTime.Now);
        }

        public double GetClientBalanceInfo(Client client)
        {
            if (!clients.ContainsKey(client))
                throw new NotFoundClient();

            return clients[client].Account.Balance;
        }

        public List<Client> GetClients()
        {
            return clients.Keys.Select(s => s).ToList();
        }

        public bool IsAccount(Client client)
        {
            if (!clients.ContainsKey(client))
                throw new NotFoundClient();

            return clients[client].Account != null;
        }

        private void CreateNewAccount(AccountType accountType, Client client, double money, DateTime openDate)
        {
            if (!clients.ContainsKey(client))
                throw new NotFoundClient();

            clients[client].Account = accountManager.GetNewAccount(accountType, money, openDate);
            clientObservers[accountType].Add(client);
        }
    }
}
using Banks.Accounts;

namespace Banks.Commands
{
    public class TransactionManager
    {
        private int currentId = 0;

        public ITransaction GetTransactionWithdraw(Account account, double amount, DateTime dateTime)
        {
            currentId++;
            return new Withdraw(currentId, account, amount, dateTime);
        }

        public ITransaction GetTransactionDeposit(Account account, double amount, DateTime dateTime)
        {
            currentId++;
            return new Deposit(currentId, account, amount, dateTime);
        }

        public ITransaction GetTransactionTransfer(Account account, double amount, DateTime dateTime, CentralBank centralBank, Client toClient, string toNameBank)
        {
            currentId++;
            return new Transfer(currentId, account, amount, dateTime, centralBank, toClient, toNameBank);
        }
    }
}
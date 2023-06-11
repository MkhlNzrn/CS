using Banks.Accounts;
using Banks.Commands;
using Spectre.Console;
using Xunit;

namespace Banks.Test
{
    public class Test
    {
        [Fact]
        public void Test1()
        {
            CentralBank centralBank = CentralBank.GetCentralBank();

            Bank b1 = centralBank.AddNewBank("Bank 1", 8, 30000, 4, new NewDepositCalc());
            Bank b2 = centralBank.AddNewBank("Bank 2", 5, 20000, 3, new DepositCalc());
            Bank b3 = centralBank.AddNewBank("Bank 3", 4, 20000, 3, new DepositCalc());

            Client client = b1.AddNewClientTest(new Client(1) { FullName = "111", Passport = "111", Address = "111" });
            Client client2 = b2.AddNewClientTest(new Client(2) { FullName = "222", Passport = "222", Address = "222" });
            Client client3 = b3.AddNewClientTest(new Client(3) { FullName = "333", Passport = "333", Address = "333" });

            b1.OpenNewDebitAccount(client, 1000, new DateTime(2020, 05, 10));
            var balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(1000, balance);

            b2.OpenNewDepositAccount(client2, 1000, new DateTime(2021, 06, 10));
            balance = b2.GetClientBalanceInfo(client2);
            Assert.Equal(1000, balance);

            b3.OpenNewCreditAccount(client3, 3000, new DateTime(2021, 05, 10));
            balance = b3.GetClientBalanceInfo(client3);
            Assert.Equal(3000, balance);

            b1.Deposit(client, 500, new DateTime(2020, 05, 15));
            balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(1500, balance);

            b1.Deposit(client, 500, new DateTime(2020, 05, 30));
            balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(2000, balance);

            b1.Deposit(client, 500, new DateTime(2020, 06, 05));
            balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(2506.91, balance);

            b2.Withdraw(client2, 500, new DateTime(2021, 06, 20));
            balance = b2.GetClientBalanceInfo(client2);
            Assert.Equal(1000, balance);

            b2.Deposit(client2, 500, new DateTime(2021, 07, 20));
            balance = b2.GetClientBalanceInfo(client2);
            Assert.Equal(1530, balance);

            b2.Withdraw(client2, 200, new DateTime(2021, 07, 22));
            balance = b2.GetClientBalanceInfo(client2);
            Assert.Equal(1330, balance);

            b3.Deposit(client3, 2000, new DateTime(2022, 05, 15));
            balance = b3.GetClientBalanceInfo(client3);
            Assert.Equal(5000, balance);

            b3.Transfer(client3, client, 1000, "Bank 1", new DateTime(2022, 05, 16));
            balance = b3.GetClientBalanceInfo(client3);
            Assert.Equal(4000, balance);

            balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(3890.54, balance);

            IReadOnlyCollection<TransactionInfo> transactions = b3.GetTransactions(client3);
            TransactionInfo transactionInfo = transactions.Last();
            b3.UndoTransaction(client3, transactionInfo.Number);

            balance = b3.GetClientBalanceInfo(client3);
            Assert.Equal(5000, balance);

            balance = b1.GetClientBalanceInfo(client);
            Assert.Equal(2890.54, balance);

            b3.Withdraw(client3, 10000, new DateTime(2022, 07, 15));
            balance = b3.GetClientBalanceInfo(client3);
            Assert.Equal(balance, -5000);
        }
    }
}
using Banks.Accounts;

namespace Banks.Commands
{
    public class Withdraw : ITransaction
    {
        private Account account;
        private double amount;
        private int id;

        public Withdraw(int id, Account account, double amount, DateTime dateTime)
        {
            this.id = id;
            this.account = account;
            this.amount = amount;
            TransactionDate = dateTime;
        }

        public double Amount => amount;
        public double Balance => this.account.Balance;

        public DateTime TransactionDate { get; }

        public void Execute()
        {
            if (account.IsValidBalance(amount, TransactionDate))
            {
                this.account.CalcBankOperation(amount, TransactionDate);
                this.account.Balance = this.account.Balance - amount;
            }
        }

        public void Undo()
        {
            this.account.Balance = this.account.Balance + amount;
        }
    }
}
namespace Banks.Accounts
{
    public abstract class Account
    {
        public int Id { get; protected set; }
        public double Balance { get; set; }
        public DateTime OpenDate { get; protected set; }
        public DateTime LastBankOperation { get; protected set; }
        public AccountType AccountType { get; protected set; }
        public abstract bool IsValidBalance(double money, DateTime dateTime);
        public abstract void CalcBankOperation(double amount, DateTime dateTime);
        public abstract void CalcBankShowOperation(DateTime dateTime);
    }
}
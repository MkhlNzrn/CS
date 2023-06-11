namespace Banks.Accounts
{
    public class DepositAccountFactory : IAccountFactory
    {
        private Bank bank;

        public DepositAccountFactory(Bank bank)
        {
            this.bank = bank;
        }

        public Account GetNewAccount(int id, double money, DateTime openDate)
        {
            return new DepositAccount(id, money, openDate, bank.DepositCalc.Calculate(money));
        }
    }
}
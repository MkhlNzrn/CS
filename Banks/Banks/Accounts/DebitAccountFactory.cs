namespace Banks.Accounts
{
    public class DebitAccountFactory : IAccountFactory
    {
        private Bank bank;
        public DebitAccountFactory(Bank bank)
        {
            this.bank = bank;
        }

        public Account GetNewAccount(int id, double money, DateTime openDate)
        {
            return new DebitAccount(id, money, bank.FixedPercentage, openDate);
        }
    }
}

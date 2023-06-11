namespace Banks.Accounts
{
    public class CreditAccountFactory : IAccountFactory
    {
        private Bank bank;
        public CreditAccountFactory(Bank bank)
        {
            this.bank = bank;
        }

        public Account GetNewAccount(int id, double money, DateTime openDate)
        {
            return new CreditAccount(id, money, bank.CreditLimit, bank.Fees, openDate);
        }
    }
}

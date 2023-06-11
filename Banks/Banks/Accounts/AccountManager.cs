namespace Banks.Accounts
{
    public class AccountManager
    {
        private Dictionary<AccountType, IAccountFactory> accountFactories;
        private int id = 0;

        public AccountManager()
        {
            accountFactories = new Dictionary<AccountType, IAccountFactory>();
        }

        public void AddAccountFactory(AccountType accountType, IAccountFactory accountFactory)
        {
            accountFactories.Add(accountType, accountFactory);
        }

        public Account GetNewAccount(AccountType type, double money, DateTime openDate)
        {
            id++;
            Account account = accountFactories[type].GetNewAccount(id, money, openDate);

            return account;
        }
    }
}
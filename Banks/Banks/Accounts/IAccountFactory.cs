namespace Banks.Accounts
{
    public interface IAccountFactory
    {
        Account GetNewAccount(int id, double money, DateTime openDate);
    }
}
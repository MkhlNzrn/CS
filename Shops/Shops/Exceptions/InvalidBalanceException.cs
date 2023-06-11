namespace Shops
{
    public class InvalidBalanceException : Exception
    {
        public InvalidBalanceException(string message)
            : base(message)
        {
        }
    }
}
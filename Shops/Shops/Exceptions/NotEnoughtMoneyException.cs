namespace Shops
{
    public class NotEnoughtMoneyException : Exception
    {
        public NotEnoughtMoneyException(string message)
            : base(message)
        {
        }
    }
}

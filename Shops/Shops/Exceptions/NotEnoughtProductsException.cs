namespace Shops
{
    public class NotEnoughtProductsException : Exception
    {
        public NotEnoughtProductsException(string message)
            : base(message)
        {
        }
    }
}
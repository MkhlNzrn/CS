namespace Shops
{
    public class NoProductException : Exception
    {
        public NoProductException(string message)
            : base(message)
        {
        }
    }
}
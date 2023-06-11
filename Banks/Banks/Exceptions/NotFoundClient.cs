namespace Banks.Exceptions
{
    public class NotFoundClient : Exception
    {
        public NotFoundClient()
            : base("Клиент не найден")
        {
        }
    }
}
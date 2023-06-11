namespace Banks.Exceptions
{
    public class InValidTransactionNumber : Exception
    {
        public InValidTransactionNumber()
            : base("Неверный номер транзакции")
        {
        }
    }
}
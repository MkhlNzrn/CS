namespace Banks.Exceptions
{
    public class ClientNotValid : Exception
    {
        public ClientNotValid()
            : base("Запрет операции.Необходимо введите адрес и номер паспорта.")
        {
        }
    }
}
namespace Banks.Builder
{
    public class ClientBuilder : Builder
    {
        private static int id;
        private Client client;

        public ClientBuilder()
        {
        }

        public override void BuildEmptyClient()
        {
            id++;
            client = new Client(id);
            Console.WriteLine("Ввод клиента №" + id);
        }

        public override void BuildAddress()
        {
            Console.WriteLine("Введите адрес");
            client.Address = Console.ReadLine();
        }

        public override void BuildFullName()
        {
            while (true)
            {
                Console.WriteLine("Введите имя и фамилию  (обязательно)");
                client.FullName = Console.ReadLine();
                if (!string.IsNullOrEmpty(client.FullName))
                {
                    break;
                }
            }
        }

        public override void BuildPassport()
        {
            Console.WriteLine("Введите паспортные данные");
            client.Passport = Console.ReadLine();
        }

        public override Client GetClient()
        {
            return client;
        }

        public override void BuildFromClient(Client client)
        {
            this.client = client;
        }
    }
}
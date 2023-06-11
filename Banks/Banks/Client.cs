using Banks.Observer;

namespace Banks
{
    public class Client : IClientObserver
    {
        public Client(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            return client != null && client.Id == this.Id;
        }

        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(Address)) && (!string.IsNullOrEmpty(Passport));
        }

        public void Notification(string message)
        {
            Console.WriteLine(message);
        }
    }
}
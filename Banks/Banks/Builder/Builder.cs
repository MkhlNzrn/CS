namespace Banks.Builder
{
    public abstract class Builder
    {
        public abstract void BuildEmptyClient();
        public abstract void BuildFromClient(Client client);
        public abstract void BuildFullName();
        public abstract void BuildAddress();
        public abstract void BuildPassport();
        public abstract Client GetClient();
    }
}

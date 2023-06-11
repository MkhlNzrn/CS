namespace Banks.Commands
{
    public interface ITransaction
    {
        double Amount { get; }
        double Balance { get; }
        DateTime TransactionDate { get;  }
        void Execute();
        void Undo();
    }
}

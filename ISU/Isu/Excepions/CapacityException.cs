namespace Isu.Excepions;

public class CapacityException : Exception
{
    public CapacityException(string message)
        : base(message)
    {
    }
}
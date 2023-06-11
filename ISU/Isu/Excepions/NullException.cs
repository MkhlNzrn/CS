namespace Isu.Excepions;

public class NullException : Exception
{
    public NullException(string message)
        : base(message)
    {
    }
}
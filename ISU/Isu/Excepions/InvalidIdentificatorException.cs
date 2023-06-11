namespace Isu.Excepions;

public class InvalidIdentificatorException : Exception
{
    public InvalidIdentificatorException(string message)
        : base(message)
    {
    }
}
namespace Isu.Excepions;

public class ValueRepeatingException : Exception
{
    public ValueRepeatingException(string message)
        : base(message)
    {
    }
}
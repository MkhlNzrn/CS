namespace Isu.Extra.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException(string message)
        : base(message)
    {
    }
}
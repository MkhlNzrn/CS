namespace Isu.Excepions;

public class StudentNotExistException : Exception
{
    public StudentNotExistException(string message)
        : base(message)
    {
    }
}
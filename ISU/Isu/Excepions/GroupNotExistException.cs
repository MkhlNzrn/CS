namespace Isu.Excepions;

public class GroupNotExistException : Exception
{
    public GroupNotExistException(string message)
        : base(message)
    {
    }
}
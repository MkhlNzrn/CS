namespace Isu.Excepions;

public class IncorrectGroupName : Exception
{
    public IncorrectGroupName(string message)
        : base(message)
    {
    }
}
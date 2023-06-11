namespace Isu.Excepions;

public class IncorrectCourseNumber : Exception
{
    public IncorrectCourseNumber(string message)
        : base(message)
    {
    }
}
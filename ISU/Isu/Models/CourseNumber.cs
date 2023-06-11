using Isu.Entities;
using Isu.Excepions;

namespace Isu.Models;

public class CourseNumber
{
    public const int Maxcourse = 4;
    public const int Mincourse = 1;

    public CourseNumber(int num)
    {
        if (num > Maxcourse && num < Mincourse) throw new IncorrectCourseNumber("number is incorrect");
        Number = num;
    }

    public int Number { get; }
}
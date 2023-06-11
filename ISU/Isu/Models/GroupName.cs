using System;
using System.Dynamic;
using Isu.Entities;
using Isu.Excepions;

namespace Isu.Models;

public class GroupName
{
    private const int GroupNameLenght = 4;

    public GroupName(string line)
    {
        if (!CheckName(line)) throw new IncorrectGroupName("Incorrect group name");
        Name = line;
        Course = new CourseNumber(line[1]);
    }

    public string Name { get; }

    public CourseNumber Course { get; }

    public static bool CheckName(string name)
    {
        if (name == null) throw new NullException("There are null");
        return name.Length <= GroupNameLenght &&
               char.IsLetter(name[0]) &&
               char.GetNumericValue(name[1]) <= 4 &&
               char.GetNumericValue(name[1]) >= 1 &&
               char.IsNumber(name[2]) &&
               char.IsNumber(name[3]);
    }

    public CourseNumber GetCourse()
    {
        return this.Course;
    }

    public string GetName()
    {
        return this.Name;
    }
}
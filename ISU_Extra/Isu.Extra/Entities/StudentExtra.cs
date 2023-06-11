using Isu.Entities;
using Isu.Excepions;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Newtonsoft.Json.Schema;

namespace Isu.Extra.Entities;

public class StudentExtra : Student
{
    public const int MaxCoursesQuantity = 3;
    private List<OGNP> courses = new List<OGNP>(MaxCoursesQuantity);
    private GroupExtra _groupExtra;
    public StudentExtra(string name, int id, GroupExtra group)
        : base(name, id)
    {
        ArgumentNullException.ThrowIfNull(group);
        Faculty = GroupExtra.Faculty;
        _groupExtra = group;
    }

    public IReadOnlyCollection<OGNP> Courses => courses;
    public char Faculty { get; set; }

    public void AddToCourse(OGNP course)
    {
        ArgumentNullException.ThrowIfNull(course);
        if (course.Faculty == Faculty) throw new FacultyCollision("Student and course has the same faculty");
        if (courses.Count >= MaxCoursesQuantity) throw new CapacityException("Student cannot have more than 3 courses");
        courses.Add(course);
    }

    public void DeleteFrom(OGNP course)
    {
        ArgumentNullException.ThrowIfNull(course);
        courses.Remove(course);
    }

    public bool IsRegisteredToCourse()
    {
        return courses.Count != 0;
    }

    public IReadOnlyCollection<Lesson> Getlessons()
    {
       return _groupExtra.Lessons;
    }
}
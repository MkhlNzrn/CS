using System.Collections.ObjectModel;
using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : Group
{
    private const int FacultyPosition = 0;
    private List<Lesson> _lessons = new List<Lesson>();
    private List<StudentExtra> _students = new List<StudentExtra>(MaxGroupSize);

    public GroupExtra(List<IStudent> groupList, GroupName name, CourseNumber courseNumber)
        : base(groupList, name, courseNumber)
    {
        Faculty = name.GetName()[FacultyPosition];
    }

    public static char Faculty { get; set; }
    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public IReadOnlyCollection<StudentExtra> GetUnregisteredStudents()
    {
        List<StudentExtra> result = _students.Where(student => !student.IsRegisteredToCourse()).ToList();
        return new ReadOnlyCollection<StudentExtra>(result);
    }
}
using Isu.Excepions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Flow
{
    public const int FlowCapacity = 30;
    private List<StudentExtra> _students = new List<StudentExtra>(FlowCapacity);
    private List<Lesson> _lessons = new List<Lesson>();

    public IReadOnlyCollection<StudentExtra> ExtraStudents => _students;
    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public bool IsFull()
    {
        return _students.Count >= FlowCapacity;
    }

    public void AddStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Add(student);
    }

    public void AddLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        if (_lessons.Contains(lesson)) throw new ValueRepeatingException("This flow already contains such lesson");
        _lessons.Add(lesson);
    }

    public bool Contains(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        return _students.Contains(student);
    }

    public void UnregisterStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Remove(student);
    }
}
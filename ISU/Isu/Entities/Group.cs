using System.Runtime.CompilerServices;
using Isu.Excepions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IGroup
{
    public const int MaxGroupSize = 30;
    private readonly GroupName _name;
    private List<IStudent> _groupList;

    public Group(List<IStudent> groupList, GroupName name, CourseNumber courseNumber)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(groupList);
        _groupList = groupList;
        _name = name;
    }

    public void AddStudent(IStudent student)
    {
        if (student == null) throw new NullException("Student is null");
        if (_groupList.Count < MaxGroupSize) _groupList.Add(student);
    }

    public GroupName Name()
    {
        return _name;
    }

    public CourseNumber Course() => new CourseNumber((int)char.GetNumericValue(_name.Name[1]));

    public IEnumerable<IStudent> GetStudents()
    {
        return _groupList;
    }

    public IStudent GetStudentFromThisGroup(int id)
    {
        return _groupList.SingleOrDefault(x => x != null && x.GetId() == id);
    }

    public void Delete(IStudent student)
    {
        if (student == null) throw new NullException("Student is null");
        _groupList.Remove(student);
    }

    public IStudent IsReal(int id)
    {
        IStudent student = _groupList.SingleOrDefault(x => x != null && x.GetId() == id);
        return student;
    }
}
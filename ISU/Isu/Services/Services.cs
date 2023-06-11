using Isu.Entities;
using Isu.Excepions;
using Isu.Models;

namespace Isu.Services;

public class Services : IIsuService
{
    private List<IGroup> _groups = new List<IGroup>();
    private int _studentidentificator = 0;
    public IReadOnlyCollection<IGroup> ListOfGroups => _groups;

    public IGroup AddGroup(GroupName name)
    {
        if (name == null) throw new NullException("Null");
        if (ListOfGroups.Any(groups => groups != null && groups.Name().Equals(name)))
            throw new ValueRepeatingException("Name repeating");
        IGroup group = new Group(new List<IStudent>(), name, new CourseNumber(name.GetName()[1]));
        _groups.Add(group);
        return group;
    }

    public IStudent AddStudent(IGroup group, string name)
    {
        if (name == null || group == null) throw new NullException("there are null");
        if (group.GetStudents().ToList().Count >= 30) throw new CapacityException("More than 30");
        var student = new Student(name, _studentidentificator);
        group.AddStudent(student);
        _studentidentificator++;
        return student;
    }

    public IStudent GetStudent(int id)
    {
        var student = FindStudent(id);
        return student ?? throw new NullException("null");
    }

    public IStudent FindStudent(int id)
    {
        if (id < 0) return null;
        IGroup group = ListOfGroups.SingleOrDefault(x => x?.IsReal(id) != null);
        if (group == null) return null;
        return group.GetStudentFromThisGroup(id);
    }

    public IReadOnlyCollection<IStudent> FindStudents(GroupName groupName)
    {
        if (groupName == null) throw new NullException("There are null");
        IGroup group = ListOfGroups.SingleOrDefault(group => group?.Name() == groupName);
        if (group == null) throw new NullException("Null");
        return (IReadOnlyCollection<IStudent>)group.GetStudents();
    }

    public IReadOnlyCollection<IGroup> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber == null) throw new NullException("There are null");
        var students1 = ListOfGroups.Where(x => x?.Course() == courseNumber).ToList();
        if (students1 == null) throw new NullException("there are null");
        return students1;
    }

    public IGroup FindGroup(GroupName groupName)
    {
        if (groupName == null) throw new NullException("there are null");
        return ListOfGroups.FirstOrDefault(group => group != null && group.Name() == groupName);
    }

    public IReadOnlyCollection<IGroup> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber == null) throw new NullException("there are null");
        return ListOfGroups.Where(x => x != null && x.Course() == courseNumber).ToList();
    }

    public void ChangeStudentGroup(IStudent student, IGroup newGroup)
    {
        if (student == null || newGroup == null) throw new NullException("Null student or group");
        GetStudent(student.GetId());
        if (FindGroup(newGroup.Name()) == null) throw new GroupNotExistException("Group not found");
        newGroup.AddStudent(student);
        _groups.FirstOrDefault(x => x != null && x.GetStudents().Contains(student))?.Delete(student);
    }
}
using Isu.Entities;
using Isu.Excepions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    private Services.Services _isu = new Services.Services();
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var group = new Group(new List<IStudent>(), new GroupName("M211"), new CourseNumber(2));
        _isu.AddGroup(group.Name());
        IStudent student = _isu.AddStudent(group, "Maxim");
        if (student != null) Assert.Equal(group.GetStudentFromThisGroup(student.GetId()), student);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var group1 = new Group(new List<IStudent>(), new GroupName("M111"), new CourseNumber(1));
        for (int i = 0; i < Group.MaxGroupSize; i++)
        {
            _isu.AddStudent(group1, "Tester");
        }

        Assert.Throws<CapacityException>(() => _isu.AddStudent(group1, "Michael"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<IncorrectGroupName>(() => new GroupName("Michael"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        IGroup group1 = _isu.AddGroup(new GroupName("M211"));
        IGroup group2 = _isu.AddGroup(new GroupName("M212"));
        IStudent student = _isu.AddStudent(group1, "Tester") ?? throw new InvalidOperationException();
        if (student != null)
        {
            _isu.ChangeStudentGroup(student, group2);
            Assert.Contains(_isu.FindStudents(group2.Name()), student2 => student2 == student);
        }
    }
}
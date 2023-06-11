using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    IGroup AddGroup(GroupName name);
    IStudent AddStudent(IGroup group, string name);

    IStudent GetStudent(int id);
    IStudent FindStudent(int id);
    IReadOnlyCollection<IStudent> FindStudents(GroupName groupName);
    IReadOnlyCollection<IGroup> FindStudents(CourseNumber courseNumber);

    IGroup FindGroup(GroupName groupName);
    IReadOnlyCollection<IGroup> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(IStudent student, IGroup newGroup);
}
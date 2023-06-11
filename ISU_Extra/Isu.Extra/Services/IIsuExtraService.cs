using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService : IIsuService
{
    OGNP AddOgnpCourse(char faculty);
    void RegisterStudent(StudentExtra student, OGNP course);
    void UnregisterStudent(StudentExtra student, OGNP course);
    IReadOnlyCollection<Flow> GetCourseParts(OGNP course);
    IReadOnlyCollection<StudentExtra> GetStudentsByGroup(Flow group);
    IReadOnlyCollection<StudentExtra> GetUnregisteredByGroup(GroupExtra group);
}
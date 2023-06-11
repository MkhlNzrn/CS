using Isu.Models;

namespace Isu.Entities;

public interface IGroup
{
     void AddStudent(IStudent student);

     GroupName Name();

     CourseNumber Course();

     IEnumerable<IStudent> GetStudents();

     IStudent GetStudentFromThisGroup(int id);

     void Delete(IStudent student);

     IStudent IsReal(int id);
}
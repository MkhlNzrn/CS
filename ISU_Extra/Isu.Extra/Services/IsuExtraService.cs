using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public class IsuExtraService : Isu.Services.Services, IIsuExtraService
{
    private List<GroupExtra> _groupExtra = new List<GroupExtra>();
    private List<OGNP> _ognpCourses = new List<OGNP>();
    public IReadOnlyCollection<GroupExtra> ListOfGroupsExtra => _groupExtra;

    public OGNP AddOgnpCourse(char faculty)
    {
        OGNP course = new OGNP(faculty);
        _ognpCourses.Add(course);
        return course;
    }

    public void RegisterStudent(StudentExtra student, OGNP course)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(course);
        if (student.Faculty == course.Faculty)
            throw new FacultyCollision("student and course cannot belong to same faculty");
        IReadOnlyCollection<Lesson> courseLessons = course.GetLessonsForStudent();
        IReadOnlyCollection<Lesson> studentLessons = student.Getlessons();
        if (studentLessons.Any(lesson => course.GetLessonsForStudent().Contains(lesson)))
            throw new LessonCrossing("student has lesson crossing with this course");

        student.AddToCourse(course);
        course.AddStudent(student);
    }

    public void UnregisterStudent(StudentExtra student, OGNP course)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(course);
        course.UnregisterStudent(student);
        student.DeleteFrom(course);
    }

    public IReadOnlyCollection<Flow> GetCourseParts(OGNP course)
    {
        ArgumentNullException.ThrowIfNull(course);
        return course.Flows;
    }

    public IReadOnlyCollection<StudentExtra> GetStudentsByGroup(Flow group)
    {
        ArgumentNullException.ThrowIfNull(group);
        return group.ExtraStudents;
    }

    public IReadOnlyCollection<StudentExtra> GetUnregisteredByGroup(GroupExtra group)
    {
        ArgumentNullException.ThrowIfNull(group);
        return group.GetUnregisteredStudents();
    }
}
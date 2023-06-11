using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class Test
{
    private IsuExtraService _isuExtraService = new IsuExtraService();
    private OGNP _ognp = new OGNP('P');

    [Fact]
    public void AddLessonsAndRegisterOgnpTest()
    {
        GroupExtra groupExtra = new GroupExtra(new List<IStudent>(), new GroupName("M211"), new CourseNumber(2));
        StudentExtra studentExtra = new StudentExtra("Miqael", 1, groupExtra);
        Lesson lesson = new Lesson("Saturday", "10:00", groupExtra, "Роман Макаревич", 112);
        _ognp.AddLessonToFlow(lesson, _ognp.Flows.ToList()[0]);
        _isuExtraService.RegisterStudent(studentExtra, _ognp);
        Assert.Contains(studentExtra, _ognp.Flows.ToList()[0].ExtraStudents);
    }

    [Fact]
    public void InvalidInputInLesson()
    {
        GroupExtra groupExtra = new GroupExtra(new List<IStudent>(), new GroupName("M211"), new CourseNumber(2));
        StudentExtra studentExtra = new StudentExtra("Miqael", 1, groupExtra);
        groupExtra = null;
        Assert.Throws<ArgumentNullException>(() => new Lesson("Saturday", "10:00", groupExtra, "Роман Макаревич", 112));
    }
}
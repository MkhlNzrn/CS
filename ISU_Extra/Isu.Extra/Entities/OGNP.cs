using Isu.Excepions;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNP
{
    private List<Flow> _flows;
    private int lastFlowPosition = 0;

    public OGNP(char faculty)
    {
        _flows = new List<Flow>();
        _flows.Add(new Flow());
        Faculty = faculty;
    }

    public char Faculty { get; set; }
    public IReadOnlyCollection<Flow> Flows => _flows;

    public void AddStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (_flows[lastFlowPosition].IsFull() && lastFlowPosition == _flows.Count)
            throw new CapacityException("Course is full");
        _flows[lastFlowPosition].AddStudent(student);
    }

    public void AddLessonToFlow(Lesson lesson, Flow flow)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        ArgumentNullException.ThrowIfNull(flow);
        if (!_flows.Contains(flow)) throw new InvalidValueException("invalid flow given");
        flow.AddLesson(lesson);
    }

    public void UnregisterStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        foreach (var flow in _flows.Where(flow => flow.Contains(student)))
        {
            flow.UnregisterStudent(student);
        }
    }

    public IReadOnlyCollection<Lesson> GetLessonsForStudent()
    {
        return _flows[lastFlowPosition].Lessons;
    }
}
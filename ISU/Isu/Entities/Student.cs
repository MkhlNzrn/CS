using Isu.Excepions;
using Isu.Models;

namespace Isu.Entities;

public class Student : IStudent
{
    private readonly int _id;
    private string _name;
    public Student(string name, int id)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new NullException("null");
        _name = name;
        _id = id;
    }

    public int GetId()
    {
        return _id;
    }
}
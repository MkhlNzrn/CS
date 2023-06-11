using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Lesson : IEquatable<Lesson>
{
        public Lesson(string day, string time, GroupExtra group, string teacher, int room)
        {
            ArgumentNullException.ThrowIfNull(day);
            ArgumentNullException.ThrowIfNull(group);
            ArgumentNullException.ThrowIfNull(teacher);
            ArgumentNullException.ThrowIfNull(time);
            Day = day;
            Time = time;
            Group = group;
            Teacher = teacher;
            Room = room;
        }

        public string Day { get; }
        public string Time { get; }
        public GroupExtra Group { get; }
        public string Teacher { get; }
        public int Room { get; }

        public bool Equals(Lesson? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Day == other.Day && Time == other.Time;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Lesson)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Time);
        }
}
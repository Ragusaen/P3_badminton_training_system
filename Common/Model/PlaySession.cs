using System;

namespace Common.Model
{
    public abstract class PlaySession
    {
        public enum Type {Practice, Match};

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
    }
}

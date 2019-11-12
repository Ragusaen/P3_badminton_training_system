using System;

namespace Common.Model
{
    public abstract class PlaySession
    {
        public enum Type {Practice, Match};

        public int id;
        public DateTime Start;
        public DateTime End;
        public string Location;
    }
}

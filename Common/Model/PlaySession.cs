using System;

namespace Common.Model
{
    public abstract class PlaySession
    {
        private int id;
        public DateTime Start;
        public DateTime End;
        public string Location;
    }
}

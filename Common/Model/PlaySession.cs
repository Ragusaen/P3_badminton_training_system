using System;
using System.Xml.Serialization;

namespace Common.Model
{
    [XmlInclude(typeof(PracticeSession))]
    [XmlInclude(typeof(TeamMatch))]
    public abstract class PlaySession
    {
        [Flags]
        public enum Type {Practice = 1, Match = 2};

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
    }
}

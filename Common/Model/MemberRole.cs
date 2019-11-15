using System.Reflection;

namespace Common.Model
{
    public abstract class MemberRole
    {
        public Member Member;
    }

    public enum MemberType { None, Player, Trainer, Both }
}

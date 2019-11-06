using System.Collections.Generic;
using Common.Model.Member;

namespace Common.Model.Positions
{
    class MensDouble : IPosition
    {
        public bool Legal { get; set; }
        public List<Player> Player { get; set; } = new List<Player>();
    }
}

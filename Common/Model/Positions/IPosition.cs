using System.Collections.Generic;
using Common.Model.Member;

namespace Common.Model.Positions
{
    public interface IPosition
    {
        bool Legal { get; set; }
        List<Player> Player { get; set; }
    }
}

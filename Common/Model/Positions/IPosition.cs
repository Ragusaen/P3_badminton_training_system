using System.Collections.Generic;
using Common.Model.Member;

namespace Common.Model.Positions
{
    interface IPosition
    {
        bool Legal { get; set; }
        List<Player> Player { get; set; }
    }
}

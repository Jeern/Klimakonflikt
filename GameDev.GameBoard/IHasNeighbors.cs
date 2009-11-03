using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.GameBoard
{
    public interface IHasNeighbours<N>
    {
        IEnumerable<N> AccessibleNeighbours { get; }
    }
}

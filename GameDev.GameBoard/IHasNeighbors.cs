using System.Collections.Generic;

namespace GameDev.GameBoard
{
    public interface IHasNeighbours<N>
    {
        IEnumerable<N> AccessibleNeighbours { get; }
    }
}

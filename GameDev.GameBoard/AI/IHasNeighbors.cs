using System.Collections.Generic;

namespace GameDev.GameBoard.AI
{
    public interface IHasNeighbours<N>
    {
        IEnumerable<N> AccessibleNeighbours { get; }
    }
}

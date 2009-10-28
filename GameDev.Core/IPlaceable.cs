using System;

namespace GameDev.Core
{
    public interface IPlaceable
    {
        int X { get; set; }
        int Y { get; set; }

        void Move(Direction direction, float distance);

    }
}

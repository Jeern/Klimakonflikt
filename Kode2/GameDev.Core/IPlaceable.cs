using System;

namespace GameDev.Core
{
    public interface IPlaceable
    {
        int X { get; set; }
        int Y { get; set; }
        //int XOffset { get; set; }
        //int YOffset { get; set; }

        void Move(Direction direction, float distance);
        //void ChangeOffset(Direction direction, float distance);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core
{
    public class DirectionChanger
    {
        public DirectionChanger() : this(0, 0) { }

        public DirectionChanger(int deltaX, int deltaY)
        {
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
        }

        public int DeltaX;
        public int DeltaY;

        public void Offset(DirectionChanger offset)
        {
            this.DeltaX += offset.DeltaX;
            this.DeltaY += offset.DeltaY;
        }

        public void Offset(int x, int y)
        {
            this.DeltaX += x;
            this.DeltaY += y;
        }


    }
}

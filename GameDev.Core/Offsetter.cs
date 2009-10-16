using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core
{
    public struct Offsetter
    {
        public Offsetter(int deltaX, int deltaY)
        {
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
        }

        public int DeltaX;
        public int DeltaY;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameDev.Core
{
    public class DirectionChanger : IEquatable<DirectionChanger>
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
            this.Offset(offset.DeltaX, offset.DeltaY);
        }

        public void Offset(int x, int y)
        {
            this.DeltaX += x;
            this.DeltaY += y;
        }

        #region IEquatable<DirectionChanger> Members

        public bool Equals(DirectionChanger other)
        {
            return (other.DeltaX == this.DeltaX && other.DeltaY == this.DeltaY);
        }

        #endregion

        public override int GetHashCode()
        {
            return ((DeltaX % Int16.MaxValue) << 16) + (DeltaY % Int16.MaxValue);
        }


        public Direction GetDirection()
        {
            return DirectionHelper8.GetDirection(DeltaX, DeltaY);
        }


        public override string ToString()
        {
            return this.GetType() + "DeltaX: " + DeltaX + ", DeltaY: " + DeltaY;
        }

    }
}

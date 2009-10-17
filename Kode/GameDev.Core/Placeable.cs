using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core
{
    public struct Placeable : IPlaceable
    {
        private int _x;
        private int _y;

        public Placeable(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #region IPlaceable Members

        public int X
        {
            get { return _x; }
            set { _x = value;}
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        #endregion
    }
}

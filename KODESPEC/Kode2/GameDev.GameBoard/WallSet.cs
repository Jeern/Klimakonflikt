using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.GameBoard
{
    public class WallSet
    {
        public bool HasTopBorder { get; set; }
        public bool HasRightBorder { get; set; }
        public bool HasBottomBorder { get; set; }
        public bool HasLeftBorder { get; set; }
    }
}

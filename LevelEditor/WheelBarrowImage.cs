using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class WheelBarrowImage : MoveableImage
    {
        public WheelBarrowImage(Canvas maze, Coordinate coordinate) : base(maze, "WheelBarrow.png", coordinate) { }

        public override string XmlName
        {
            get { return "Wheelbarrow"; }
        }

    }
}

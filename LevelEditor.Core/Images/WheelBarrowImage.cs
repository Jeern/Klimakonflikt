using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LevelEditor.Core.MazeItems;

namespace LevelEditor.Core.Images
{
    public class WheelBarrowImage : MoveableImage
    {
        public WheelBarrowImage(Canvas maze, Coordinate coordinate) : base(maze, "Resources\\WheelBarrow.png", coordinate) { }

        public override string XmlName
        {
            get { return "Wheelbarrow"; }
        }

    }
}

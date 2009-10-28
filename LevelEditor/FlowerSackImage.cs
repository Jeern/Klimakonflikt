using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class FlowerSackImage : MoveableImage
    {
        public FlowerSackImage(Canvas maze, Coordinate coordinate) : base(maze, "FlowerSack.png", coordinate) { }

        public override string XmlName
        {
            get { return "Flowersack"; }
        }
    }
}

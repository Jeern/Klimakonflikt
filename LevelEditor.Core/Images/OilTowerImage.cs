using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LevelEditor.Core.MazeItems;

namespace LevelEditor.Core.Images
{
    public class OilTowerImage : MoveableImage
    {
        public OilTowerImage(Canvas maze, Coordinate coordinate) : base(maze, "Resources\\Oiltower.png", coordinate) { }

        public override string XmlName
        {
            get { return "Oiltower"; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LevelEditor.Core.MazeItems;

namespace LevelEditor.Core.Images
{
    public class OilBarrelImage : MoveableImage
    {
        public OilBarrelImage(Canvas maze, Coordinate coordinate) : base(maze, "Resources\\OilBarrel.png", coordinate) { }

        public override string XmlName
        {
            get { return "Oilbarrel"; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class OilTowerImage : MoveableImage
    {
        public OilTowerImage(Canvas maze) : base(maze, "Oiltower.png") { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class OilBarrelImage: MoveableImage
    {
        public OilBarrelImage(Canvas maze) : base(maze, "OilBarrel.png") { }
    }
}

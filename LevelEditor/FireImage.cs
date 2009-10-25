using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class FireImage : MoveableImage
    {
        public FireImage(Canvas maze) : base(maze, "Fire.png") { }
    }
}

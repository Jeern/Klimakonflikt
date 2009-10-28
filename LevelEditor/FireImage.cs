using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class FireImage : MoveableImage
    {
        public FireImage(Canvas maze, Coordinate coordinate) : base(maze, "Fire.png", coordinate) { }

        private bool m_HasEgg = true;

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (m_HasEgg && IsMoving)
            {
                m_HasEgg = false;
                MoveableImageController.Images.Add(new FireImage(MazeCanvas, new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y)));
            }
            base.OnMouseMove(e);
        }

        public override string XmlName
        {
            get { return "Fire"; }
        }

    }
}

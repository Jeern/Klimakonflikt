using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LevelEditor.Core.MazeItems;
using LevelEditor.Core.Helpers;

namespace LevelEditor.Core.Images
{
    public class FireImage : MoveableImage
    {
        public FireImage(Canvas maze, Coordinate coordinate)
            : base(maze, "Resources\\Fire.png", coordinate) 
        {
            m_HasEgg = CurrentCoordinate.X < 0 | CurrentCoordinate.Y < 0;
        }

        private bool m_HasEgg = false;

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (m_HasEgg && IsMoving)
            {
                MoveableImageController.Images.Add(new FireImage(MazeCanvas, new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y)));
                m_HasEgg = false;
            }
            base.OnMouseMove(e);
        }

        public override string XmlName
        {
            get { return "Fire"; }
        }

        protected override void OnOutOfBounds(int x, int y, int maxX, int maxY)
        {
            this.Opacity = LEConstants.Invisible;
        }
    }
}

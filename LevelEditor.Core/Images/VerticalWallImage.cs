﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using LevelEditor.Core.MazeItems;
using LevelEditor.Core.Helpers;

namespace LevelEditor.Core.Images
{
    public class VerticalWallImage : StaticImage
    {
        public VerticalWallImage(Canvas maze, Grid grid, int x, int y)
            : base(maze, grid, (x == 0 | x == Maze.HorizontalTiles), "Resources\\Mur vertical.png") 
        {
            VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(this, y);

            if (x == (int)Maze.HorizontalTiles)
            {
                Grid.SetColumn(this, x - 1);
                HorizontalAlignment = HorizontalAlignment.Right;
                RenderTransform = new TranslateTransform(2.0 * LEConstants.XMargin, 0.0);
            }
            else
            {
                Grid.SetColumn(this, x);
                HorizontalAlignment = HorizontalAlignment.Left;
                RenderTransform = new TranslateTransform(-2.0 * LEConstants.XMargin, 0.0);
            }
        }

        private bool m_VisibilityReversed = false;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!m_VisibilityReversed && !MoveableImageController.ImageIsMoving)
            {
                ReverseVisibility();
                m_VisibilityReversed = true;
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (LeftButtonIsDown && !m_VisibilityReversed && !MoveableImageController.ImageIsMoving)
            {
                ReverseVisibility();
                m_VisibilityReversed = true;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            m_VisibilityReversed = false;
            base.OnMouseLeave(e);
        }


    }
}

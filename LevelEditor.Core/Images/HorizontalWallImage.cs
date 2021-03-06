﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using LevelEditor.Core.MazeItems;
using LevelEditor.Core.Helpers;

namespace LevelEditor.Core.Images
{
    public class HorizontalWallImage : StaticImage
    {
        public HorizontalWallImage(Canvas maze, Grid grid, int x, int y)
            : base(maze, grid, (y == 0 | y == Maze.VerticalTiles), "Resources\\Mur horizontal.png")
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(this, x);

            if (y == (int)Maze.VerticalTiles)
            {
                Grid.SetRow(this, y - 1);
                VerticalAlignment = VerticalAlignment.Bottom;
                RenderTransform = new TranslateTransform(0.0, 2.0 * LEConstants.YMargin);
            }
            else
            {
                Grid.SetRow(this, y);
                VerticalAlignment = VerticalAlignment.Top;
                RenderTransform = new TranslateTransform(0.0, -2.0 * LEConstants.YMargin);
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

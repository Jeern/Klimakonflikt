using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor
{
    public class HorizontalWallImage : StaticImage
    {
        public HorizontalWallImage(Canvas maze, Grid grid, int x, int y)
            : base(maze, grid, (y == 0 | y == LEConstants.VerticalTiles), "Mur horizontal.png")
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(this, x);

            if (y == (int)LEConstants.VerticalTiles)
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

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            ReverseVisibility();
            base.OnMouseLeftButtonDown(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor
{
    public class RoundSpotImage : StaticImage
    {
        public RoundSpotImage(Canvas maze, Grid grid, int x, int y)
            : base(maze, grid, false, "mur-afrunding.png")
        {
            Width = 4.0 * LEConstants.XMargin;
            Height = 4.0 * LEConstants.YMargin;

            double xOffset = 0.0;
            double yOffset = 0.0;

            if (y == (int)LEConstants.VerticalTiles)
            {
                Grid.SetRow(this, y - 1);
                VerticalAlignment = VerticalAlignment.Bottom;
                yOffset = 2.0 * LEConstants.YMargin;
            }
            else
            {
                Grid.SetRow(this, y);
                VerticalAlignment = VerticalAlignment.Top;
                yOffset = -2.0 * LEConstants.YMargin;
            }

            if (x == (int)LEConstants.HorizontalTiles)
            {
                Grid.SetColumn(this, x - 1);
                HorizontalAlignment = HorizontalAlignment.Right;
                xOffset = 2.0 * LEConstants.YMargin;
            }
            else
            {
                Grid.SetColumn(this, x);
                HorizontalAlignment = HorizontalAlignment.Left;
                xOffset = -2.0 * LEConstants.YMargin;
            }
            RenderTransform = new TranslateTransform(xOffset, yOffset);

        }
    }
}
    
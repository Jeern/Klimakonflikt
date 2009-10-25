using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor
{
    public class VerticalWallImage : StaticImage
    {
        public VerticalWallImage(Canvas maze, Grid grid, int x, int y)
            : base(maze, grid, (x == 0 | x == LEConstants.HorizontalTiles), "Mur vertical.png") 
        {
            VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(this, y);

            if (x == (int)LEConstants.HorizontalTiles)
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

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            Opacity = ReverseVisibility(Opacity);
            base.OnMouseLeftButtonDown(e);
        }

    }
}

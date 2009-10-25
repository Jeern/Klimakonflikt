using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace LevelEditor
{
    public class MoveableImage : Image
    {
        private bool m_IsMoving = false;
        private Point m_ImageMovePoint;
        private Canvas m_Maze;

        public MoveableImage(Canvas maze, string name)
        {
            m_Maze = maze;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(name, UriKind.Relative);
            src.EndInit();
            Source = src;
            Stretch = Stretch.Uniform;
            Width = 60.0;
            Height = 60.0;
            Visibility = Visibility.Visible;
            Opacity = LEConstants.Visible;
            IsHitTestVisible = true;
            maze.MouseMove += MoveImage;
            maze.Children.Add(this);
        }


        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            m_IsMoving = false;
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            m_IsMoving = true;
            m_ImageMovePoint = e.GetPosition(this);
            base.OnMouseLeftButtonDown(e);
        }

        private void MoveImage(object sender, MouseEventArgs e)
        {
            if (m_IsMoving && e.LeftButton == MouseButtonState.Pressed)
            {
                Point mazePoint = e.GetPosition(m_Maze);
                Point newPoint = new Point(mazePoint.X - m_ImageMovePoint.X, mazePoint.Y - m_ImageMovePoint.Y);
                Canvas.SetLeft(this, newPoint.X);
                Canvas.SetTop(this, newPoint.Y);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevelEditor
{
    public class StaticImage : Image
    {
        private Canvas m_Maze;

        private event EventHandler<EventArgs> m_Changed = delegate { };
        public event EventHandler<EventArgs> Changed
        {
            add { m_Changed += value; }
            remove { m_Changed -= value; }
        }

        public StaticImage(Canvas maze, Grid grid, bool visible, string name)
        {
            m_Maze = maze;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(name, UriKind.Relative);
            src.EndInit();
            Source = src;
            Stretch = Stretch.Uniform;
            Visibility = Visibility.Visible;
            if (visible)
            {
                Opacity = LEConstants.Visible;
            }
            else
            {
                Opacity = LEConstants.Transparent;
            }
            IsHitTestVisible = true;
            grid.Children.Add(this);
            maze.MouseLeftButtonDown += MazeMouseLeftButtonDown;
            maze.MouseLeftButtonUp += MazeMouseLeftButtonUp;
            maze.MouseLeave += MazeMouseLeave;
        }

        private void MazeMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            m_LeftButtonIsDown = false;
        }

        private void MazeMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            m_LeftButtonIsDown = false;
        }

        private void MazeMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            m_LeftButtonIsDown = true;
        }

        private bool m_LeftButtonIsDown = false;
        protected bool LeftButtonIsDown
        {
            get { return m_LeftButtonIsDown; }
        }

        protected virtual void OnChanged()
        {
            m_Changed(this, new EventArgs());
        }

        protected void ReverseVisibility()
        {
            if (Opacity == LEConstants.Visible)
            {
                Opacity = LEConstants.Transparent;
            }
            else
            {
                Opacity = LEConstants.Visible;
            }
            OnChanged();
        }
    }
}

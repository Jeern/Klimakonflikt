using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LevelEditor.Core.Helpers;

namespace LevelEditor.Core.Images
{
    public class StaticImage : Image
    {
        private Canvas m_Maze;

        private static DateTime m_LatestOverallChange = DateTime.MinValue;
        private DateTime m_LatestChange = DateTime.MinValue;
        private static Type m_LatestImageType = null;

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

        private const int SmoothTime = 800; //Milliseconds

        protected void ReverseVisibility()
        {
            //This DateTime logic is a weird logic designed to give a smoother drawing experience.
            DateTime now = DateTime.Now;
            Type thisType = this.GetType();
            if (m_LatestChange.AddMilliseconds(SmoothTime) < now && 
                (m_LatestOverallChange.AddMilliseconds(SmoothTime) < now || m_LatestImageType == thisType))
            {
                m_LatestOverallChange = now;
                m_LatestImageType = thisType;
                m_LatestChange = now;
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
}

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
            //if (checkMouseButton)
            //{
            //    MouseLeftButtonDown += ImageMouseLeftButtonDown;
            //}
            grid.Children.Add(this);
        }

        protected virtual void OnChanged()
        {
            m_Changed(this, new EventArgs());
        }

        protected double ReverseVisibility(double opacity)
        {
            OnChanged();
            if (opacity == LEConstants.Visible)
                return LEConstants.Transparent;
            else
                return LEConstants.Visible;
        }


    }
}

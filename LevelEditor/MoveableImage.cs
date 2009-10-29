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
        public bool IsMoving
        {
            get { return m_IsMoving; }
        }

        private Point m_ImageMovePoint;

        private Canvas m_MazeCanvas;
        protected Canvas MazeCanvas
        {
            get { return m_MazeCanvas; }
        }

        private Coordinate m_CurrentCoordinate;
        public Coordinate CurrentCoordinate
        {
            get { return m_CurrentCoordinate; }
        }

        private Point m_CurrentScreenPosition;

        public MoveableImage(Canvas canvas, string name, Coordinate coordinate)
        {
            m_MazeCanvas = canvas;
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
            m_CurrentCoordinate = coordinate;
            m_CurrentScreenPosition = coordinate;
            SetToCenter();
            canvas.MouseLeave += CanvasMouseLeave;
            canvas.MouseMove += MoveImage;
            canvas.MouseLeftButtonUp += CanvasMouseLeftButtonUp;
            canvas.Children.Add(this);
        }

        private void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            StopMovingImage();
        }

        private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopMovingImage();
        }

        private void StopMovingImage()
        {
            if (m_IsMoving)
            {
                m_IsMoving = false;
                SetToCenter();
                if (CurrentCoordinate.X < 0 || CurrentCoordinate.X > Maze.HorizontalTiles-1 
                    || CurrentCoordinate.Y < 0 || CurrentCoordinate.Y > Maze.VerticalTiles-1)
                {
                    OnOutOfBounds(CurrentCoordinate.X, CurrentCoordinate.Y, Maze.HorizontalTiles-1, Maze.VerticalTiles-1);
                }
            }
        }

        private event EventHandler<OutOfboundsEventArgs> m_OutOfBounds = delegate { };
        public event EventHandler<OutOfboundsEventArgs> OutOfBounds
        {
            add { m_OutOfBounds += value; }
            remove { m_OutOfBounds -= value; }
        }

        protected virtual void OnOutOfBounds(int x, int y, int maxX, int maxY)
        {
            m_OutOfBounds(this, new OutOfboundsEventArgs(x, y, maxX, maxY));
            MoveInsideBounds(x, y, maxX, maxY);
        }

        private void MoveInsideBounds(int x, int y, int maxX, int maxY)
        {
            if (x < 0)
            {
                x = 0;
            }
            if (x > maxX)
            {
                x = maxX;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (y > maxY)
            {
                y = maxY;
            }
            m_CurrentCoordinate = new Coordinate(x, y);
            SetToCenter();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            StopMovingImage();
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
                Point mazePoint = e.GetPosition(m_MazeCanvas);
                m_CurrentScreenPosition = new Point(mazePoint.X - m_ImageMovePoint.X, mazePoint.Y - m_ImageMovePoint.Y);
                m_CurrentCoordinate = m_CurrentScreenPosition;
                ShowImageAtPoint();
            }
        }

        private void SetToCenter()
        {
            m_CurrentScreenPosition = m_CurrentCoordinate;
            ShowImageAtPoint();
        }

        private void ShowImageAtPoint()
        {
            Canvas.SetLeft(this, m_CurrentScreenPosition.X);
            Canvas.SetTop(this, m_CurrentScreenPosition.Y);
        }

        public string XmlColumn
        {
            get { return CurrentCoordinate.X.ToString(); }
        }

        public string XmlRow
        {
            get { return CurrentCoordinate.Y.ToString(); }
        }

        public virtual string XmlName 
        {
            get { return "MoveableImage"; }
        }
    }
}

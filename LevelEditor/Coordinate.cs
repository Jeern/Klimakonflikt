using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;

namespace LevelEditor
{
    public class Coordinate
    {
        private int m_X;
        public int X
        {
            get { return m_X; }
        }
        private int m_Y;
        public int Y
        {
            get { return m_Y; }
        }

        public Coordinate(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }
        
        /// <summary>
        /// Converts point coordinate on maze to nearest tile coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static implicit operator Coordinate(Point center)
        {
            //We find the 4 nearest coordinates and use pythagoras to find out which one is actually closest
            
            //First we make 5 points (they actually corresponds to coordinates)
            Point northWest = new Point(NearestSmallerCenterDouble(center.X, Maze.TileWidth), NearestSmallerCenterDouble(center.Y, Maze.TileHeight));
            Point northEast = new Point(NearestLargerCenterDouble(center.X, Maze.TileWidth), NearestSmallerCenterDouble(center.Y, Maze.TileHeight));
            Point southWest = new Point(NearestSmallerCenterDouble(center.X, Maze.TileWidth), NearestLargerCenterDouble(center.Y, Maze.TileHeight));
            Point southEast = new Point(NearestLargerCenterDouble(center.X, Maze.TileWidth), NearestLargerCenterDouble(center.Y, Maze.TileHeight));

            //Then we calculate the point closest to center
            Point closestPoint = MinimumDistance(center, northWest, northEast, southWest, southEast);

            //int x = (int)((closestPoint.X - Maze.TileWidth / 2) / Maze.TileWidth);
            //int y = (int)((closestPoint.Y - Maze.TileHeight / 2) / Maze.TileHeight);
            int x = (int)(closestPoint.X / Maze.TileWidth);
            int y = (int)(closestPoint.Y / Maze.TileHeight);

            return new Coordinate(x, y);
        }

        private static double NearestSmallerCenterDouble(double org, double tileSize)
        {
            //org -= tileSize / 2;
            int tmp = (int)(org / tileSize);
            return tmp * tileSize; // +tileSize / 2;
        }

        private static double NearestLargerCenterDouble(double org, double tileSize)
        {
            return NearestSmallerCenterDouble(org + tileSize, tileSize); 
        }

        /// <summary>
        /// Returns the point with the minimum distance to the center
        /// </summary>
        /// <param name="center"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Point MinimumDistance(Point center, params Point[] points)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException("points has 0 elements");
            return MinimumDistance(center, points.ToList());
        }

        private static Point MinimumDistance(Point center, List<Point> points)
        {
            if (points == null || points.Count == 0)
                throw new ArgumentException("points has 0 elements");
            
            if (points.Count == 1)
                return points[0];

            double minimumDistance = double.MaxValue;
            Point minimumPoint = points[0];
            foreach (Point p in points)
            {
                double distance = Math.Abs((center - p).Length);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    minimumPoint = p;
                }
            }
            return minimumPoint;            
        }

        /// <summary>
        /// Converts tile coordinate to Point coordinate on maze corresponding to center of tile
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static implicit operator Point(Coordinate coordinate)
        {
//            return new Point(coordinate.X * Maze.TileWidth + (Maze.TileWidth / 2), coordinate.Y * Maze.TileHeight + (Maze.TileHeight / 2));
            return new Point(coordinate.X * Maze.TileWidth, coordinate.Y * Maze.TileHeight);
        }

    }
}

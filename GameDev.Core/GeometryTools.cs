using System;
using Microsoft.Xna.Framework;

namespace GameDev.Core
{
    public static class GeometryTools
    {

        public static Rectangle NewBoundingRectangle(Point corner1, Point corner2)
        {
            return new Rectangle(Math.Min(corner1.X, corner2.X), Math.Min(corner1.Y, corner2.Y), Math.Abs(corner1.X - corner2.X), Math.Abs(corner1.Y - corner2.Y));
}

        public static bool IsBetweenPoints(Point pointToCheck, Point a, Point b)
        {
            Rectangle border = NewBoundingRectangle(a, b);
            //Console.WriteLine(border);
            //Console.WriteLine("Top: " + border.Top + ", Bottom: " + border.Bottom + ", Right: " + border.Right + ", Left: " + border.Left);
            return pointToCheck.X >= border.Left 
                && pointToCheck.X <= border.Right
                && pointToCheck.Y >= border.Top
                && pointToCheck.Y <= border.Bottom;
        }

        public static Point GetPosition(this IPlaceable placeable)
        {
            return new Point(placeable.X, placeable.Y);
        }
        public static void SetPosition(this IPlaceable placeable, Point newPosition)
        {
            placeable.X = newPosition.X;
            placeable.Y = newPosition.Y;
        }

        public static Rectangle GetOffsetCopy(this Rectangle rectangleToOffset, int offset)
        {
            return GetOffsetCopy(rectangleToOffset, offset, offset);
        }


        public static Rectangle GetOffsetCopy(this Rectangle rectangleToOffset, int xOffset, int yOffset)
    {
        return new Rectangle(rectangleToOffset.X + xOffset, rectangleToOffset.Y + yOffset, rectangleToOffset.Width, rectangleToOffset.Height);
    }

        public static float DistanceTo(this Point firstPoint, Point otherPoint)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(firstPoint.X - otherPoint.X),2)+  Math.Pow(Math.Abs(firstPoint.Y- otherPoint.Y),2));
        }

        public static Vector2 ToVector(this Point p)
        { return new Vector2(p.X, p.Y); }


    }
}

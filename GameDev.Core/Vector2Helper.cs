#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

using System;

namespace GameDev.Core
{
    public static class Vector2Helper
    {
        public static Vector2 Right = new Vector2(1,0);
        public static Vector2 Up = new Vector2(0, -1);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Down = new Vector2(0, 1);

        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static float DistanceTo(this Vector2 first, Vector2 other)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(first.X - other.X), 2) + Math.Pow(Math.Abs(first.Y - other.Y), 2));
        }


    }
}

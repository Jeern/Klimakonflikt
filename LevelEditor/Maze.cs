using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class Maze
    {
        public static void Initialize(int width, int height, int horizontalTiles, int verticalTiles, string backgroundImageFullName)
        {
            m_Width = width;
            m_Height = height;
            m_HorizontalTiles = horizontalTiles;
            m_VerticalTiles = verticalTiles;
            m_BackgroundImageFullName = backgroundImageFullName;
        }
       
        private static int m_HorizontalTiles;
        public static int HorizontalTiles
        {
            get { return m_HorizontalTiles; }
        }

        private static int m_VerticalTiles;
        public static int VerticalTiles
        {
            get { return m_VerticalTiles; }
        }

        private static int m_Width;
        public static int Width
        {
            get { return m_Width; }
        }

        private static int m_Height;
        public static int Height
        {
            get { return m_Height; }
        }

        private static string m_BackgroundImageFullName;
        public static string BackgroundImageFullName
        {
            get { return m_BackgroundImageFullName; }
        }

        public string BackgroundImage
        {
            get { return m_BackgroundImageFullName; }
        }

        public static double TileWidth
        {
            get { return Width / HorizontalTiles; }
        }

        public static double TileHeight
        {
            get { return Height / VerticalTiles; }
        }



    }
}

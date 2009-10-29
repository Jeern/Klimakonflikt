using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LevelEditor
{
    public class Maze
    {
        public static void Initialize(int width, int height, int horizontalTiles, int verticalTiles, 
            string backgroundImageFullName, int levelNumber, string levelName, double speedFactor)
        {
            m_Width = width;
            m_Height = height;
            m_HorizontalTiles = horizontalTiles;
            m_VerticalTiles = verticalTiles;
            m_BackgroundImageFullName = backgroundImageFullName;
            m_LevelNumber = levelNumber;
            m_LevelName = levelName;
            m_SpeedFactor = speedFactor;
            m_VerticalImages = new Image[(int)HorizontalTiles + 1, (int)VerticalTiles];
            m_HorizontalImages = new Image[(int)HorizontalTiles, (int)VerticalTiles + 1];
            m_RoundedImages = new Image[(int)HorizontalTiles + 1, (int)VerticalTiles + 1];
            m_Tiles = new Tile[(int)HorizontalTiles, (int)VerticalTiles];
        }

        //public void InitializeWallsFromTiles()
        //{
        //}

        /// <summary>
        /// Creates the Tiles array from the walllimages. Used when saving as XML.
        /// </summary>
        public static void InitializeTilesFromWalls()
        {
            for (int x = 0; x < HorizontalTiles; x++)
            {
                for (int y = 0; y < VerticalTiles; y++)
                {
                    m_Tiles[x, y] = new Tile();
                    m_Tiles[x, y].Left = (m_VerticalImages[x, y].Opacity == LEConstants.Visible);
                    m_Tiles[x, y].Right = (m_VerticalImages[x+1, y].Opacity == LEConstants.Visible);
                    m_Tiles[x, y].Top = (m_HorizontalImages[x, y].Opacity == LEConstants.Visible);
                    m_Tiles[x, y].Bottom = (m_HorizontalImages[x, y+1].Opacity == LEConstants.Visible);
                }
            }
        }

        private static double m_SpeedFactor;
        public static double SpeedFactor
        {
            get { return m_SpeedFactor; }
        }

        private static string m_LevelName;
        public static string LevelName
        {
            get { return m_LevelName; }
        }

        private static int m_LevelNumber;
        public static int LevelNumber
        {
            get { return m_LevelNumber; }
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

        private static Image[,] m_VerticalImages;
        public static Image[,] VerticalImages
        {
            get { return m_VerticalImages; }
        }

        private static Image[,] m_HorizontalImages;
        public static Image[,] HorizontalImages
        {
            get { return m_HorizontalImages; }
        }

        private static Image[,] m_RoundedImages;
        public static Image[,] RoundedImages
        {
            get { return m_RoundedImages; }
        }

        private static Tile[,] m_Tiles;
        public static Tile[,] Tiles
        {
            get { return m_Tiles; }
        }

        
    }
}

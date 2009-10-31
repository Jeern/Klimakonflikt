using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Xml.Linq;

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

            if (!FileLoaded)
            {
                m_Tiles = new Tile[(int)HorizontalTiles, (int)VerticalTiles];
                m_FlowersackCoordinate = new Coordinate((int)HorizontalTiles - 1, (int)VerticalTiles - 1);
                m_WheelbarrowCoordinate = new Coordinate((int)HorizontalTiles - 1, (int)VerticalTiles - 2);
                m_OilbarrelCoordinate = new Coordinate(0, 1);
                m_OiltowerCoordinate = new Coordinate(0, 0);
            }
        }

        public static void InitializeWallsFromTiles()
        {
            for (int x = 0; x <= HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= VerticalTiles - 1; y++)
                {
                    Tile tile = Tiles[x, y];
                    m_HorizontalImages[x, y].Opacity = LEConstants.Transparent;
                    m_HorizontalImages[x, y + 1].Opacity = LEConstants.Transparent;
                    m_VerticalImages[x, y].Opacity = LEConstants.Transparent;
                    m_VerticalImages[x + 1, y].Opacity = LEConstants.Transparent;
                    if (tile.Top)
                    {
                        m_HorizontalImages[x, y].Opacity = LEConstants.Visible;
                    }
                    if (tile.Bottom)
                    {
                        m_HorizontalImages[x, y + 1].Opacity = LEConstants.Visible;
                    }
                    if (tile.Left)
                    {
                        m_VerticalImages[x, y].Opacity = LEConstants.Visible;
                    }
                    if (tile.Right)
                    {
                        m_VerticalImages[x + 1, y].Opacity = LEConstants.Visible;
                    }
                }
            }
        }

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
                    m_Tiles[x, y].Right = (m_VerticalImages[x + 1, y].Opacity == LEConstants.Visible);
                    m_Tiles[x, y].Top = (m_HorizontalImages[x, y].Opacity == LEConstants.Visible);
                    m_Tiles[x, y].Bottom = (m_HorizontalImages[x, y + 1].Opacity == LEConstants.Visible);
                }
            }
        }

        private static double m_SpeedFactor = 1.0;
        public static double SpeedFactor
        {
            get { return m_SpeedFactor; }
        }

        private static string m_LevelName = "Default";
        public static string LevelName
        {
            get { return m_LevelName; }
        }

        private static int m_LevelNumber = 1;
        public static int LevelNumber
        {
            get { return m_LevelNumber; }
        }

        private static int m_HorizontalTiles = 10;
        public static int HorizontalTiles
        {
            get { return m_HorizontalTiles; }
        }

        private static int m_VerticalTiles = 10;
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

        private static Coordinate m_FlowersackCoordinate;
        public static Coordinate FlowersackCoordinate
        {
            get { return m_FlowersackCoordinate; }
        }

        private static Coordinate m_OilbarrelCoordinate;
        public static Coordinate OilbarrelCoordinate
        {
            get { return m_OilbarrelCoordinate; }
        }

        private static Coordinate m_WheelbarrowCoordinate;
        public static Coordinate WheelbarrowCoordinate
        {
            get { return m_WheelbarrowCoordinate; }
        }

        private static Coordinate m_OiltowerCoordinate;
        public static Coordinate OiltowerCoordinate
        {
            get { return m_OiltowerCoordinate; }
        }

        private static List<Coordinate> m_Fires = new List<Coordinate>();
        public static List<Coordinate> Fires
        {
            get { return m_Fires; }
        }

        private static string m_BackgroundImageFullName = Path.Combine(Directory.GetCurrentDirectory(), "Backgrounds\\DefaultBackground.png");
        public static string BackgroundImageFullName
        {
            get { return m_BackgroundImageFullName; }
        }

        public static string BackgroundImageName
        {
            get { return Path.GetFileName(m_BackgroundImageFullName); }
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

        public static void LoadXmlFile(string file)
        {
            if (file == null)
            {
                m_FileLoaded = false;
                return;
            }

            if (!File.Exists(file))
            {
                m_FileLoaded = false;
                return;
            }

            XDocument doc = XDocument.Load(file);

            m_LevelNumber = Convert.ToInt32((from e in doc.Descendants("LevelInfo")
                                             select e.Attribute("Number").Value).First());

            m_LevelName = (from e in doc.Descendants("LevelInfo")
                           select e.Attribute("Name").Value).First();

            m_HorizontalTiles = Convert.ToInt32((from e in doc.Descendants("Columns")
                                                 select e.Value).First());

            m_VerticalTiles = Convert.ToInt32((from e in doc.Descendants("Rows")
                                               select e.Value).First());

            string backgroundFile = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Backgrounds"),
                (from e in doc.Descendants("Background") select e.Value).First());
            if (File.Exists(backgroundFile))
            {
                m_BackgroundImageFullName = backgroundFile;
            }

            m_FlowersackCoordinate = new Coordinate(
                Convert.ToInt32((from e in doc.Descendants("Flowersack")
                                 select e.Attribute("Column").Value).First()),
                Convert.ToInt32((from e in doc.Descendants("Flowersack")
                                 select e.Attribute("Row").Value).First()));

            m_OilbarrelCoordinate = new Coordinate(
                Convert.ToInt32((from e in doc.Descendants("Oilbarrel")
                                 select e.Attribute("Column").Value).First()),
                Convert.ToInt32((from e in doc.Descendants("Oilbarrel")
                                 select e.Attribute("Row").Value).First()));

            m_WheelbarrowCoordinate = new Coordinate(
                Convert.ToInt32((from e in doc.Descendants("Wheelbarrow")
                                 select e.Attribute("Column").Value).First()),
                Convert.ToInt32((from e in doc.Descendants("Wheelbarrow")
                                 select e.Attribute("Row").Value).First()));

            m_OiltowerCoordinate = new Coordinate(
                Convert.ToInt32((from e in doc.Descendants("Oiltower")
                                 select e.Attribute("Column").Value).First()),
                Convert.ToInt32((from e in doc.Descendants("Oiltower")
                                 select e.Attribute("Row").Value).First()));

            m_Fires.Clear();
            foreach (var elementFire in doc.Descendants("Fire"))
            {
                int xFire = Convert.ToInt32(elementFire.Attribute("Column").Value);
                int yFire = Convert.ToInt32(elementFire.Attribute("Row").Value);
                m_Fires.Add(new Coordinate(xFire, yFire));
            }

            var tiles =
                from e in doc.Descendants("Tile")
                select new
                {
                    Column = Convert.ToInt32(e.Attribute("Column").Value),
                    Row = Convert.ToInt32(e.Attribute("Row").Value),
                    Top = Convert.ToBoolean(e.Attribute("Top").Value),
                    Bottom = Convert.ToBoolean(e.Attribute("Bottom").Value),
                    Left = Convert.ToBoolean(e.Attribute("Left").Value),
                    Right = Convert.ToBoolean(e.Attribute("Right").Value)
                };

            m_Tiles = new Tile[(int)HorizontalTiles, (int)VerticalTiles];
            foreach (var tile in tiles)
            {
                Tiles[tile.Column, tile.Row] = new Tile();
                var levelTile = Tiles[tile.Column, tile.Row];
                levelTile.Bottom = tile.Bottom;
                levelTile.Top = tile.Top;
                levelTile.Left = tile.Left;
                levelTile.Right = tile.Right;
            }
            m_FileLoaded = true;
        }

        private static bool m_FileLoaded;
        public static bool FileLoaded
        {
            get { return m_FileLoaded; }
            set { m_FileLoaded = value; }
        }
    }
}

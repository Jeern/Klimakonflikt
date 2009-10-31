using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Linq;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MazeEditorPage.xaml
    /// </summary>
    public partial class MazeEditorPage : Page
    {
        public MazeEditorPage()
        {
            InitializeComponent();
            InitializeCanvas();
            InitializeGrid();
            InitializeImages();
            if (Maze.FileLoaded)
            {
                Maze.InitializeWallsFromTiles();
                MoveableImageController.Images.Add(new FlowerSackImage(MazeCanvas, Maze.FlowersackCoordinate));
                MoveableImageController.Images.Add(new OilBarrelImage(MazeCanvas, Maze.OilbarrelCoordinate));
                MoveableImageController.Images.Add(new OilTowerImage(MazeCanvas, Maze.OiltowerCoordinate));
                MoveableImageController.Images.Add(new WheelBarrowImage(MazeCanvas, Maze.WheelbarrowCoordinate));
                MoveableImageController.Images.Add((new FireImage(MazeCanvas, new Coordinate(-1, 1))));
                foreach (var fireCoordinate in Maze.Fires)
                {
                    MoveableImageController.Images.Add((new FireImage(MazeCanvas, fireCoordinate)));
                }
            }
            else
            {
                var flowerSackimage = new FlowerSackImage(MazeCanvas, new Coordinate((int)Maze.FlowersackCoordinate.X, (int)Maze.FlowersackCoordinate.Y));
                var oilBarrelImage = new OilBarrelImage(MazeCanvas, new Coordinate((int)Maze.OilbarrelCoordinate.X, (int)Maze.OilbarrelCoordinate.Y));
                var oilTowerImage = new OilTowerImage(MazeCanvas, new Coordinate((int)Maze.OiltowerCoordinate.X, (int)Maze.OiltowerCoordinate.Y));
                var wheelBarrowImage = new WheelBarrowImage(MazeCanvas, new Coordinate((int)Maze.WheelbarrowCoordinate.X, (int)Maze.WheelbarrowCoordinate.Y));
                MoveableImageController.Images.Add(flowerSackimage);
                MoveableImageController.Images.Add(oilBarrelImage);
                MoveableImageController.Images.Add(oilTowerImage);
                MoveableImageController.Images.Add(wheelBarrowImage);
                MoveableImageController.Images.Add((new FireImage(MazeCanvas, new Coordinate(-1, 1))));
            }
            UpdateRoundedImages();
        }

        private void InitializeCanvas()
        {
            MazeCanvas.Width = Maze.Width;
            MazeCanvas.Height = Maze.Height;
        }

        private void InitializeGrid()
        {
            MazeGrid.Width = Maze.Width;
            MazeGrid.Height = Maze.Height;
            CreateRowDefinitions(MazeGrid);
            CreateColumnDefinitions(MazeGrid);
        }

        private static void CreateRowDefinitions(Grid grid)
        {
            for (int row = 0; row < Maze.VerticalTiles; row++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = new GridLength(Maze.TileHeight);
                grid.RowDefinitions.Add(rowDef);
            }
        }

        private static void CreateColumnDefinitions(Grid grid)
        {
            for (int col = 0; col < Maze.HorizontalTiles; col++)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = new GridLength(Maze.TileWidth);
                grid.ColumnDefinitions.Add(colDef);
            }
        }

        private void InitializeImages()
        {
            InitializeRoundedImages();
            InitializeHorizontalImages();
            InitializeVerticalImages();
        }

        private void InitializeRoundedImages()
        {
            for (int x = 0; x <= (int)Maze.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles; y++)
                {
                    Maze.RoundedImages[x, y] = new RoundSpotImage(MazeCanvas, MazeGrid, x, y);
                }
            }
        }

        private void InitializeVerticalImages()
        {
            for (int x = 0; x <= (int)Maze.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles - 1; y++)
                {
                    var verticalImage = new VerticalWallImage(MazeCanvas, MazeGrid, x, y);
                    verticalImage.Changed += StaticImageChanged;
                    Maze.VerticalImages[x, y] = verticalImage;
                }
            }
        }

        private void InitializeHorizontalImages()
        {
            for (int x = 0; x <= (int)Maze.HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles; y++)
                {
                    var horizontalImage = new HorizontalWallImage(MazeCanvas, MazeGrid, x, y);
                    horizontalImage.Changed += StaticImageChanged;
                    Maze.HorizontalImages[x, y] = horizontalImage;
                }
            }
        }

        private void StaticImageChanged(object sender, EventArgs e)
        {
            UpdateRoundedImages();
        }

        private void SaveToPng(Canvas maze)
        {
            //Cool code borrowed from
            //http://dvuyka.spaces.live.com/blog/cns!305B02907E9BE19A!240.entry
            //Thank you...
            Transform transform = maze.LayoutTransform;
            maze.LayoutTransform = null;
            var size = new Size(maze.Width, maze.Height);
            Point pointMaze = maze.PointToScreen(new Point(0.0, 0.0)); //Point of maze relative to screen
            Point pointParent = (maze.Parent as Page).PointToScreen(new Point(0.0, 0.0));
            pointMaze.Offset(-pointParent.X, -pointParent.Y); //Point relative to parent of Maze
            maze.Measure(size);
            maze.Arrange(new Rect(size));
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(maze);

            using (FileStream fs = new FileStream(System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName + ".png"), FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(fs);
            }
            maze.LayoutTransform = transform;
            maze.Arrange(new Rect(pointMaze, size));
        }

        public void Save()
        {
            BeforeSave();
            SaveToPng(MazeCanvas);
            SaveToXml();
            AfterSave();
            MessageBox.Show(string.Format("Saved 2 files:\r\n{0}\r\n{1}", FullFileName + ".png", FullFileName + ".kklevel"), "KKLevel Editor", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveToXml()
        {
            var gameboard = new XElement("Gameboard");
            gameboard.Add(new XElement("LevelInfo", new XAttribute("Number", Maze.LevelNumber.ToString()), new XAttribute("Name", Maze.LevelName)));
            gameboard.Add(new XElement("Columns", Maze.HorizontalTiles));
            gameboard.Add(new XElement("Rows", Maze.VerticalTiles));
            gameboard.Add(new XElement("Image", FileName + ".png"));
            gameboard.Add(new XElement("Background", Maze.BackgroundImageName));
            var items = new XElement("Items");
            foreach (MoveableImage image in MoveableImageController.Images)
            {
                if (image.CurrentCoordinate.X >= 0 && image.CurrentCoordinate.Y >= 0)
                {
                    items.Add(new XElement(image.XmlName, new XAttribute("Column", image.XmlColumn), new XAttribute("Row", image.XmlRow)));
                }
            }
            gameboard.Add(items);
            var tiles = new XElement("Tiles");
            Maze.InitializeTilesFromWalls();
            for (int column = 0; column < Maze.HorizontalTiles; column++)
            {
                for (int row = 0; row < Maze.VerticalTiles; row++)
                {
                    var tile = new XElement("Tile",
                        new XAttribute("Row", row.ToString()),
                        new XAttribute("Column", column.ToString()),
                        new XAttribute("Top", Maze.Tiles[column, row].Top.ToString()),
                        new XAttribute("Bottom", Maze.Tiles[column, row].Bottom.ToString()),
                        new XAttribute("Left", Maze.Tiles[column, row].Left.ToString()),
                        new XAttribute("Right", Maze.Tiles[column, row].Right.ToString()));
                    tiles.Add(tile);
                }
            }
            gameboard.Add(tiles);
            gameboard.Save(FileName + ".kklevel");
        }

        private string FileName
        {
            get { return "Level" + Maze.LevelNumber.ToString() + Maze.LevelName.Replace(" ", ""); }
        }

        private string FullFileName
        {
            get { return System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName); }
        }

        private void UpdateRoundedImages()
        {
            //Make all transparent
            for (int x = 0; x <= (int)Maze.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles; y++)
                {
                    Maze.RoundedImages[x, y].Opacity = LEConstants.Transparent;
                }
            }

            //Make all adjacent to Horizontal Visible
            for (int x = 0; x <= (int)Maze.HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles; y++)
                {
                    if (Maze.HorizontalImages[x, y].Opacity == LEConstants.Visible)
                    {
                        Maze.RoundedImages[x, y].Opacity = LEConstants.Visible;
                        Maze.RoundedImages[x + 1, y].Opacity = LEConstants.Visible;
                    }
                }
            }

            //Make all adjacent to Vertical Visible
            for (int x = 0; x <= (int)Maze.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)Maze.VerticalTiles - 1; y++)
                {
                    if (Maze.VerticalImages[x, y].Opacity == LEConstants.Visible)
                    {
                        Maze.RoundedImages[x, y].Opacity = LEConstants.Visible;
                        Maze.RoundedImages[x, y + 1].Opacity = LEConstants.Visible;
                    }
                }
            }
        }

        private void AfterSave()
        {
            foreach (Image image in Maze.HorizontalImages)
            {
                AfterSaveImage(image);
            }
            foreach (Image image in Maze.VerticalImages)
            {
                AfterSaveImage(image);
            }
            foreach (Image image in Maze.RoundedImages)
            {
                AfterSaveImage(image);
            }
            foreach (Image image in MoveableImageController.Images)
            {
                image.Opacity = LEConstants.Visible;
            }
            MazeGrid.ShowGridLines = true;
        }

        private void BeforeSave()
        {
            foreach (Image image in Maze.HorizontalImages)
            {
                BeforeSaveImage(image);
            }
            foreach (Image image in Maze.VerticalImages)
            {
                BeforeSaveImage(image);
            }
            foreach (Image image in Maze.RoundedImages)
            {
                BeforeSaveImage(image);
            }
            foreach (Image image in MoveableImageController.Images)
            {
                image.Opacity = LEConstants.Invisible;
            }
            MazeGrid.ShowGridLines = false;
        }

        private static void BeforeSaveImage(Image image)
        {
            if (image.Opacity == LEConstants.Transparent)
            {
                image.Opacity = LEConstants.Invisible;
            }
        }

        private static void AfterSaveImage(Image image)
        {
            if (image.Opacity == LEConstants.Invisible)
            {
                image.Opacity = LEConstants.Transparent;
            }
        }


    }
}

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
            InitializeImages();
            UpdateRoundedImages();
            var image = new FlowerSackImage(MazeCanvas);
        }

        private Image[,] m_VerticalImages = new Image[(int)LEConstants.HorizontalTiles + 1, (int)LEConstants.VerticalTiles];
        private Image[,] m_HorizontalImages = new Image[(int)LEConstants.HorizontalTiles, (int)LEConstants.VerticalTiles + 1];
        private Image[,] m_RoundedImages = new Image[(int)LEConstants.HorizontalTiles + 1, (int)LEConstants.VerticalTiles + 1];

        private void InitializeImages()
        {
            InitializeRoundedImages();
            InitializeHorizontalImages();
            InitializeVerticalImages();
        }

        private void InitializeRoundedImages()
        {
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles; y++)
                {
                    m_RoundedImages[x, y] = new RoundSpotImage(MazeCanvas, MazeGrid, x, y);
                }
            }
        }



        private void InitializeVerticalImages()
        {
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles - 1; y++)
                {
                    var verticalImage = new VerticalWallImage(MazeCanvas, MazeGrid, x, y);
                    verticalImage.Changed += StaticImageChanged;
                    m_VerticalImages[x, y] = verticalImage;
                }
            }
        }

        private void InitializeHorizontalImages()
        {
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles; y++)
                {
                    var horizontalImage = new HorizontalWallImage(MazeCanvas, MazeGrid, x, y);
                    horizontalImage.Changed += StaticImageChanged;
                    m_HorizontalImages[x, y] = horizontalImage;
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

            using (FileStream fs = new FileStream(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "test.png"), FileMode.Create))
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
            AfterSave();
        }

        private void UpdateRoundedImages()
        {
            //Make all transparent
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles; y++)
                {
                    m_RoundedImages[x, y].Opacity = LEConstants.Transparent;
                }
            }

            //Make all adjacent to Horizontal Visible
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles; y++)
                {
                    if (m_HorizontalImages[x, y].Opacity == LEConstants.Visible)
                    {
                        m_RoundedImages[x, y].Opacity = LEConstants.Visible;
                        m_RoundedImages[x + 1, y].Opacity = LEConstants.Visible;
                    }
                }
            }

            //Make all adjacent to Vertical Visible
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles - 1; y++)
                {
                    if (m_VerticalImages[x, y].Opacity == LEConstants.Visible)
                    {
                        m_RoundedImages[x, y].Opacity = LEConstants.Visible;
                        m_RoundedImages[x, y + 1].Opacity = LEConstants.Visible;
                    }
                }
            }
        }

        private void AfterSave()
        {
            foreach (Image image in m_HorizontalImages)
            {
                AfterSaveImage(image);
            }
            foreach (Image image in m_VerticalImages)
            {
                AfterSaveImage(image);
            }
            foreach (Image image in m_RoundedImages)
            {
                AfterSaveImage(image);
            }
            MazeGrid.ShowGridLines = true;
        }

        private void BeforeSave()
        {
            foreach (Image image in m_HorizontalImages)
            {
                BeforeSaveImage(image);
            }
            foreach (Image image in m_VerticalImages)
            {
                BeforeSaveImage(image);
            }
            foreach (Image image in m_RoundedImages)
            {
                BeforeSaveImage(image);
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

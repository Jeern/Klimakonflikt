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
            var image = new MoveableImage(MazeCanvas, "FlowerSack.png");
        }

        private Image CreateRoundedImage(int x, int y)
        {
            Image image = CreateImage("mur-afrunding.png", false, false);
            image.Width = 4.0 * LEConstants.XMargin;
            image.Height = 4.0 * LEConstants.YMargin;

            double xOffset = 0.0;
            double yOffset = 0.0;

            if (y == (int)LEConstants.VerticalTiles)
            {
                Grid.SetRow(image, y - 1);
                image.VerticalAlignment = VerticalAlignment.Bottom;
                yOffset = 2.0 * LEConstants.YMargin;
            }
            else
            {
                Grid.SetRow(image, y);
                image.VerticalAlignment = VerticalAlignment.Top;
                yOffset = -2.0 * LEConstants.YMargin;
            }

            if (x == (int)LEConstants.HorizontalTiles)
            {
                Grid.SetColumn(image, x - 1);
                image.HorizontalAlignment = HorizontalAlignment.Right;
                xOffset = 2.0 * LEConstants.YMargin;
            }
            else
            {
                Grid.SetColumn(image, x);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                xOffset = -2.0 * LEConstants.YMargin;
            }
            image.RenderTransform = new TranslateTransform(xOffset, yOffset);
            return image;
        }

        private Image CreateHorizontalImage(int x, int y)
        {
            Image image = CreateImage("Mur horizontal.png", true, (y == 0 | y == LEConstants.VerticalTiles));
            image.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(image, x);

            if (y == (int)LEConstants.VerticalTiles)
            {
                Grid.SetRow(image, y - 1);
                image.VerticalAlignment = VerticalAlignment.Bottom;
                image.RenderTransform = new TranslateTransform(0.0, 2.0 * LEConstants.YMargin);
            }
            else
            {
                Grid.SetRow(image, y);
                image.VerticalAlignment = VerticalAlignment.Top;
                image.RenderTransform = new TranslateTransform(0.0, -2.0 * LEConstants.YMargin);
            }
            return image;
        }

        private Image CreateVerticalImage(int x, int y)
        {
            Image image = CreateImage("Mur vertical.png", true, (x == 0 | x == LEConstants.HorizontalTiles));
            image.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(image, y);

            if (x == (int)LEConstants.HorizontalTiles)
            {
                Grid.SetColumn(image, x - 1);
                image.HorizontalAlignment = HorizontalAlignment.Right;
                image.RenderTransform = new TranslateTransform(2.0 * LEConstants.XMargin, 0.0);
            }
            else
            {
                Grid.SetColumn(image, x);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.RenderTransform = new TranslateTransform(-2.0 * LEConstants.XMargin, 0.0);
            }
            return image;
        }

        private Image CreateImage(string name, bool checkMouseButton, bool visible)
        {
            Image image = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(name, UriKind.Relative);
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
            image.Visibility = Visibility.Visible;
            if (visible)
            {
                image.Opacity = LEConstants.Visible;
            }
            else
            {
                image.Opacity = LEConstants.Transparent;
            }
            image.IsHitTestVisible = true;
            if (checkMouseButton)
            {
                image.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            }
            MazeGrid.Children.Add(image);
            return image;
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Opacity = ReverseVisibility(image.Opacity);
                UpdateRoundedImages();
            }
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
                    m_RoundedImages[x, y] = CreateRoundedImage(x, y);
                }
            }
        }



        private void InitializeVerticalImages()
        {
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles - 1; y++)
                {
                    m_VerticalImages[x, y] = CreateVerticalImage(x, y);
                }
            }
        }

        private void InitializeHorizontalImages()
        {
            for (int x = 0; x <= (int)LEConstants.HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)LEConstants.VerticalTiles; y++)
                {
                    m_HorizontalImages[x, y] = CreateHorizontalImage(x, y);
                }
            }
        }

        private double ReverseVisibility(double opacity)
        {
            if (opacity == LEConstants.Visible)
                return LEConstants.Transparent;
            else
                return LEConstants.Visible;
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

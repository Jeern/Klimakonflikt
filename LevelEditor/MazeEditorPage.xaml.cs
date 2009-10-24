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
            CreateMovableImage("FlowerSack.png");
        }

        private Image CreateRoundedImage(int x, int y)
        {
            Image image = CreateImage("mur-afrunding.png", false, false);
            image.Width = 4.0 * XMargin;
            image.Height = 4.0 * YMargin;

            double xOffset = 0.0;
            double yOffset = 0.0;

            if (y == (int)VerticalTiles)
            {
                Grid.SetRow(image, y - 1);
                image.VerticalAlignment = VerticalAlignment.Bottom;
                yOffset = 2.0 * YMargin;
            }
            else
            {
                Grid.SetRow(image, y);
                image.VerticalAlignment = VerticalAlignment.Top;
                yOffset = -2.0 * YMargin;
            }

            if (x == (int)HorizontalTiles)
            {
                Grid.SetColumn(image, x - 1);
                image.HorizontalAlignment = HorizontalAlignment.Right;
                xOffset = 2.0 * YMargin;
            }
            else
            {
                Grid.SetColumn(image, x);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                xOffset = -2.0 * YMargin;
            }
            image.RenderTransform = new TranslateTransform(xOffset, yOffset);
            return image;
        }

        private Image CreateHorizontalImage(int x, int y)
        {
            Image image = CreateImage("Mur horizontal.png", true, (y == 0 | y == VerticalTiles));
            image.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(image, x);

            if (y == (int)VerticalTiles)
            {
                Grid.SetRow(image, y - 1);
                image.VerticalAlignment = VerticalAlignment.Bottom;
                image.RenderTransform = new TranslateTransform(0.0, 2.0 * YMargin);
            }
            else
            {
                Grid.SetRow(image, y);
                image.VerticalAlignment = VerticalAlignment.Top;
                image.RenderTransform = new TranslateTransform(0.0, -2.0 * YMargin);
            }
            return image;
        }

        private Image CreateVerticalImage(int x, int y)
        {
            Image image = CreateImage("Mur vertical.png", true, (x == 0 | x == HorizontalTiles));
            image.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(image, y);

            if (x == (int)HorizontalTiles)
            {
                Grid.SetColumn(image, x - 1);
                image.HorizontalAlignment = HorizontalAlignment.Right;
                image.RenderTransform = new TranslateTransform(2.0 * XMargin, 0.0);
            }
            else
            {
                Grid.SetColumn(image, x);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.RenderTransform = new TranslateTransform(-2.0 * XMargin, 0.0);
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
                image.Opacity = Visible;
            }
            else
            {
                image.Opacity = Transparent;
            }
            image.IsHitTestVisible = true;
            if (checkMouseButton)
            {
                image.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            }
            MazeGrid.Children.Add(image);
            return image;
        }

        private Image CreateMovableImage(string name)
        {
            Image image = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(name, UriKind.Relative);
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
            image.Visibility = Visibility.Visible;
            image.Opacity = Visible;
            image.IsHitTestVisible = true;
//            image.MouseMove += MoveImage;
            image.MouseLeftButtonDown += new MouseButtonEventHandler(image_MouseLeftButtonDown);
            MazeCanvas.Children.Add(image);
            return image;
        }

        void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(MazeCanvas);
                Canvas.SetLeft(image, p.X);
                Canvas.SetTop(image, p.Y);
            }
        }

        private void MoveImage(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
            if (image != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(MazeCanvas);
                Canvas.SetLeft(image, p.X);
                Canvas.SetTop(image, p.Y);
            }
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

        private const double XMargin = 3.0;
        private const double YMargin = 3.0;
        private const double VerticalTiles = 10.0;
        private const double HorizontalTiles = 10.0;
        private const double Invisible = 0.0;
        private const double Transparent = 0.1;
        private const double Visible = 1.0;

        private Image[,] m_VerticalImages = new Image[(int)HorizontalTiles + 1, (int)VerticalTiles];
        private Image[,] m_HorizontalImages = new Image[(int)HorizontalTiles, (int)VerticalTiles + 1];
        private Image[,] m_RoundedImages = new Image[(int)HorizontalTiles + 1, (int)VerticalTiles + 1];

        private void InitializeImages()
        {
            InitializeRoundedImages();
            InitializeHorizontalImages();
            InitializeVerticalImages();
        }

        private void InitializeRoundedImages()
        {
            for (int x = 0; x <= (int)HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles; y++)
                {
                    m_RoundedImages[x, y] = CreateRoundedImage(x, y);
                }
            }
        }



        private void InitializeVerticalImages()
        {
            for (int x = 0; x <= (int)HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles - 1; y++)
                {
                    m_VerticalImages[x, y] = CreateVerticalImage(x, y);
                }
            }
        }

        private void InitializeHorizontalImages()
        {
            for (int x = 0; x <= (int)HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles; y++)
                {
                    m_HorizontalImages[x, y] = CreateHorizontalImage(x, y);
                }
            }
        }

        private double ReverseVisibility(double opacity)
        {
            if (opacity == Visible)
                return Transparent;
            else
                return Visible;
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
            for (int x = 0; x <= (int)HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles; y++)
                {
                    m_RoundedImages[x, y].Opacity = Transparent;
                }
            }

            //Make all adjacent to Horizontal Visible
            for (int x = 0; x <= (int)HorizontalTiles - 1; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles; y++)
                {
                    if (m_HorizontalImages[x, y].Opacity == Visible)
                    {
                        m_RoundedImages[x, y].Opacity = Visible;
                        m_RoundedImages[x + 1, y].Opacity = Visible;
                    }
                }
            }

            //Make all adjacent to Vertical Visible
            for (int x = 0; x <= (int)HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles - 1; y++)
                {
                    if (m_VerticalImages[x, y].Opacity == Visible)
                    {
                        m_RoundedImages[x, y].Opacity = Visible;
                        m_RoundedImages[x, y + 1].Opacity = Visible;
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
            if (image.Opacity == Transparent)
            {
                image.Opacity = Invisible;
            }
        }

        private static void AfterSaveImage(Image image)
        {
            if (image.Opacity == Invisible)
            {
                image.Opacity = Transparent;
            }
        }


    }
}

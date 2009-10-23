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
using System.Diagnostics;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            InitializeComponent();
            InitializeImages();
        }

        private Image CreateHorizontalImage(int x, int y)
        {
            Image image = CreateImage("Mur horizontal.png");
            image.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(image, x);

            if (y == (int)VerticalTiles)
            {
                Grid.SetRow(image, y-1);
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
            Image image = CreateImage("Mur vertical.png");
            image.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(image, y);

            if (x == (int)HorizontalTiles)
            {
                Grid.SetColumn(image, x-1);
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

        private Image CreateImage(string name)
        {
            Image image = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(name, UriKind.Relative);
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
            image.Visibility = Visibility.Visible;
            image.Opacity = 0.1;
            image.IsHitTestVisible = true;
            image.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            MazeGrid.Children.Add(image);
            return image;
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Opacity = ReverseVisibility(image.Opacity);
            }
        }
        
        private const double XMargin = 3.0;
        private const double YMargin = 3.0;
        private const double VerticalTiles = 10.0;
        private const double HorizontalTiles = 10.0;

        private Image[,] m_VerticalImages = new Image[(int)HorizontalTiles + 1, (int)VerticalTiles];
        private Image[,] m_HorizontalImages = new Image[(int)HorizontalTiles, (int)VerticalTiles + 1];

        private void InitializeImages()
        {
            InitializeHorizontalImages();
            InitializeVerticalImages();
        }

        private void InitializeVerticalImages()
        {
            for (int x = 0; x <= (int)HorizontalTiles; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles-1; y++)
                {
                    m_VerticalImages[x, y] = CreateVerticalImage(x, y);
                }
            }
        }

        private void InitializeHorizontalImages()
        {
            for (int x = 0; x <= (int)HorizontalTiles-1; x++)
            {
                for (int y = 0; y <= (int)VerticalTiles; y++)
                {
                    m_HorizontalImages[x, y] = CreateHorizontalImage(x, y);
                }
            }
        }
        
        private double ReverseVisibility(double opacity)
        {
            if (opacity == 1.0)
                return 0.1;
            else
                return 1.0;
        }
    }
}

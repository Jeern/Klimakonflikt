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
using LevelEditor.Core.Validators;
using LevelEditor.Core.MazeItems;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for LevelInfoEditorPage.xaml
    /// </summary>
    public partial class LevelInfoEditorPage : Page
    {
        public LevelInfoEditorPage()
        {
            InitializeComponent();
            NumberTextbox.Validator = new IntegerValidator("Level Number");
            NameTextbox.Validator = new LevelNameValidator("Level Name");
            ColumnsTextbox.Validator = new TilesValidator("Columns");
            RowsTextbox.Validator = new TilesValidator("Rows");
            SpeedFactorTextbox.Validator = new DoubleValidator("Speed factor");
            NumberTextbox.Text = Maze.LevelNumber.ToString();
            NameTextbox.Text = Maze.LevelName;
            ColumnsTextbox.Text = Maze.HorizontalTiles.ToString();
            RowsTextbox.Text = Maze.VerticalTiles.ToString();
            SpeedFactorTextbox.Text = Maze.SpeedFactor.ToString();

            ColumnsTextbox.Changed += TextChanged;
            RowsTextbox.Changed += TextChanged;
             
            BackgroundCombobox.SelectedValue = Maze.BackgroundImageFullName;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Maze.FileLoaded = false;
        }

        public int LevelNumber
        {
            get { return Convert.ToInt32(NumberTextbox.Text); }
        }

        public string LevelName
        {
            get { return NameTextbox.Text; }
        }

        public int Columns
        {
            get { return Convert.ToInt32(ColumnsTextbox.Text); }
        }

        public int Rows
        {
            get { return Convert.ToInt32(RowsTextbox.Text); }
        }

        public double SpeedFactor
        {
            get { return Convert.ToDouble(SpeedFactorTextbox.Text); }
        }

        public string BackgroundImageFullName
        {
            get { return BackgroundCombobox.SelectedValue; }
        }

        public bool Validate()
        {
            return
                NumberTextbox.Validate() &
                NameTextbox.Validate() &
                ColumnsTextbox.Validate() &
                RowsTextbox.Validate() &
                SpeedFactorTextbox.Validate();
        }
    }
}

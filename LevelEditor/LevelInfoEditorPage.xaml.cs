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

    }
}

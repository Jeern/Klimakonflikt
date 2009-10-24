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
    /// Interaction logic for LETextBox.xaml
    /// </summary>
    public partial class LETextBox : UserControl
    {
        public LETextBox()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return LELabel.Content as string; }
            set { LELabel.Content = value; }
        }

        public string Text
        {
            get { return LEText.Text; }
            set { LEText.Text = value; }
        }
    }
}

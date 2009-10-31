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
using System.Collections;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for LEComboBox.xaml
    /// </summary>
    public partial class LEComboBox : UserControl
    {
        public LEComboBox()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return LELabel.Content as string; }
            set { LELabel.Content = value; }
        }

        public string SelectedValue
        {
            get { return LECombo.SelectedValue as string; }
            set { LECombo.SelectedValue = value; }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LEComboBox));
        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(LEComboBox));
        public static readonly DependencyProperty SelectedValuePathProperty = DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(LEComboBox));
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(LEComboBox));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(LEComboBox));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(LEComboBox.ItemsSourceProperty); }
            set { SetValue(LEComboBox.ItemsSourceProperty, value); }
        }

        public string DisplayMemberPath
        {
            get { return (string)GetValue(LEComboBox.DisplayMemberPathProperty); }
            set { SetValue(LEComboBox.DisplayMemberPathProperty, value); }
        }

        public string SelectedValuePath
        {
            get { return (string)GetValue(LEComboBox.SelectedValuePathProperty); }
            set { SetValue(LEComboBox.SelectedValuePathProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(LEComboBox.SelectedIndexProperty); }
            set { SetValue(LEComboBox.SelectedIndexProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(LEComboBox.SelectedItemProperty); }
            set { SetValue(LEComboBox.SelectedItemProperty, value); }
        }
    }
}

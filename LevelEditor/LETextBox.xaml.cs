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
    public partial class LETextBox : UserControl, IValidator
    {
        private Validator m_Validator;
        public LETextBox()
        {
            InitializeComponent();
        }

        public Validator Validator
        {
            get { return m_Validator; }
            set { m_Validator = value; }
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

        public bool Validate()
        {
            return Validate(Text);
        }

        #region IValidator Members

        public bool Validate(string item)
        {
            if (m_Validator == null)
                return true;
            return m_Validator.Validate(item);
        }

        #endregion
    }
}

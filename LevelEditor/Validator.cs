using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LevelEditor
{
    public abstract class Validator : IValidator
    {
        private string m_ItemText;

        public Validator(string itemText)
        {
            m_ItemText = itemText;
        }

        public virtual bool Validate(string item)
        {
            if (!IsValid(item))
            {
                MessageBox.Show(string.Format("{0} {1}", m_ItemText, ErrorString), "LevelEditor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        protected abstract bool IsValid(string item);

        protected abstract string ErrorString { get; }
        
    }
}

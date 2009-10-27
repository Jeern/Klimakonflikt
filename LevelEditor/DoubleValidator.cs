using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class DoubleValidator : Validator
    {
        public DoubleValidator(string itemText) : base(itemText)  { }

        protected override bool IsValid(string item)
        {
            double result;
            return double.TryParse(item, out result);
        }

        protected override string ErrorString
        {
            get { return "has to be a decimal number"; }
        }
    }
}

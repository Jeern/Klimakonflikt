using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor.Core.Validators
{
    public class IntegerValidator : Validator
    {
        public IntegerValidator(string itemText) : base(itemText) { }
        protected override bool IsValid(string item)
        {
            int result;
            return int.TryParse(item, out result);
        }

        protected override string ErrorString
        {
            get { return "has to be an integer"; }
        }
    }
}

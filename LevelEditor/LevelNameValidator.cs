using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class LevelNameValidator : Validator
    {
        public LevelNameValidator(string itemText) : base(itemText) { }
        protected override bool IsValid(string item)
        {
            return item.Trim().Length >= 3;
        }

        protected override string ErrorString
        {
            get { return "has to be at least 3 characters long"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class TilesValidator : Validator
    {
        public TilesValidator(string itemText) : base(itemText) { }
        protected override bool IsValid(string item)
        {
            int result;
            int.TryParse(item, out result);
            return result >= 3 & result <= 10;
        }

        protected override string ErrorString
        {
            get { return "has to be between 3 and 10"; }
        }

    }
}

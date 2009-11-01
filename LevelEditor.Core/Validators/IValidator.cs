using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor.Core.Validators
{
    public interface IValidator
    {
        bool Validate(string item);
    }
}

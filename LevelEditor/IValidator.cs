using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public interface IValidator
    {
        bool Validate(string item);
    }
}

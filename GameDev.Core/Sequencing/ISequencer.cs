using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public interface ISequencer
    {
        int Current { get; }
        void MoveNext();
    }
}
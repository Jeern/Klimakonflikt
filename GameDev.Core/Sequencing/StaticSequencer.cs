using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public class StaticSequencer : Sequencer
    {
        public StaticSequencer()
            : this(0)
        {
        }

        public StaticSequencer(int value)
            : base(value)
        {
            Current = value;
        }

        public override bool MoveNext()
        {
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public class StaticSequencer : Sequencer
    {
        public StaticSequencer(int value) : base(value)
        {
            Current = value;
        }

        public override void MoveNext()
        {
            //Do nothing
        }
    }
}

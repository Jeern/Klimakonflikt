using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public class ForwardingSequencer : Sequencer
    {
        public ForwardingSequencer(int maxValue) : this(0, maxValue) {}

        public ForwardingSequencer(int minValue, int maxValue) : base(minValue, maxValue)
        {
            Current = minValue;  
        }

        public override bool MoveNext()
        {
            if (Current < MaxValue)
            {
                Current++;
                return true;
            }
            return false;
        }
    }
}

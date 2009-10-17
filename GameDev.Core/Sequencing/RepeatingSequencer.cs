using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public class RepeatingSequencer : Sequencer
    {
        public RepeatingSequencer(int maxValue) : this(0, maxValue) {}

        public RepeatingSequencer(int minValue, int maxValue)
            : base(minValue, maxValue)
        {
            Current = minValue;  
        }

        public override void MoveNext()
        {
            if (Current < MaxValue)
            {
                Current++;
            }
            else
            {
                Current = MinValue;
            }
        }
    }
}

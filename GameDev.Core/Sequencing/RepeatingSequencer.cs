using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    /// <summary>
    /// Repeats sequence again and again. Don't use foreach
    /// </summary>
    public class RepeatingSequencer : Sequencer
    {
        public RepeatingSequencer(int maxValue) : this(0, maxValue) {}

        public RepeatingSequencer(int minValue, int maxValue)
            : base(minValue, maxValue)
        {
        }

        public override bool MoveNext()
        {
            if (Current < MaxValue)
            {
                Current++;
            }
            else
            {
                Current = MinValue;
            }
            return true; 
        }
    }
}

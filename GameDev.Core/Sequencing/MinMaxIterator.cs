using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public class MinMaxIterator : SequencedIterator<int>
    {
        public MinMaxIterator(Sequencer sequencer, int min, int max) : base(sequencer, new SequenceCreator().GetMinMax(min, max).ToList())
        {

        }

    }
}

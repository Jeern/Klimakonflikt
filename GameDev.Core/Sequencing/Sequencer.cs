using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    public abstract class Sequencer : ISequencer
    {
        protected int MinValue { get; set; }
        protected int MaxValue { get; set; }

        public Sequencer(int minValue, int maxValue)
        {
            if(minValue >= maxValue && maxValue > 0)
            throw new ArgumentException(SequencerErrorMessage(), "minValue");

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public Sequencer(int maxValue) : this(0, maxValue) { }


        #region ISequencer Members

        public int Current
        {
            get;
            protected set;
        }

        public abstract void MoveNext();

        protected string SequencerErrorMessage()
        {
            return string.Format("minValue: {0} must be smaller than maxValue: {1} in {2}", MinValue, MaxValue, this.GetType().Name);
        }

        #endregion
    }
}

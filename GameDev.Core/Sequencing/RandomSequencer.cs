﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Sequencing
{
    /// <summary>
    /// Don't use foreach the random sequence never stops
    /// </summary>
    public class RandomSequencer : Sequencer
    {
        private RealRandom m_Random;
        public RandomSequencer(int maxValue) : this(0, maxValue) {}

        public RandomSequencer(int minValue, int maxValue)
            : base(minValue, maxValue)
        {
        }

        public override bool MoveNext()
        {
            if(m_Random == null)
                m_Random = new RealRandom(MinValue, MaxValue);
            Current = m_Random.Next();
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core
{
    public class SequenceCreator
    {
        private int index;

        public IEnumerable<int> GetMinMax(int min, int max)
        {
            if (index < min)
            {
                index = min;
            }
            else if (index < max)
            {
                index++;
                yield return index;
            }
            else
            {
                yield break;
            }

        }

    }
}

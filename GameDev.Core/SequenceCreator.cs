using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core
{
    public class SequenceCreator
    {
        public List<int> GetMinMax(int min, int max)
        {
            return GetMinMaxInternal(min, max).ToList();
        }

        private IEnumerable<int> GetMinMaxInternal(int min, int max)
        {
            for (int index = min; index <= max; index++)
            {
                yield return index;
            }
        }

    }
}

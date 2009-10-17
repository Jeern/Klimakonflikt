using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SudokuBackEnd
{
    public class RandomStream
    {
        private Random m_Random;
        private List<int> m_ListOfNumbers;

        public RandomStream(int min, int max)
        {

            m_Random = new Random(GetSeed());
            m_ListOfNumbers = new List<int>(max-min);
            for (int i = min; i <= max; i++)
            {
                m_ListOfNumbers.Add(i);
            }
        }

        public int Next()
        {
            if (m_ListOfNumbers == null || m_ListOfNumbers.Count() == 0)
                throw new ArgumentOutOfRangeException();

            int index = m_Random.Next(m_ListOfNumbers.Count());
            int number = m_ListOfNumbers[index];
            m_ListOfNumbers.RemoveAt(index);
            return number;
        }

        private int GetSeed()
        {
            var bytes = new byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return bytes[0] * bytes[1] * bytes[2] * bytes[3];
        }

    }
}

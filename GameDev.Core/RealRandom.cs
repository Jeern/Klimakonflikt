using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GameDev.Core
{
    public class RealRandom
    {
        private Random m_Random;
        private int m_Min;
        private int m_Max;

        public RealRandom(int min, int max)
        {
            m_Random = new Random(GetSeed());
        }

        public int Next()
        {
            return m_Random.Next(m_Min, m_Max);
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

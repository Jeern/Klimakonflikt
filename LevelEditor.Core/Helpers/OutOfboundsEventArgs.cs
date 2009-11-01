using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor.Core.Helpers
{
    public class OutOfboundsEventArgs : EventArgs
    {
        public OutOfboundsEventArgs(int x, int y, int maxX, int maxY)
        {
            m_X = x;
            m_Y = y;
            m_MaxX = maxX;
            m_MaxY = maxY;
        }

        private int m_X;
        public int X
        {
            get { return m_X; }
        }

        private int m_Y;
        public int Y
        {
            get { return m_Y; }
        }

        private int m_MaxX;
        public int MaxX
        {
            get { return m_MaxX; }
        }

        private int m_MaxY;
        public int MaxY
        {
            get { return m_MaxY; }
        }
    }
}

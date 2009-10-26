using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public static class MoveableImageController
    {
        private static List<MoveableImage> m_Images = new List<MoveableImage>();
        public static List<MoveableImage> Images
        {
            get { return m_Images; }
        }
    }
}

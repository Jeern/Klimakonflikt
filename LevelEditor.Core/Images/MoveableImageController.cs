using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor.Core.Images
{
    public static class MoveableImageController
    {
        private static List<MoveableImage> m_Images = new List<MoveableImage>();
        public static List<MoveableImage> Images
        {
            get { return m_Images; }
        }

        public static bool ImageIsMoving
        {
            get
            {
                foreach (MoveableImage image in m_Images)
                {
                    if (image.IsMoving)
                        return true;
                }
                return false;
            }
        }
    }
}

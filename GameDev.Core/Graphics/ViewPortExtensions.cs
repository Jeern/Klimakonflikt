#region Usings

using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GameDev.Core.Graphics
{

    public static  class ViewPortExtensions
    {
        public static int GetLeft(this Viewport viewPort)
        {
            return viewPort.X + viewPort.Width;
        }

        public static int GetBottom(this Viewport viewPort)
        {
            return viewPort.Y + viewPort.Height;
        }

    }
}

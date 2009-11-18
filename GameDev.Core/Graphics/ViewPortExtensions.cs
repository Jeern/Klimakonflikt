#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GameDev.Core.Graphics
{

    public static  class ViewPortExtensions
    {
        public static int GetRight(this Viewport viewPort)
        {
            return viewPort.X + viewPort.Width;
        }

        public static int GetBottom(this Viewport viewPort)
        {
            return viewPort.Y + viewPort.Height;
        }

        public static Vector2 GetCenter(this Viewport viewPort)
        {

            return new Vector2((viewPort.X + viewPort.Width) / 2, (viewPort.Y + viewPort.Height) / 2);
        }

    }
}

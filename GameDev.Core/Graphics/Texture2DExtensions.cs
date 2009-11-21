using System;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Graphics
{
    public static class Texture2DExtensions
    {

        public static Vector2 GetSize(this Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.Graphics
{
    public static class SpriteBatchExtensions
    {

        public static void DrawRectangle(this SpriteBatch batch, Rectangle rect, Color color)
        {
            batch.Draw(BaseTextures.Square_128x128, rect, color);
        }
    }
}

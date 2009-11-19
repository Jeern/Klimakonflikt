using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.Graphics
{
    public class GameDevTexture
    { 
        public Texture2D Texture
        { 
            get; 
            set; 
        }

        public static implicit operator GameDevTexture(Texture2D texture)
        {
            var gdTexture = new GameDevTexture();
            gdTexture.Texture = texture;
            return gdTexture;
        }

        public static implicit operator Texture2D(GameDevTexture texture)
        {
            return texture.Texture;
        }
    }
}

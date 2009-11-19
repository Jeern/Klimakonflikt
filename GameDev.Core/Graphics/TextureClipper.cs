using System;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.Graphics
{
    public class TextureClipper
    {

        public Texture2D[,] Textures { get; set; }


        public TextureClipper(Texture2D bigTexture, int tileSize, GraphicsDevice device)
        {
            if (bigTexture.Width % tileSize != 0 || bigTexture.Height % tileSize != 0)
            {
                throw new ArgumentException("The texture passed to the constructor is not an even split into squares of size " + tileSize + " x " + tileSize + "!");
            }

            int TilesHorizontally = bigTexture.Width / tileSize;
            int tilesDown = bigTexture.Height / tileSize;

            for (int x = 0; x < TilesHorizontally; x++)
            {
                for (int y = 0; y < tilesDown; y++)
                {
                    Texture2D texture = new Texture2D(device, tileSize, tileSize);
                    //texture.
                    //Textures[x, y] = texture;
                    
                }
            }
        }

    }
}

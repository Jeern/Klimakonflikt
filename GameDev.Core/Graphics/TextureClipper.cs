using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using GameDev.Core;

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

            int tilesWide = bigTexture.Width / tileSize;
            int tilesDown = bigTexture.Height / tileSize;

            for (int x = 0; x < tilesWide; x++)
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

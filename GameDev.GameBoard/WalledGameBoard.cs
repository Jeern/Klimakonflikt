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

namespace GameDev.GameBoard
{


    public class WalledGameBoard : GameBoard
    {

        public WalledGameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels)
            : base(game, baseTileTexture, spriteBatch, name, tilesHorizontally, tilesVertically, tileSizeInPixels)
        {
        }
    }
}

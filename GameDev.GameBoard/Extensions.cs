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
using GameDev.Core.Graphics;

namespace GameDev.GameBoard
{
    public static class Extensions
    {

        public static List<WalledTile> GetWalledTiles(this List<Tile> tiles)
        {
            List<WalledTile> wallTiles = new List<WalledTile>();
            foreach (var tile in tiles)
            {
                if (tile is WalledTile)
                {
                    wallTiles.Add((WalledTile)tile);
                }
            }
            return wallTiles;
        }

    }
}

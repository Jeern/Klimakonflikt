//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

//using GameDev.Core;

//namespace GameDev.GameBoard
//{
//    public class WalledGameBoard : GameBoard<WalledTile>
//    {

//        public WalledGameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch) : this(game, baseTileTexture, spriteBatch, "Unnamed", 10, 10, 64) { }

//        public WalledGameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels): base(game, baseTileTexture, spriteBatch, name, tilesHorizontally, tilesVertically, tileSizeInPixels)
//        {
//        }

//        public override void CreateTiles()
//        {
//            Tiles = new WalledTile[TilesHorizontally, TilesVertically];
//            for (int x = 0; x < TilesHorizontally; x++)
//            {
//                for (int y = 0; y < TilesVertically; y++)
//                {
//                    Tiles[x, y] = new WalledTile(Game, this, BaseTileTexture, SpriteBatch, x, y);
//                }
//            }

//        }

//    }
//}

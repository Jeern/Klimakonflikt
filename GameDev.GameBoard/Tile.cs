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
    public class Tile : DrawableGameComponent, GameDev.Core.IPlaceable
    {
       
        public GameBoard GameBoard { get; set; }
        
        public bool HasBorderTop { get; set; }
        public bool HasBorderLeft { get; set; }
        public bool HasBorderBottom { get; set; }
        public bool HasBorderRight { get; set; }

        public bool HasBorder(Direction4 direction) 
        {
            return GameBoard.ContainsPosition(this.Offset(direction));
        }

        public int Width { get {return this.Texture.Width; }}
        public int Height { get { return this.Texture.Height; } }

        public int TileIndexHorizontally { get; private set; }
        public int TileIndexVertically { get; private set; }

        

        public Texture2D Texture { get; set; }

        public Tile(Game game, GameBoard board, Texture2D texture): base (game)
        {
            this.GameBoard = board;
            this.Texture = texture;
        }





        #region IPlaceable Members
        
        public int X { get; set; }
        public int Y { get; set; }
        
        #endregion
    }
}

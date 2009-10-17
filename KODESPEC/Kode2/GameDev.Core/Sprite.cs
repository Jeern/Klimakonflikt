using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace GameDev.Core
{
    //mangler Image. 

    public class Sprite : Placeable
    {
        protected Direction spriteDirection;
        protected Texture2D spriteTexture;
        protected const float speed = 10.0F;

        // Lidt i tvivl om hvorledes Textture-sizen sættes til texturefilen's size
        public Sprite(Game game, string fileSprite,float speed) : base(game)
        {
            this.spriteTexture = new Texture2D(Game.GraphicsDevice, 100, 100);
            //sætter den til left for at undgå compiler never used warning
            spriteDirection = Direction.Left;
        }

        public Sprite(Game game, string fileSprite, float speed, int x, int y)
            : base(game, x, y)
        {
        }
    }
}

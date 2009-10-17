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
using GameDev.Core.Graphics;



namespace KlimaKonflikt
{
    public class KKPlayer : Sprite
    {
        public Direction WantedDirection { get; set; }
        public KKPlayer(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed):base(game, gameImage, spriteBatch, speed)
        {

        }

    }
}

using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameDev.Core;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;

namespace KlimaKonflikt
{
    public class KKMonster : Sprite
    {
        public Direction WantedDirection { get; set; }
        public KKMonster(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed, Point startingPosition, int ammo)
            : base(game, gameImage, spriteBatch, speed, startingPosition)
        {
        }
    }        
}

#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics.Silverlight;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Control
{
    public interface IController
    {
        void Update(GameTime gameTime, Sprite controllee);
    }
}

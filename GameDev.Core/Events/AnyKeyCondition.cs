#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Input;
#else
using Microsoft.Xna.Framework.Input;
#endif

namespace GameDev.Core.Events
{
  public  class AnyKeyCondition : Condition
    {
        public override bool IsCurrentlyFulfilled()
        {
            return (Keyboard.GetState().GetPressedKeys().Length > 0);
        }
    }
}

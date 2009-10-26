using System;

using Microsoft.Xna.Framework.Input;

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

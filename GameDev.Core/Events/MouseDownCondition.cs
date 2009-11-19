using System.Collections.Generic;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework.Input;
#endif

namespace GameDev.Core.Events
{
    public class MouseDownCondition : Condition
    {

        public List<Buttons> TriggerButtons { get; set; }
        public MouseState MouseState { get; private set; }
        public MouseDownCondition(List<Buttons> triggerButtons)
        {
            TriggerButtons = TriggerButtons;
        }

        public override bool IsCurrentlyFulfilled()
        {
            MouseState = Mouse.GetState();
            return (MouseState.LeftButton == ButtonState.Pressed
                || MouseState.MiddleButton == ButtonState.Pressed
                || MouseState.RightButton == ButtonState.Pressed);
        }

    }

}

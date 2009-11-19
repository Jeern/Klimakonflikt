using System.Collections.Generic;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Input;
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
#if SILVERLIGHT
            return (MouseState.LeftButton == ButtonState.Pressed
                || MouseState.RightButton == ButtonState.Pressed);
#else
            return (MouseState.LeftButton == ButtonState.Pressed
                || MouseState.MiddleButton == ButtonState.Pressed
                || MouseState.RightButton == ButtonState.Pressed);
#endif
        }

    }

}

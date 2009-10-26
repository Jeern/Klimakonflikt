using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameDev.Core.Events
{
    public class KeysDownCondition : Condition
    {

        public List<Keys> TriggerKeys { get; set; }
        public KeyboardState KeyboardState { get; private set; }
        public KeysDownCondition(List<Keys> triggerKeys)
        {
            TriggerKeys = triggerKeys;
        }

        public KeysDownCondition(params Keys[] triggerKeys ) : this(new List<Keys>(triggerKeys)){}

        public override bool IsCurrentlyFulfilled()
        {
            KeyboardState = Keyboard.GetState();
            foreach (Keys key in TriggerKeys)
            {
                if (KeyboardState.IsKeyDown(key))
	{
                    return true;
	}
            }
            return false;
        }

    }

}

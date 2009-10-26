using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public class KeyboardGameExitLink : ManyConditionsToOneReactionLink
    {
        public KeyboardGameExitLink(List<Keys> triggerKeys)
            : base(new KeysDownCondition(triggerKeys), new GameExitReaction()) 
        { }

        public KeyboardGameExitLink(params Keys[] triggerKeys)
            : this(new List<Keys>(triggerKeys))
            {}

    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public class KeyboardSceneChangeLink : ManyConditionsToOneReactionLink
    {
        public KeyboardSceneChangeLink(Scene targetScene, List<Keys> triggerKeys)
            : base(new KeysDownCondition(triggerKeys), new SceneChangeReaction(targetScene)) 
        { }

        public KeyboardSceneChangeLink(Scene targetScene, params Keys[] triggerKeys ): this(targetScene, new List<Keys>( triggerKeys))
            {}

    }
}

using System.Collections.Generic;
using GameDev.Core.Events;
using Microsoft.Xna.Framework.Input;

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

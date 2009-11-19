using System.Collections.Generic;
using GameDev.Core.Events;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework.Input;
#endif

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

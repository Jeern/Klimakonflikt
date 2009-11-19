using System.Collections.Generic;
using GameDev.Core.Events;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework.Input;
#endif

namespace GameDev.Core.SceneManagement
{
    public class MouseClickSceneChangeLink : ManyConditionsToOneReactionLink
    {
        public MouseClickSceneChangeLink(List<Buttons> triggerButtons, Scene targetScene) :
            base (new MouseDownCondition(triggerButtons), new SceneChangeReaction(targetScene)) { }

    }
}

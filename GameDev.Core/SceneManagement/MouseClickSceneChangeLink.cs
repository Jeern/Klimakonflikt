using System.Collections.Generic;
using GameDev.Core.Events;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.SceneManagement
{
    public class MouseClickSceneChangeLink : ManyConditionsToOneReactionLink
    {
        public MouseClickSceneChangeLink(List<Buttons> triggerButtons, Scene targetScene) :
            base (new MouseDownCondition(triggerButtons), new SceneChangeReaction(targetScene)) { }

    }
}

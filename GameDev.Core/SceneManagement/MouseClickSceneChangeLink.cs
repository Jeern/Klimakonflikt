using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;


namespace GameDev.Core.SceneManagement
{
    public class MouseClickSceneChangeLink : ManyConditionsToOneReactionLink
    {
        public MouseClickSceneChangeLink(List<Buttons> triggerButtons, Scene targetScene) :
            base (new MouseDownCondition(triggerButtons), new SceneChangeReaction(targetScene)) { }

    }
}

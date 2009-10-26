using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public class TimerSceneChangeLink : ManyConditionsToOneReactionLink
    {

        public TimerSceneChangeLink(int millisecondsPause, Scene sceneToChangeTo) : base(new TimerCondition(millisecondsPause), new SceneChangeReaction(sceneToChangeTo)){}

    }
}

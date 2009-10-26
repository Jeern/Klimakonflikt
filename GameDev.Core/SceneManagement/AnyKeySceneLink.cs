using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public class AnyKeySceneLink : ManyConditionsToOneReactionLink
    {
        public AnyKeySceneLink(Scene targetScene)
            : base(new AnyKeyCondition(), new SceneChangeReaction(targetScene))
        { }

    }
}

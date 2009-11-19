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

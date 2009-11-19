using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public class SceneChangeReaction : Reaction
    {

        public Scene TargetScene { get; set; }

        public SceneChangeReaction(Scene targetScene)
        {
            this.TargetScene = targetScene;
        }

        public void GoToScene()
        {
            TargetScene.SceneManager.ChangeScene(TargetScene);
        }

        public override void React(Condition sender)
        {
            GoToScene();
        }
    }
}

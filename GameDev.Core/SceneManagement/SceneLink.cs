using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public abstract class SceneLink : ISceneLink
    {
        public SceneLink(string targetSceneName, SceneManager sceneManager)
        {
            this.TargetSceneName = targetSceneName;
            this.SceneManager = sceneManager;
        }
        public string TargetSceneName { get; set; }
        public SceneManager SceneManager { get; set; }
        public abstract void Update(GameTime gameTime);

        public void GoToLink()
        {
            SceneManager.ChangeScene(TargetSceneName);
        }

    }
}

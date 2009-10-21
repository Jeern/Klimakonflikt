using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public abstract class SceneLink : ISceneLink
    {
        public SceneLink(IScene targetScene)
        {
            this.TargetScene = targetScene;
        }
        public IScene TargetScene { get; set; }
        
        SceneManager _sceneManager;
        public SceneManager SceneManager 
        {
            get
            {
                if (_sceneManager == null)
                {
                    SceneManager = TargetScene.SceneManager;
                }
                return _sceneManager;
            }
            private set { this._sceneManager = value; }
        }

        public abstract void Update(GameTime gameTime);

        public void GoToLink()
        {
            SceneManager.ChangeScene(TargetScene);
        }

    }
}

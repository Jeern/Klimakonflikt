using System;
using Microsoft.Xna.Framework;

namespace GameDev.Core.SceneManagement
{
    interface ISceneLink
    {
        void GoToLink();
        SceneManager SceneManager { get; set; }
        string TargetSceneName { get; set; }
        void Update(GameTime gameTime);
    }
}

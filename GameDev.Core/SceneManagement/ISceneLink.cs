using System;
using Microsoft.Xna.Framework;

namespace GameDev.Core.SceneManagement
{
    interface ISceneLink
    {
        void GoToLink();
        IScene TargetScene { get; set; }
        void Update(GameTime gameTime);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameDev.Core.SceneManagement
{
    public interface ISceneManager
    {
        void ChangeScene(string sceneName);
        void ChangeScene(IScene scene);
        IScene CurrentScene { get; }
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}

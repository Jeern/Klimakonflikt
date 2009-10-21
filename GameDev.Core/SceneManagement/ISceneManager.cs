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
        IScene CurrentScene { get; }
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}

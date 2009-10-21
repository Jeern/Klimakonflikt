using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.SceneManagement
{
    public interface ISceneManager
    {
        void ChangeScene(string sceneName);
        Scene CurrentScene { get; }
        void Draw(Microsoft.Xna.Framework.GameTime gameTime);
        void Update(Microsoft.Xna.Framework.GameTime gameTime);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public interface IScene
    {
        List<GameComponent> Components { get; set; }
        SceneManager SceneManager { get; }
        string Name { get; set; }
        SpriteBatch SpriteBatch { get; }

        void Pause();
        void Resume();
        void Initialize();
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}

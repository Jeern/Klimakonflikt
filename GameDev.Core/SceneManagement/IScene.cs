using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public interface IScene
    {

        string Name { get; set; }
        bool IsPaused { get; }
        Game Game { get; }
        SceneManager SceneManager { get; set; }
        SpriteBatch SpriteBatch { get; }

        List<GameComponent> Components { get; set; }
        List<SceneLink> SceneLinks { get; set; }

        void Pause();
        void Resume();
        
        void Initialize(); 
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}

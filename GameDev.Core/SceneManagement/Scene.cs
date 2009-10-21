using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public abstract class Scene :  IScene
    {

        #region IScene Members

        public Game Game { get; private set; }
        public SceneManager SceneManager { get;  set; }
        public string Name { get; set; }
        public List<GameComponent> Components { get; set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public bool IsPaused { get; private set; }
        public List<SceneLink> SceneLinks { get; set; }

        public virtual void Initialize() 
        {
            foreach (GameComponent component in Components)
            {
                component.Initialize();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsPaused)
            {
                foreach (GameComponent component in Components)
                {
                    component.Update(gameTime);
                }
                foreach (SceneLink link in SceneLinks)
                {
                    link.Update(gameTime);
                }
            }
        }


        public abstract void Draw(GameTime gameTime);

        public void Pause()
        {
            this.IsPaused = true;
        }

        public void Resume()
        {
            this.IsPaused = false;
        }

        #endregion


        #region Constructors

        public Scene(string name, Game game)
        {
            this.Name = name;
            this.Game = game;
            this.Components = new List<GameComponent>();
            SceneLinks = new List<SceneLink>();
            this.SpriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }

        #endregion






    
    }
}

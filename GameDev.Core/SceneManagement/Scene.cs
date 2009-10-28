using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public abstract class Scene 
    {

        #region Scene Members

        public GameDevGame Game { get; private set; }
        public SceneManager SceneManager { get;  set; }
        public string Name { get; set; }
        public List<GameComponent> Components { get; set; }
        public bool IsPaused { get; private set; }
        public SpriteBatch SpriteBatch { get { return Game.SpriteBatch; } }
        //public List<ManyConditionsToOneReactionLink> SceneLinks { get; set; }

        public abstract void OnEntered();
        public abstract void OnLeft();
        public abstract void Reset();

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
            }
        }


        public virtual void Draw(GameTime gameTime)
        {
            foreach (DrawableGameComponent comp in Components)
            {
                comp.Draw(gameTime);
            }
        }

        public virtual void Pause()
        {
            this.IsPaused = true;
        }

        public virtual void Resume()
        {
            this.IsPaused = false;
        }

        #endregion


        #region Constructors

        public Scene(string name)
        {
            this.Name = name;
            this.Game = GameDevGame.Current;
            this.Components = new List<GameComponent>();
            //SceneLinks = new List<ManyConditionsToOneReactionLink>();
        }

        #endregion








        
    }
}

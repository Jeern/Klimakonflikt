using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameDev.Core.Events;

namespace GameDev.Core.SceneManagement
{
    public abstract class Scene 
    {

        #region Scene Members

        public Color BackgroundColor { get; set; }
        protected KeyboardState UpdatedKeyboardState;
        public bool NoKeysPressed { get; protected set; }
        public GameDevGame Game { get; private set; }
        public SceneManager SceneManager { get;  set; }
        public string Name { get; set; }
        public List<GameComponent> Components { get; set; }
        public bool IsPaused { get; private set; }
        public SpriteBatch SpriteBatch { get { return Game.SpriteBatch; } }
        //public List<ManyConditionsToOneReactionLink> SceneLinks { get; set; }

        public virtual void OnEntered()
        {
            NoKeysPressed = false;
        }
        public virtual  void OnLeft() { }
        public virtual void Reset() { }

        public virtual void Initialize() 
        {
            foreach (GameComponent component in Components)
            {
                component.Initialize();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdatedKeyboardState = Keyboard.GetState();

            //ensures that the user must have lifted fingers from the key that brought him/her here
            if (!NoKeysPressed && UpdatedKeyboardState.GetPressedKeys().Length == 0)
            {
                NoKeysPressed = true;
                return;
            }

            if (!IsPaused)
            {
                foreach (GameComponent component in Components)
                {
                    if (component.Enabled)
                    {
                        component.Update(gameTime);    
                    }
                }
            }
        }


        public virtual void Draw(GameTime gameTime)
        {
            foreach (DrawableGameComponent comp in Components)
            {
                if (comp.Visible)
                {
                    comp.Draw(gameTime);
                }
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
            : this(name, Color.Black)
        { }

        public Scene(string name, Color backgroundColor)
        {
            this.Name = name;
            this.Game = GameDevGame.Current;
            this.Components = new List<GameComponent>();
            this.BackgroundColor = backgroundColor;
            NoKeysPressed = false;
            //SceneLinks = new List<ManyConditionsToOneReactionLink>();
        }

        #endregion








        
    }
}

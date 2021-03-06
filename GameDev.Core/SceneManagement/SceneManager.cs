﻿using System.Collections.Generic;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace GameDev.Core.SceneManagement
{
    /// <summary>
    /// Manages scenes (individual screens with interactivity) in a game
    /// </summary>
    public class SceneManager : DrawableGameComponent
    {
        static object m_lockObject = new object();
        static SceneManager m_manager;
        public static SceneManager Current
        {
            get
            { return m_manager; }
            private set 
            {
                lock (m_lockObject)
                {
                    if (m_manager == null)
                    {
                        m_manager = value;
                    }
                }
            }
        }
        private Scene _currentScene;
        public Scene CurrentScene
        {
            get { return _currentScene; }
            private set
            {
                //to make sure we don't set the scene twice
                //this is done because the Update() method runs on another thread
                //and might call update on the previous scene
                
                if (CurrentScene == value)
                {
                    return;
                }
                if (CurrentScene != null)
                {
                    CurrentScene.OnLeft();
                }
                _currentScene = value;
                CurrentScene.OnEntered();
            }
        }
        private SpriteBatch _spriteBatch;

        private Dictionary<string, Scene> _scenes;

        public SceneManager(Game game)
            : base(game)
        {
            Current = this;
            _scenes = new Dictionary<string, Scene>();
            this._spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            game.Services.AddService(typeof(SceneManager), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentScene != null)
            {
                this.CurrentScene.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            if (this.CurrentScene != null)
            {

                this.CurrentScene.Draw(gameTime);
            }
        }

        public Scene AddScene(Scene sceneToAdd)
        {
            _scenes.Add(sceneToAdd.Name, sceneToAdd);
            sceneToAdd.SceneManager = this;
            //if this is the first scene that was added
            if (_scenes.Count == 1)
            {
                this.CurrentScene = sceneToAdd;
            }
            return sceneToAdd;
        }

        public Scene GetScene(string sceneName)
        {
            return _scenes[sceneName];
        }

        public void ChangeScene(string sceneName)
        {
            if (_scenes.ContainsKey(sceneName))
            {
                this.CurrentScene = _scenes[sceneName];
            }
        }

        public void ChangeScene(Scene sceneToChangeTo)
        {
            ChangeScene(sceneToChangeTo.Name);
        }



        public void Pause()
        {
            if (this.CurrentScene != null) { this.CurrentScene.Pause(); }
        }
        public void Resume()
        {
            if (this.CurrentScene != null) { this.CurrentScene.Resume(); }
        }


        public override void Initialize()
        {
            foreach (string name in this._scenes.Keys)
            {
                _scenes[name].Initialize();
            }
        }


        public void ResetAllScenes()
        {
            foreach (string name in this._scenes.Keys)
            {
                _scenes[name].Reset();
            }
        }
    }
}

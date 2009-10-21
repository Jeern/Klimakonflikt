using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameDev.Core;
using GameDev.Core.Graphics;

namespace GameDev.Core.SceneManagement
{
    /// <summary>
    /// Manages scenes (individual screens with interactivity) in a game
    /// </summary>
    public class SceneManager : DrawableGameComponent, ISceneManager
    {

        public Scene CurrentScene { get; private set; }
        
        private Dictionary<string, Scene> _scenes;

        public SceneManager(Game game)
            : base(game)
        {
            _scenes = new Dictionary<string, Scene>();

            game.Services.AddService(typeof(ISceneManager), this);
        }

        public override void Update(GameTime gameTime) { this.CurrentScene.Update(gameTime); }

        public override void Draw(GameTime gameTime) { this.CurrentScene.Update(gameTime); }

        public void AddScene(Scene sceneToAdd)
        {
            _scenes.Add(sceneToAdd.Name, sceneToAdd);
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
    }
}

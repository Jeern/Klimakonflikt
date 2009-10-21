using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.SceneManagement
{
    public abstract class Scene : DrawableGameComponent, IScene
    {
        public SceneManager Manager { get; private set; }
        public string Name { get; set; }
        public List<DrawableGameComponent> DrawableGameComponents { get; set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Scene(string name, Game game) : base(game)
        {
            this.Name = name;
            this.DrawableGameComponents = new List<DrawableGameComponent>();
            this.SpriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

namespace GameDev.Core
{
    public class GameDevGame : Game
    {
        static object _locker = new object();
        static GameDevGame _current;
        public static GameDevGame Current { get { return _current; } }
        public SpriteBatch SpriteBatch { get; set; }
        public float GameSpeed { get; set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }


        public GameDevGame()
        {
            GameDevGame._current = this;
            this.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            this.Services.AddService(typeof(GraphicsDeviceManager), GraphicsDeviceManager);
            GameSpeed = 1.0F;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}

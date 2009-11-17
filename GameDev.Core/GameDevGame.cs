﻿using System;
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
        public Random Random = new Random();

        static object _locker = new object();
        private static GameDevGame m_game;
        public static GameDevGame
            Current { get { return m_game; }
                private set 
                {
                    lock (_locker)
                    {
                        if (m_game == null)
                        {
                            m_game = value;
                        }
                    }
                }
            }
        public SpriteBatch SpriteBatch { get; set; }
        public float GameSpeed { get; set; }

        public Vector2 ViewPortSize { get { return new Vector2(this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height); } }
        public Vector2 ViewPortCenter { get { return ViewPortSize / 2; } }

        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public SpriteFont DebugFont { get; set; }

        public GameDevGame()
        {
            GameDevGame.Current = this;
            this.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            this.Services.AddService(typeof(GraphicsDeviceManager), GraphicsDeviceManager);
            GameSpeed = 1.0F;
        }

        protected override void Initialize()
        {
            base.Initialize();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.DebugFont = Content.Load<SpriteFont>("DebugFont");
        }

    }
}

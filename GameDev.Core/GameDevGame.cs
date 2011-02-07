using System;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace GameDev.Core
{
    public class GameDevGame : Game
    {
        private static Random random = new Random();
        public static Random Random
        {
            get { return random; }
        }


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
        public SpriteBatch ParticleSpriteBatch { get; set; }
        public float GameSpeed { get; set; }

        public Vector2 ViewPortSize { get {
            return new Vector2(this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height); 
        } }
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
            ParticleSpriteBatch = new SpriteBatch(GraphicsDevice);
            
            this.DebugFont = Content.Load<SpriteFont>("DebugFont");
        }


        // From the Particles Sample on the XNA/MSDN site
        //  a handy little function that gives a random float between two
        // values. This will be used in several places in the sample, in particilar in
        // ParticleSystem.InitializeParticle.
        public static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }


    }
}

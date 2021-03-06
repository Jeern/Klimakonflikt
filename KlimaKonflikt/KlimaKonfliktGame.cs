using GameDev.Core;
using GameDev.Core.SceneManagement;
using KlimaKonflikt.Scenes;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace KlimaKonflikt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class KlimaKonfliktGame : GameDevGame
    {
        SceneManager sceneManager;


        RealRandom random = new RealRandom(1, 8);

        IntroScene m_introScene;
        KKMenuScene m_menuScene;
        MainScene m_mainGameScene;
        OilWinScene m_oilWinScene;
        InstructionsScene m_instructionsScene;
        FlowerWinScene m_flowerWinScene;
        CreditsScene m_creditsScene;
        
        public KlimaKonfliktGame()
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)this.Services.GetService(typeof(GraphicsDeviceManager));
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
#if DEBUG || SILVERLIGHT
            graphics.IsFullScreen = false;
#else
            graphics.IsFullScreen = true;
#endif
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            sceneManager = new SceneManager(this);

            m_introScene = new IntroScene();
            m_menuScene = new KKMenuScene();
            m_mainGameScene = new MainScene();
            m_oilWinScene = new OilWinScene();
            m_flowerWinScene = new FlowerWinScene();
            m_instructionsScene = new InstructionsScene();
            m_creditsScene = new CreditsScene();

            sceneManager.AddScene(m_introScene);
            sceneManager.AddScene(m_menuScene);
            sceneManager.AddScene(m_mainGameScene);
            sceneManager.AddScene(m_oilWinScene);
            sceneManager.AddScene(m_flowerWinScene);
            sceneManager.AddScene(m_instructionsScene);
            sceneManager.AddScene(m_creditsScene);

            this.Components.Add(sceneManager);
            sceneManager.Initialize();
            
        }

    }
}

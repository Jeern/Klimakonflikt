using System;
using System.Collections.Generic;
using System.Linq;
using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
using GameDev.GameBoard;
using GameDev.GameBoard.AI;
using GameDev.Core.Particles;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
using SilverArcade.SilverSprite.Audio;
using SilverArcade.SilverSprite.Input;
using SilverlightHelpers;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endif

namespace KlimaKonflikt.Scenes
{
    public class MainScene : Scene
    {

        public bool FireIsLethal { get; set; }
        public int NumberOfWinsToWinGame { get; set; }
        KKPlayer m_roundWinner = null;
        SmokePlumeParticleSystem m_smokeParticle;

        private enum GameState
        {
            WaitingToPlay,
            Playing,
            RoundOver,
            GameOver
        }
        #region Variables

        private GameImage m_healthPowerUpGameImage;
        List<VectorSprite> m_PowerUps;
        Dictionary<VectorSprite, WalledTile> m_powerupTiles;
        private float m_timeToNextPowerUp;
        private bool m_debug = false;
        private GameBoardControllerBase m_FireController;
        private GameBoardControllerBase m_OilController, m_SackController;
        private Color m_overlayColor = new Color(100, 100, 100, 180);
        private string m_overlaySubtext = "Press ENTER to continue";
        private Vector2 m_overlaySubtextSize;
        private GameState m_gameState;
        private float m_damageFactor = 1.0F;

        private GraphicTimer m_timer;
        private float m_timeTillSmokePuff;

        private SimpleDirectionController m_DirectionController;
        private SoundEffect m_SeedPlanting, m_OilDrip, m_SeedFueling, m_OilFueling;
        private SoundEffectInstance m_MainGameTune;
        private Point m_OilTowerBeginTilePosition, m_WheelbarrowBeginTilePosition;
        private Ejerskab[,] m_EjerskabsOversigt;
        private RealRandom m_RandomX, m_RandomY;

        private Texture2D m_TileFloor, m_OilSpill, m_Flower, m_HealthBar, m_ScorePicOilBarrel, m_ScorePicFlowerSack;
        private GameImage m_OilTowerImage, m_WheelBarrowImage, m_CompleteFloorGameImage;
        private Sprite m_OilTower, m_WheelBarrow;
        private SpriteFont m_largeFont, m_mediumFont, m_smallFont;
        private Rectangle[,] m_AmmoPlacering;

        private List<Color> m_HealthColors = new List<Color>()
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.GreenYellow,
                Color.Green 
            };

        private Color m_Shadow = new Color(0, 0, 0, .6F);
        private Color m_Background = new Color(50, 50, 50);
        private Color m_PositiveScoreColor = Color.White;
        private Color m_NegativeScoreColor = Color.Red;

        private KKGameBoard m_Board;
        private KKPlayer m_SeedSack, m_OilBarrel;
        private List<KKPlayer> m_Players = new List<KKPlayer>();
        private List<KKMonster> m_Fires = new List<KKMonster>();
        private Dictionary<KKPlayer, KKPlayer> m_Adversaries = new Dictionary<KKPlayer, KKPlayer>();
        private Dictionary<KKPlayer, WalledTile> m_RefuelPositions = new Dictionary<KKPlayer, WalledTile>();
        private Dictionary<KKPlayer, Sprite> m_RefuelImages = new Dictionary<KKPlayer, Sprite>();
        //private Dictionary<KKPlayer, GameImage> m_AmmoImages = new Dictionary<KKPlayer, GameImage>();

        private float m_ScoreDifference;

        public bool SinglePlayer { get; set; }

        #endregion

        #region Constructor and Initialize

        public MainScene()
            : base(SceneNames.MAINSCENE)
        {
            LoadLevels();
            LoadSounds();
            LoadGraphics();
            InitializeBoard();
            InitializeImages();
            InitializePlayersAndMonsters();
            InitializeParticleSystem();
            SaveToDictionaries();
            AddComponents();
            Reset();
        }

        private void InitializeParticleSystem()
        {
            m_smokeParticle = new SmokePlumeParticleSystem(25);
            m_smokeParticle.Initialize();
            //this.Components.Add(m_smokeParticle);
        }

        private void SaveToDictionaries()
        {
            m_RefuelImages[m_SeedSack] = m_WheelBarrow;
            m_RefuelImages[m_OilBarrel] = m_OilTower;
            //m_AmmoImages[m_SeedSack] = GameImages.GetBlomstImage();
            //m_AmmoImages[m_OilBarrel] = GameImages.GetOlieImage();
            m_Adversaries[m_SeedSack] = m_OilBarrel;
            m_Adversaries[m_OilBarrel] = m_SeedSack;

            m_Players.Add(m_OilBarrel);
            m_Players.Add(m_SeedSack);
        }

        private void InitializePlayersAndMonsters()
        {
            m_SeedSack = new KKPlayer(GameImages.GetFlowersackImage(), .2F,
                m_Board.Tiles[m_Board.StartPositionFlowerSack.X, m_Board.StartPositionFlowerSack.Y].Center,
                10,100, m_SeedPlanting, m_SeedFueling, Ejerskab.Blomst);
            m_OilBarrel = new KKPlayer(GameImages.GetOilBarrelImage(), .2F,
                m_Board.Tiles[m_Board.StartPositionOilBarrel.X, m_Board.StartPositionOilBarrel.Y].Center,
                10, 100,m_OilDrip, m_OilFueling, Ejerskab.Olie);
            foreach (Point startPos in m_Board.StartPositionsFire)
            {
                m_Fires.Add(new KKMonster(GameImages.GetIldImage(), .10F,
                    m_Board.Tiles[startPos.X, startPos.Y].Center,
                    new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                    new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20));
            }
            FireIsLethal = true;
        }

        private void InitializeImages()
        {
            m_OilTowerBeginTilePosition = m_Board.StartPositionOilTower;
            m_WheelbarrowBeginTilePosition = m_Board.StartPositionWheelBarrow;
            m_OilTower.GameImageOffset = new Point(0, -20);
            m_AmmoPlacering = new Rectangle[2, 10];
            int ammoSize = 100;
            int ammoBottomOffset = 600;
            for (int y = 0; y < 10; y++)
            {
                m_AmmoPlacering[0, y] = new Rectangle(20, ammoBottomOffset - 55 * y, ammoSize, ammoSize);
                m_AmmoPlacering[1, y] = new Rectangle(910, ammoBottomOffset - 55 * y, ammoSize, ammoSize);
            }
            
            m_FireController = new RandomController(m_Board);
            //m_FireController = new SimpleDirectionController(m_Board);
            //((SimpleDirectionController)m_FireController).TargetTile = m_Board.Tiles[4, 4];
            m_timer = new GraphicTimer(m_largeFont, 3);
            m_timer.TimesUp += new EventHandler<EventArgs>(m_timer_TimesUp);
        }

        private void InitializeBoard()
        {
            Components.Add(m_Board);
            base.Initialize();
            m_Board.SetPosition(new Point((m_Board.GraphicsDevice.Viewport.Width - m_Board.WidthInPixels) / 2,
                (m_Board.GraphicsDevice.Viewport.Height - m_Board.HeightInPixels) / 2));
        }

        private void AddComponents()
        {
            m_WheelBarrow.DrawOrder = 100;
            Components.Add(m_WheelBarrow);
            Components.Add(m_OilTower);
            Components.Add(m_SeedSack);
            Components.Add(m_OilBarrel);
            foreach (KKMonster fire in m_Fires)
            {
                Components.Add(fire);
            }
            Components.Add(m_timer);
        }

        private void LoadGraphics()
        {
            m_HealthBar = Game.Content.Load<Texture2D>("bar");
            m_OilSpill = Game.Content.Load<Texture2D>("Olie/ThePatch0030");
            m_Flower = Game.Content.Load<Texture2D>("Blomst/Blomst0030");
            m_ScorePicOilBarrel = Game.Content.Load<Texture2D>("Oilbarrel/OilBarrel001");
            m_ScorePicFlowerSack = Game.Content.Load<Texture2D>("Flowersack/FlowerSack1");
            Texture2D completeFloor = Texture2D.FromFile(Game.GraphicsDevice, m_Board.LevelImageFileName);
            m_CompleteFloorGameImage = new GameImage(completeFloor);
            m_Board.CompleteBackground = m_CompleteFloorGameImage;
            m_largeFont = Game.Content.Load<SpriteFont>("LargeArial");
            m_mediumFont = Game.Content.Load<SpriteFont>("MediumArial");
            m_smallFont = Game.Content.Load<SpriteFont>("SmallArial");
            m_overlaySubtextSize = m_smallFont.MeasureString(m_overlaySubtext);
            Texture2D wheelBarrowTexture = Game.Content.Load<Texture2D>("wheelbarrel");
            m_OilTowerImage = GameImages.GetOlieTaarnImage();
            m_OilTower = new Sprite(m_OilTowerImage, 0, m_Board.Tiles[m_OilTowerBeginTilePosition.X, m_OilTowerBeginTilePosition.Y].Center);

            m_WheelBarrowImage = new GameImage(wheelBarrowTexture);
            m_healthPowerUpGameImage = Game.Content.Load<Texture2D>("PowerUps/healthpowerup");
            m_WheelBarrow = new Sprite(m_WheelBarrowImage, 0, m_Board.Tiles[m_WheelbarrowBeginTilePosition.X, m_WheelbarrowBeginTilePosition.Y].Center);

        }

        private void LoadSounds()
        {
            SoundEffect effect = Game.Content.Load<SoundEffect>(@"GameTunes\MainGameTune");
            m_MainGameTune = effect.CreateInstance();
#if SILVERLIGHT
            m_MainGameTune.Loop = true;
#else
            m_MainGameTune.IsLooped = true;
#endif
            m_SeedPlanting = Game.Content.Load<SoundEffect>("froe_plantes");
            m_OilDrip = Game.Content.Load<SoundEffect>("olieplet_spildes");
            m_SeedFueling = Game.Content.Load<SoundEffect>("tankfroe");
            m_OilFueling = Game.Content.Load<SoundEffect>("tankolie");
        }

        private void LoadLevels()
        {
            m_TileFloor = Game.Content.Load<Texture2D>("64x64");
            GameImage staticFloor = new GameImage(m_TileFloor);
            IEnumerable<KKGameBoard> boards = LevelLoader.GetLevels(Game, staticFloor, SpriteBatch, staticFloor.CurrentTexture.Width);
            m_Board = boards.ToArray()[0];
        }

        void m_timer_TimesUp(object sender, EventArgs e)
        {

            m_gameState = GameState.Playing;
        }

        void UnitMoved(object sender, EventArgs<Sprite> e)
        {
            FireAndFuelCollisionTest((KKPlayer)e.Data);
        }

        void TileCenterCrossed(object sender, EventArgs<Sprite> e)
        {
            TileCollisionTest((KKPlayer)e.Data);
        }

        #endregion

        #region Movement and collision

        private void FireAndFuelCollisionTest(KKPlayer player)
        {
            WalledTile playerTile = m_Board.GetTileFromPixelPosition(player.X, player.Y);
            foreach (KKMonster fire in m_Fires)
            {
                WalledTile fireTile = m_Board.GetTileFromPixelPosition(fire.GetPosition().X, fire.GetPosition().Y);

                if (fireTile == playerTile)
                {
                    Collision(player);
                    player.TakeoverTileSound.Play();
                    return;
                }
            }
            if (m_RefuelPositions[player] == playerTile)
            {
                if (player.Ammunition < 10)
                {
                    player.RefuelSound.Play();
                    player.Ammunition = 10;
                    MoveRefuelPosition(player);
                }
            }
            else
            {
                if (m_RefuelPositions.ContainsValue(playerTile))
                {
                    MoveRefuelPosition(m_Adversaries[player]);
                }

            }

            if (player.Health < player.MaxHealth)
            {


                VectorSprite found = null;
                foreach (VectorSprite powerup in m_PowerUps)
                {
                    if (powerup.Position.DistanceTo(player.GetPosition().ToVector2()) < 25)
                    {
                        found = powerup;
                        break;
                    }
                }
                if (found != null)
                {
                    m_PowerUps.Remove(found);
                    m_powerupTiles.Remove(found);
                    player.Health += 20;
                }
            }

        }

        private void TileCollisionTest(KKPlayer player)
        {
            WalledTile playerTile = m_Board.GetTileFromPixelPosition(player.X, player.Y);



            if (player.Ammunition > 0 && player.EjerskabsType != m_EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex])
            {
                KKPlayer adversary = m_Adversaries[player];

                if (m_EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex] == adversary.EjerskabsType)
                {
                    adversary.EjedeFelter--;
                }
                m_EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex] = player.EjerskabsType;

                if (player == m_SeedSack)
                {
                    playerTile.ContentGameImage = GameImages.GetBlomstImage();
                }
                else if (player == m_OilBarrel)
                {
                    playerTile.ContentGameImage = GameImages.GetOlieImage();
                }


                player.EjedeFelter++;
                player.TakeoverTileSound.Play();
                player.Ammunition--;

            }
        }

        private void Collision(KKPlayer player)
        {
            player.Ammunition = 0;
            if (!FireIsLethal)
            {
                player.Health -= 1F;
            }
            else
            {
                player.Health = 0;
            }

        }

        private WalledTile MoveRefuelPosition(KKPlayer player)
        {

            WalledTile newPosition = m_RefuelPositions[m_SeedSack];
            do
            {
                newPosition = m_Board.Tiles[m_RandomX.Next(), m_RandomY.Next()];
            } while (m_RefuelPositions.ContainsValue(newPosition) || (m_powerupTiles.ContainsValue(newPosition)));

            m_RefuelPositions[player] = newPosition;
            m_RefuelImages[player].SetPosition(m_RefuelPositions[player].Center);

            if (SinglePlayer)
            {
                FindAITargets();
            }

            return newPosition;
        }

        private void FindAITargets()
        {
            KKPlayer player = m_OilBarrel;
            A_StarController astar = (A_StarController)m_OilController;
            astar.TargetTiles.Clear();


            //if (player.EjedeFelter > m_Adversaries[player].EjedeFelter && player.Health < player.MaxHealth / 2 && player.Ammunition > 5)
            //{
            //    foreach (WalledTile t in m_powerupTiles.Values)
            //    {
            //        astar.TargetTiles.Add(t);
            //    }
            //}

            if (m_OilBarrel.Ammunition == 10 && player.EjedeFelter > 1)
            {

                astar.TargetTiles.Add(m_RefuelPositions[m_SeedSack]);
                astar.TargetTiles.Add(m_RefuelPositions[m_OilBarrel]);

            }
            else
            {
                astar.TargetTiles.Add(m_RefuelPositions[m_OilBarrel]);
                astar.TargetTiles.Add(m_RefuelPositions[m_SeedSack]);
            }

            foreach (Tile t in m_Board.Corners)
            {
                astar.TargetTiles.Add((WalledTile)t);
            }

        }

        #endregion

        #region Update and Draw


        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            m_smokeParticle.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switch (m_gameState)
                {
                    case GameState.RoundOver:
                        ResetRound();
                        break;
                    case GameState.GameOver:
                        if (m_OilBarrel.RoundsWon == NumberOfWinsToWinGame)
                        {
                            SceneManager.ChangeScene(SceneNames.OILWINSCENE);
                        }
                        else
                        {
                            SceneManager.ChangeScene(SceneNames.FLOWERWINSCENE);
                        }
                        break;
                }

            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                SceneManager.ChangeScene(SceneNames.MENUSCENE);
            }

            if (keyboardState.IsKeyDown(Keys.P))
            {
                Pause();
                m_MainGameTune.Pause();
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                Resume();
                m_MainGameTune.Resume();
            }

            if (keyboardState.IsKeyDown(Keys.RightControl) && keyboardState.IsKeyDown(Keys.Insert))
            {
                m_damageFactor = 1 - m_damageFactor;
            }

            if (keyboardState.IsKeyDown(Keys.F1))
            {
                m_debug = !m_debug;
            }


            #region speedchange

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                GameDevGame.Current.GameSpeed = 0.6F;
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                GameDevGame.Current.GameSpeed = 0.8F;
            }
            else if (keyboardState.IsKeyDown(Keys.D3))
            {
                GameDevGame.Current.GameSpeed = 1F;
            }
            else if (keyboardState.IsKeyDown(Keys.D4))
            {
                GameDevGame.Current.GameSpeed = 1.4F;
            }
            else if (keyboardState.IsKeyDown(Keys.D5))
            {
                GameDevGame.Current.GameSpeed = 2F;
            }

            if (keyboardState.IsKeyDown(Keys.F1))
            {
                SinglePlayer = true;
            }

            #endregion

            if (!IsPaused && m_gameState == GameState.Playing)
            {
                m_timeToNextPowerUp -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (m_powerupTiles.Count <= 3 && m_timeToNextPowerUp <= 0)
                {
                    Vector2 boardPosition = new Vector2(m_Board.Y, m_Board.Y);
                    Tile oilTile = m_Board.GetTileFromPixelPosition(m_OilBarrel.GetPosition());
                    Tile sackTile = m_Board.GetTileFromPixelPosition(m_SeedSack.GetPosition());
                    
                    
                    Tile randomTile = null;

                    do
                    {
                        randomTile = m_Board.GetRandomTile();    
                    } while (!(randomTile != oilTile && sackTile != randomTile && randomTile != m_RefuelPositions[m_OilBarrel] && randomTile != m_RefuelPositions[m_SeedSack] && !m_powerupTiles.ContainsValue((WalledTile)randomTile)));
                    
                        VectorSprite powerup = new VectorSprite(randomTile.Center.ToVector2(), Vector2.Zero, m_healthPowerUpGameImage);
                        powerup.Scale = new Vector2(0.25F,0.25F);

                        m_PowerUps.Add(powerup);
                        m_powerupTiles.Add(powerup, (WalledTile)randomTile);

                    m_timeToNextPowerUp = 10000;
                }


                if (m_SeedSack.Health <= 0.0F)
                {
                    m_SeedSack.Health = 0.0F;
                    m_OilBarrel.RoundsWon++;
                    m_gameState = GameState.RoundOver;
                    m_roundWinner = m_OilBarrel;
                }
                else if (m_OilBarrel.Health <= 0.0F)
                {
                    m_OilBarrel.Health = 0.0F;
                    m_SeedSack.RoundsWon++;
                    m_gameState = GameState.RoundOver;
                    m_roundWinner = m_SeedSack;
                }
                foreach (KKPlayer player in m_Players)
                {
                    if (player.RoundsWon == NumberOfWinsToWinGame)
                    {
                        m_gameState = GameState.GameOver;
                    }
                }
                    m_OilController.Update(gameTime, m_OilBarrel);
                    m_SackController.Update(gameTime, m_SeedSack);
                    foreach (KKMonster fire in m_Fires)
                    {
                        m_FireController.Update(gameTime, fire);
                    }

                    m_ScoreDifference = m_SeedSack.EjedeFelter - m_OilBarrel.EjedeFelter;
                    float scoreDifferenceFactor = Math.Abs(m_ScoreDifference) / 200;
                    switch (Math.Sign(m_ScoreDifference))
                    {
                        case -1: // frø er foran
                            m_SeedSack.Health -= scoreDifferenceFactor * m_damageFactor;
                            break;

                        case 1: // OilDrum er foran
                            m_OilBarrel.Health -= scoreDifferenceFactor * m_damageFactor;
                            break;
                    }
                    float maxSpeed = .4F;
                    float lowestHealth = Math.Min(m_SeedSack.Health, m_OilBarrel.Health);
                    if (lowestHealth < 50 && m_gameState == GameState.Playing)
                    {
                        m_MainGameTune.Pitch = maxSpeed - (lowestHealth / 50 * maxSpeed);
                    }
            }

            m_timeTillSmokePuff -= gameTime.ElapsedGameTime.Milliseconds;
            if (m_timeTillSmokePuff <= 0)
            {
                foreach (KKMonster  fire in m_Fires)
                {
                    m_smokeParticle.AddParticles(fire.GetPosition().ToVector2());
                }
                m_timeTillSmokePuff = ParticleSystem.Random.Next(750);
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(m_Background);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();


            base.Draw(gameTime);

            foreach (VectorSprite powerup in m_PowerUps)
            {
                powerup.Draw(gameTime);
            }

            int shadowOffset = 9;

            for (int y = m_OilBarrel.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(m_OilSpill, m_AmmoPlacering[0, y].GetOffsetCopy(shadowOffset), m_Shadow);
                SpriteBatch.Draw(m_OilSpill, m_AmmoPlacering[0, y], Color.White);
            }
            for (int y = m_SeedSack.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(m_Flower, m_AmmoPlacering[1, y].GetOffsetCopy(shadowOffset), m_Shadow);
                SpriteBatch.Draw(m_Flower, m_AmmoPlacering[1, y], Color.White);
            }

            int olieBarRectangleHeight = (int)(640 * m_OilBarrel.Health / 100);
            int SeedSackRectangleHeight = (int)(640 * m_SeedSack.Health / 100);

            Rectangle olieBarRectangle = new Rectangle(140, 700 - olieBarRectangleHeight, m_HealthBar.Width, olieBarRectangleHeight);
            Rectangle SeedSackBarRectangle = new Rectangle(850, 700 - SeedSackRectangleHeight, m_HealthBar.Width, SeedSackRectangleHeight);

            int SeedSackHealthBarIndex = (int)(m_SeedSack.Health / 20);
            int OilDrumHealthBarIndex = (int)(m_OilBarrel.Health / 20);

            if (SeedSackHealthBarIndex > 4) SeedSackHealthBarIndex = 4;
            if (OilDrumHealthBarIndex > 4) OilDrumHealthBarIndex = 4;
            if (SeedSackHealthBarIndex < 0) SeedSackHealthBarIndex = 0;
            if (OilDrumHealthBarIndex < 0) OilDrumHealthBarIndex = 0;


            
            SpriteBatch.Draw(m_ScorePicOilBarrel, new Rectangle(10, 10, 120, 150), Color.White);
            SpriteBatch.Draw(m_ScorePicFlowerSack, new Rectangle(890, 10, 120, 150), Color.White);

            Color player1ScoreColor = Color.White;
            Color player2ScoreColor = Color.White;

            if (m_ScoreDifference > 0)
            {
                player1ScoreColor = m_NegativeScoreColor;
                player2ScoreColor = m_PositiveScoreColor;
            }
            else if (m_ScoreDifference < 0)
            {
                player2ScoreColor = m_NegativeScoreColor;
                player1ScoreColor = m_PositiveScoreColor;

            }

            string strScoreDifference = ((-1) * m_ScoreDifference).ToString();
            float scoreWidth = m_largeFont.MeasureString(strScoreDifference).X;
            Vector2 scorePosition = new Vector2(50 - scoreWidth / 2, 55);
            Vector2 scoreShadowPosition = new Vector2(55 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(m_largeFont, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(m_largeFont, strScoreDifference, scorePosition, player1ScoreColor);

            strScoreDifference = m_ScoreDifference.ToString();
            scoreWidth = m_largeFont.MeasureString(strScoreDifference).X;
            scorePosition = new Vector2(940 - scoreWidth / 2, 55);
            scoreShadowPosition = new Vector2(945 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(m_largeFont, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(m_largeFont, strScoreDifference, scorePosition, player2ScoreColor);

            SpriteBatch.Draw(m_HealthBar, olieBarRectangle, m_HealthColors[OilDrumHealthBarIndex]);
            SpriteBatch.Draw(m_HealthBar, SeedSackBarRectangle, m_HealthColors[SeedSackHealthBarIndex]);

            if (m_debug)
            {

                Vector2 tilePosition, boardPosition;
                boardPosition = new Vector2(m_Board.X + 5, m_Board.Y + 5);
                foreach (Tile t in m_Board.Tiles)
                {
                    tilePosition = new Vector2(t.X, t.Y);
                    tilePosition += boardPosition;
                    SpriteBatch.DrawString(m_smallFont, TileCostCalculator((WalledTile)t).ToString(), tilePosition, Color.Black);
                    tilePosition -= Vector2.One;
                    SpriteBatch.DrawString(m_smallFont, TileCostCalculator((WalledTile)t).ToString(), tilePosition, Color.White);

                }

                A_StarController astar = (A_StarController)m_OilController;
                List<Path<WalledTile>> paths = astar.PathsTried;
                if (paths.Count> 0)
                {
                    int pathCounter = 1;
                    foreach (Path<WalledTile> path in paths)
                    {
                        Color pathDotColor = Color.Red;
                        if (path == astar.Path)
                        {
                            pathDotColor = Color.LightGreen;
                            
                        }
                        foreach (WalledTile t in path)
                        {
                            tilePosition = t.GetPosition().ToVector2();
                            tilePosition += boardPosition + Vector2.One * 15;

                            //SpriteBatch.Draw(BaseTextures.Circle_128x128, new Rectangle((int)tilePosition.X, (int)tilePosition.Y, 10, 10), pathDotColor);
                            SpriteBatch.DrawString(m_smallFont, "path" + pathCounter, tilePosition + Vector2.One, Color.Black);
                            SpriteBatch.DrawString(m_smallFont, "path" +pathCounter,  tilePosition ,pathDotColor);
                            
                        }
                        pathCounter++;
                    }

                }
            }

            //draw scores
            SpriteBatch.DrawString(m_mediumFont, m_OilBarrel.RoundsWon + "/" + NumberOfWinsToWinGame, new Vector2(40, 700), Color.Silver);
            SpriteBatch.DrawString(m_mediumFont, m_SeedSack.RoundsWon + "/" + NumberOfWinsToWinGame, new Vector2(925, 700), Color.Silver);

            //draw end of round/game message
            if (m_gameState == GameState.RoundOver)
            {
                DrawOverlay("Round won by player " + (m_Players.IndexOf(m_roundWinner) + 1));
            }
            else if (m_gameState == GameState.GameOver)
            {
                DrawOverlay("GAME won by player " + (m_Players.IndexOf(m_roundWinner) + 1));
            }
            m_smokeParticle.Draw(gameTime);

            SpriteBatch.End();
        }

        #endregion

        #region Scene class overrides

        public override void OnEntered()
        {
            Reset();
            m_MainGameTune.Play();

        }
        public override void OnLeft()
        {
            m_MainGameTune.Stop();
        }

        public override void Reset()
        {
            ResetRound();
            foreach (KKPlayer player in m_Players)
            {
                player.ResetGame();
            }
        }

        private void ResetRound()
        {
            m_EjerskabsOversigt = new Ejerskab[m_Board.TilesHorizontally, m_Board.TilesVertically];
            m_RefuelPositions[m_SeedSack] = m_Board.Tiles[m_WheelbarrowBeginTilePosition.X, m_WheelbarrowBeginTilePosition.Y];
            m_RefuelPositions[m_OilBarrel] = m_Board.Tiles[m_OilTowerBeginTilePosition.X, m_OilTowerBeginTilePosition.Y];
            m_WheelBarrow.SetPosition(m_RefuelPositions[m_SeedSack].Center);
            m_OilTower.SetPosition(m_RefuelPositions[m_OilBarrel].Center);

            foreach (KKPlayer player in m_Players)
            {
                player.Reset();
            }

            m_SackController = new KeyboardController(m_Board, KeyboardController.KeySet.ArrowKeys);

            //m_DirectionController = new SimpleDirectionController(m_Board);
            //m_DirectionController.TargetTile = m_RefuelPositions[m_OilBarrel];


            if (SinglePlayer)
            {
                A_StarController astar = (A_StarController)new A_StarController(m_Board, TileCostCalculator);
                m_OilController = astar;
                FindAITargets();
                astar.AcceptableCost = 99;
                m_RandomX = new RealRandom(1, m_Board.TilesHorizontally - 2);
                m_RandomY = new RealRandom(1, m_Board.TilesVertically - 2);
            }
            else
            {
                m_OilController = new KeyboardController(m_Board, KeyboardController.KeySet.WASD);
                m_RandomX = new RealRandom(0, m_Board.TilesHorizontally - 1);
                m_RandomY = new RealRandom(0, m_Board.TilesVertically - 1);
            }
            m_OilController.TileCenterCrossed += TileCenterCrossed;
            m_OilController.UnitMoved += UnitMoved;
            m_SackController.TileCenterCrossed += TileCenterCrossed;
            m_SackController.UnitMoved += UnitMoved;


            //flytter ild tilbage til udgangspositioner
            foreach (KKMonster fire in m_Fires)
            {
                fire.Reset();
            }

            m_ScoreDifference = 0;
            m_MainGameTune.Pitch = 0;
            m_Board.Reset();
            m_gameState = GameState.WaitingToPlay;
            m_timer.SecondsToCountDown = 3;
            m_timer.Reset();

            m_PowerUps = new List<VectorSprite>();
            m_powerupTiles = new Dictionary<VectorSprite, WalledTile>();
            m_timeToNextPowerUp = 10000;
            m_roundWinner = null;

        }
        #endregion



        private void DrawOverlay(string textToWrite)
        {
            Vector2 textSize = m_mediumFont.MeasureString(textToWrite);
            Vector2 overlayPosition = GameDevGame.Current.ViewPortCenter - textSize / 2;
            Vector2 subtextPosition = GameDevGame.Current.ViewPortCenter - m_overlaySubtextSize / 2;
            Rectangle background = new Rectangle((int)overlayPosition.X - 25, (int)overlayPosition.Y - 15, (int)textSize.X + 50, (int)textSize.Y + 45);
            GameDevGame.Current.SpriteBatch.DrawRectangle(background, m_overlayColor);
            GameDevGame.Current.SpriteBatch.DrawString(m_mediumFont, textToWrite, overlayPosition + (Vector2.One * 3), Color.Black);
            GameDevGame.Current.SpriteBatch.DrawString(m_mediumFont, textToWrite, overlayPosition + (Vector2.One), Color.White);
            GameDevGame.Current.SpriteBatch.DrawString(m_smallFont, m_overlaySubtext, subtextPosition + Vector2Helper.Down * 45, Color.Black);
            GameDevGame.Current.SpriteBatch.DrawString(m_smallFont, m_overlaySubtext, subtextPosition + Vector2Helper.Down * 43, Color.White);

        }

        private double TileCostCalculator(WalledTile tile)
        {
            foreach (KKMonster monster in m_Fires)
            {
                WalledTile monsterTile = (WalledTile)m_Board.GetTileFromPixelPosition(monster.GetPosition());
                if (monsterTile == tile)
                {
                    return 200;
                }
                else
                {
                    if (monsterTile.AccessibleNeighbours.Contains(tile))
                    {
                        return 100;
                    }
                }
            }

            Ejerskab ejer = m_EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex];


            switch (ejer)
            {
                case Ejerskab.None:
                    return 2;
                case Ejerskab.Blomst:
                    if (m_OilBarrel.Ammunition > 4)
                    {
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                case Ejerskab.Olie:
                    return 4;
            }
            return 2;

        }
    }
}
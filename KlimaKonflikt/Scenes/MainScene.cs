using System;
using System.Collections.Generic;
using System.Linq;
using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
using GameDev.GameBoard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KlimaKonflikt.Scenes
{
    public class MainScene : Scene
    {
        #region Variables

        private RandomController m_FireController;
        private GameBoardControllerBase m_OilController, m_SackController;

        private bool m_gameWaitingToBegin = true;
        private float m_damageFactor = 1.0F;

        private GraphicTimer m_timer;

        private SimpleDirectionController m_DirectionController;
        private bool m_GameOver = false;
        private SoundEffect m_SeedPlanting, m_OilDrip, m_SeedFueling, m_OilFueling;
        private SoundEffectInstance m_MainGameTune;
        private Point m_OilTowerBeginTilePosition, m_WheelbarrowBeginTilePosition;
        private Ejerskab[,] m_EjerskabsOversigt;
        private RealRandom m_RandomX, m_RandomY;
        
        private Texture2D m_TileFloor, m_OilSpill, m_Flower, m_HealthBar, m_ScorePicOilBarrel, m_ScorePicFlowerSack;
        private GameImage m_OilTowerImage, m_WheelBarrowImage, m_CompleteFloorGameImage, m_PulsatingCircle;
        private Sprite m_OilTower, m_WheelBarrow;
        private SpriteFont m_Font;
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
        private Dictionary<KKPlayer, GameImage> m_AmmoImages = new Dictionary<KKPlayer, GameImage>();

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
            SaveToDictionaries();
            AddComponents();
            Reset();
        }

        private void SaveToDictionaries()
        {
            m_RefuelImages[m_SeedSack] = m_WheelBarrow;
            m_RefuelImages[m_OilBarrel] = m_OilTower;
            m_AmmoImages[m_SeedSack] = GameImages.GetBlomstImage();
            m_AmmoImages[m_OilBarrel] = GameImages.GetOlieImage();
            m_Adversaries[m_SeedSack] = m_OilBarrel;
            m_Adversaries[m_OilBarrel] = m_SeedSack;
            m_Players.Add(m_SeedSack);
            m_Players.Add(m_OilBarrel);
        }

        private void InitializePlayersAndMonsters()
        {
            m_SeedSack = new KKPlayer(GameImages.GetFlowersackImage(Game.Content), .2F,
                m_Board.Tiles[m_Board.StartPositionFlowerSack.X, m_Board.StartPositionFlowerSack.Y].Center,
                10, m_PulsatingCircle, m_SeedPlanting, m_SeedFueling, Ejerskab.Blomst);
            m_OilBarrel = new KKPlayer(GameImages.GetOilBarrelImage(Game.Content), .2F,
                m_Board.Tiles[m_Board.StartPositionOilBarrel.X, m_Board.StartPositionOilBarrel.Y].Center,
                10, m_PulsatingCircle, m_OilDrip, m_OilFueling, Ejerskab.Olie);
            foreach (Point startPos in m_Board.StartPositionsFire)
            {
                m_Fires.Add(new KKMonster(GameImages.GetIldImage(Game.Content), .10F,
                    m_Board.Tiles[startPos.X, startPos.Y].Center,
                    new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                    new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20));
            }
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
            m_timer = new GraphicTimer(m_Font, 3);
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
            m_Font = Game.Content.Load<SpriteFont>("Arial");
            Texture2D wheelBarrowTexture = Game.Content.Load<Texture2D>("wheelbarrel");
            m_OilTowerImage = GameImages.GetOlieTaarnImage(Game.Content);
            m_OilTower = new Sprite(m_OilTowerImage, 0, m_Board.Tiles[m_OilTowerBeginTilePosition.X, m_OilTowerBeginTilePosition.Y].Center);

            m_WheelBarrowImage = new GameImage(wheelBarrowTexture);

            m_WheelBarrow = new Sprite(m_WheelBarrowImage, 0, m_Board.Tiles[m_WheelbarrowBeginTilePosition.X, m_WheelbarrowBeginTilePosition.Y].Center);
            m_PulsatingCircle = GameImages.GetPulsatingCircleImage(Game.Content);


        }

        private void LoadSounds()
        {
            SoundEffect effect = Game.Content.Load<SoundEffect>(@"GameTunes\MainGameTune");
            m_MainGameTune = effect.CreateInstance();
            m_MainGameTune.IsLooped = true;
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
            m_gameWaitingToBegin = false;
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
                //else
                //{
                    
                //}


            }
            else
            {


                //if (player.Ammunition < 5 && player == OilDrum && astarController.TargetTile != RefuelPositions[SeedSack])
                //{
                //    astarController.TargetTile = RefuelPositions[OilDrum];
                //}

                if (m_RefuelPositions.ContainsValue(playerTile))
                {
                    MoveRefuelPosition(m_Adversaries[player]);
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
            if (!SinglePlayer)
            {
                player.Health -= 1F;
            }
            
        }

        private WalledTile MoveRefuelPosition(KKPlayer player)
        {

            WalledTile newPosition = m_RefuelPositions[m_SeedSack];
            do
            {
                newPosition = m_Board.Tiles[m_RandomX.Next(), m_RandomY.Next()];
            } while (m_RefuelPositions.ContainsValue(newPosition));

            m_RefuelPositions[player] = newPosition;
            m_RefuelImages[player].SetPosition(m_RefuelPositions[player].Center);

            if (SinglePlayer)
            {
                if (m_OilBarrel.Ammunition == 10)
                {
                    ((A_StarController)m_OilController).TargetTile = m_RefuelPositions[m_SeedSack];
                    //Console.WriteLine("Ammo: " + OilDrum.Ammunition + ", going for adversary's refuel at [" + astarController.TargetTile.HorizontalIndex + "," + astarController.TargetTile.VerticalIndex + "]");
                }
                else
                {
                    //Console.WriteLine("Ammo: " + OilDrum.Ammunition + ", going for own refuel at [" + astarController.TargetTile.HorizontalIndex + "," + astarController.TargetTile.VerticalIndex + "]");
                    ((A_StarController)m_OilController).TargetTile = m_RefuelPositions[m_OilBarrel];
                }
            }

            return newPosition;
        }

        #endregion


        #region Update and Draw


        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

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

            if (!IsPaused && !m_gameWaitingToBegin)
            {

                //oilController.Update(gameTime, OilDrum);
                //astarController.TargetTile = RefuelPositions[OilDrum];// board.GetTileFromPixelPosition(OilDrum.GetPosition());
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
                if (lowestHealth < 50 && !m_GameOver)
                {
                    m_MainGameTune.Pitch = maxSpeed - (lowestHealth / 50 * maxSpeed);
                }
                
            }
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(m_Background);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();

            if (m_SeedSack.Health <= 0.5F)
            {

                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.OILWINSCENE);
                SpriteBatch.End();
                return;
            }
            else if (m_OilBarrel.Health <= 0.5F)
            {
                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.FLOWERWINSCENE);
                SpriteBatch.End();
                return;
            }

            base.Draw(gameTime);

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
            float scoreWidth = m_Font.MeasureString(strScoreDifference).X;
            Vector2 scorePosition = new Vector2(50 - scoreWidth/2, 55);
            Vector2 scoreShadowPosition = new Vector2(55 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(m_Font, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(m_Font, strScoreDifference, scorePosition, player1ScoreColor);

            strScoreDifference = m_ScoreDifference.ToString();
            scoreWidth = m_Font.MeasureString(strScoreDifference).X;
            scorePosition = new Vector2(940 - scoreWidth / 2, 55);
            scoreShadowPosition = new Vector2(945 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(m_Font, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(m_Font, strScoreDifference, scorePosition, player2ScoreColor);      

            SpriteBatch.Draw(m_HealthBar, olieBarRectangle, m_HealthColors[OilDrumHealthBarIndex]);
            SpriteBatch.Draw(m_HealthBar, SeedSackBarRectangle, m_HealthColors[SeedSackHealthBarIndex]);
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
            m_EjerskabsOversigt = new Ejerskab[m_Board.TilesHorizontally, m_Board.TilesVertically];
            m_RefuelPositions[m_SeedSack] = m_Board.Tiles[m_WheelbarrowBeginTilePosition.X, m_WheelbarrowBeginTilePosition.Y];
            m_RefuelPositions[m_OilBarrel] = m_Board.Tiles[m_OilTowerBeginTilePosition.X, m_OilTowerBeginTilePosition.Y];
            m_WheelBarrow.SetPosition(m_RefuelPositions[m_SeedSack].Center);
            m_OilTower.SetPosition(m_RefuelPositions[m_OilBarrel].Center);

            foreach (KKPlayer player in m_Players)
            {
                player.Reset();
            }
            m_OilController = (A_StarController)new A_StarController(m_Board);

            m_SackController = new KeyboardController(m_Board, KeyboardController.KeySet.ArrowKeys);
            ((A_StarController)m_OilController).TargetTile = m_RefuelPositions[m_OilBarrel];

            m_DirectionController = new SimpleDirectionController(m_Board);
            m_DirectionController.TargetTile = m_RefuelPositions[m_OilBarrel];

            //aggressiveFireController = new SimpleDirectionController(board);
            
            if (SinglePlayer)
            {
                //oilController = directionController;
                m_RandomX = new RealRandom(1, m_Board.TilesHorizontally-2);
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
            m_gameWaitingToBegin = true;
            m_timer.SecondsToCountDown = 3;
            m_timer.Reset();

        #endregion

        }
    }
}
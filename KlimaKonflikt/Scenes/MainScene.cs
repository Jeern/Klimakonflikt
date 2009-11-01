﻿#region Usings
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameDev.Core;
using GameDev.GameBoard;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

#endregion


namespace KlimaKonflikt.Scenes
{
    public class MainScene : Scene
    {

        #region Variables

        RandomController fireController;
        GameBoardControllerBase oilController, sackController;
        SimpleDirectionController directionController;
        private bool m_GameOver = false;
        int tilesAcross = 10, tilesDown = 10;
        SoundEffect plantFrø, olieDryp, frøTankning, olieTankning;
        SoundEffectInstance m_mainGameTune;
        Point m_oilTowerBeginTilePosition, m_wheelbarrowBeginTilePosition;
        GameBoard board;
        KKPlayer SeedSack, OilDrum;
        KKMonster m_Ild1, m_Ild2;
        Ejerskab[,] EjerskabsOversigt;
        RealRandom random = null;
        Texture2D tileFloor, oilSpill, flower, healthBarTexture, scorePicOilBarrel, scorePicFlowerSack;
        GameImage oilTowerImage1, wheelBarrowImage, completeFloorGameImage, pulsatingCircle;
        Sprite oilTower1, wheelBarrow1;
        SpriteFont font;
        //WalledTile oilTowerTile, wheelBarrowTile;
        Rectangle[,] ammoPlacering;
        Color[] healthColors;
        Color shadow = new Color(0, 0, 0, .6F);
        Color background = new Color(50, 50, 50);
        Dictionary<KKPlayer, KKPlayer> Adversary;
        Dictionary<KKPlayer, WalledTile> RefuelPositions;
        Dictionary<KKPlayer, Sprite> RefuelImage;
        Dictionary<KKPlayer, GameImage> AmmoImages;

        Color positiveScoreColor = Color.White;
        Color negativeScoreColor = Color.Red;

        float scoreDifference;

        public bool SinglePlayer { get; set; }

        List<KKPlayer> Players;

        #endregion


        #region Constructor and Initialize

        public MainScene()
            : base(SceneNames.MAINSCENE)
        {
            tileFloor = Game.Content.Load<Texture2D>("64x64");

            GameImage staticFloor = new GameImage(tileFloor);
            SoundEffect effect = Game.Content.Load<SoundEffect>(@"GameTunes\MainGameTune");
            m_mainGameTune = effect.CreateInstance();
            m_mainGameTune.IsLooped = true;

            plantFrø = Game.Content.Load<SoundEffect>("froe_plantes");
            olieDryp = Game.Content.Load<SoundEffect>("olieplet_spildes");
            frøTankning = Game.Content.Load<SoundEffect>("tankfroe");
            olieTankning = Game.Content.Load<SoundEffect>("tankolie");

            healthBarTexture = Game.Content.Load<Texture2D>("bar");
            oilSpill = Game.Content.Load<Texture2D>("Olie/ThePatch0030");
            flower = Game.Content.Load<Texture2D>("Blomst/Blomst0030");

            scorePicOilBarrel = Game.Content.Load<Texture2D>("Oilbarrel/OilBarrel001");
            scorePicFlowerSack = Game.Content.Load<Texture2D>("Flowersack/FlowerSack1");

            IEnumerable<GameBoard> boards = LevelLoader.GetLevels(Game, staticFloor, SpriteBatch, staticFloor.CurrentTexture.Width);
            //board = new GameBoard(this, staticFloor, SpriteBatch, "Board", tilesAcross, tilesDown, 64);
            board = boards.ToArray()[0];

            Texture2D completeFloor = Game.Content.Load<Texture2D>("full_level");
            completeFloorGameImage = new GameImage(completeFloor);
            board.CompleteBackground = completeFloorGameImage;
            board.SetPosition(new Point(200, 50));

            //testing pathfinder


            m_oilTowerBeginTilePosition = new Point(4, 4);

            m_wheelbarrowBeginTilePosition = new Point(5, 5);


            font = Game.Content.Load<SpriteFont>("Arial");
            Texture2D wheelBarrowTexture = Game.Content.Load<Texture2D>("wheelbarrel");
            oilTowerImage1 = GameImages.GetOlieTaarnImage(Game.Content);
            oilTower1 = new Sprite(oilTowerImage1, 0, board.Tiles[m_oilTowerBeginTilePosition.X, m_oilTowerBeginTilePosition.Y].Center);

            wheelBarrowImage = new GameImage(wheelBarrowTexture);

            wheelBarrow1 = new Sprite(wheelBarrowImage, 0, board.Tiles[m_wheelbarrowBeginTilePosition.X, m_wheelbarrowBeginTilePosition.Y].Center);

            oilTower1.GameImageOffset = new Point(0, -20);

            pulsatingCircle = GameImages.GetPulsatingCircleImage(Game.Content);

            RefuelImage = new Dictionary<KKPlayer, Sprite>();

            SeedSack = new KKPlayer(GameImages.GetFlowersackImage(Game.Content), .2F, board.Tiles[9, 9].Center, 10, pulsatingCircle, plantFrø, frøTankning, Ejerskab.Blomst);
            OilDrum = new KKPlayer(GameImages.GetOilBarrelImage(Game.Content), .2F, board.Tiles[0, 0].Center, 10, pulsatingCircle, olieDryp, olieTankning, Ejerskab.Olie);

            Players = new List<KKPlayer>();
            Players.Add(SeedSack);
            Players.Add(OilDrum);


            RefuelImage[SeedSack] = wheelBarrow1;
            RefuelImage[OilDrum] = oilTower1;

            m_Ild1 = new KKMonster(GameImages.GetIldImage(Game.Content), .23F, board.Tiles[0, 9].Center,
                new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20);
            m_Ild2 = new KKMonster(GameImages.GetIldImage(Game.Content), .25F, board.Tiles[9, 0].Center,
                new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20);
            AmmoImages = new Dictionary<KKPlayer, GameImage>();

            AmmoImages[SeedSack] = GameImages.GetBlomstImage();
            AmmoImages[OilDrum] = GameImages.GetOlieImage();

            Adversary = new Dictionary<KKPlayer, KKPlayer>();
            Adversary[SeedSack] = OilDrum;
            Adversary[OilDrum] = SeedSack;

            RefuelPositions = new Dictionary<KKPlayer, WalledTile>();

            Components.Add(board);

            Components.Add(wheelBarrow1);
            Components.Add(oilTower1);

            Components.Add(SeedSack);
            Components.Add(OilDrum);

            Components.Add(m_Ild1);
            Components.Add(m_Ild2);

            healthColors = new Color[5];
            healthColors[0] = Color.Red;
            healthColors[1] = Color.Orange;
            healthColors[2] = Color.Yellow;
            healthColors[3] = Color.GreenYellow;
            healthColors[4] = Color.Green;

            base.Initialize();
            ammoPlacering = new Rectangle[2, 10];
            int ammoSize = 100;
            int ammoBottomOffset = 600;
            for (int y = 0; y < 10; y++)
            {
                ammoPlacering[0, y] = new Rectangle(20, ammoBottomOffset - 55 * y, ammoSize, ammoSize);
                ammoPlacering[1, y] = new Rectangle(910, ammoBottomOffset - 55 * y, ammoSize, ammoSize);
            }
            
            fireController = new RandomController(board);

            Reset();
        }

        void UnitMoved(Sprite sprite, EventArgs e)
        {
            FireCollistionTest((KKPlayer)sprite);
        }

        void TileCenterCrossed(Sprite sprite, EventArgs e)
        {
            TileCollisionTest((KKPlayer)sprite);
        }


        public override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here


        }

        #endregion


        #region Movement and collision

        private void FireCollistionTest(KKPlayer player)
        {
            WalledTile ild1Tile = board.GetTileFromPixelPosition(m_Ild1.GetPosition().X, m_Ild1.GetPosition().Y);
            WalledTile ild2Tile = board.GetTileFromPixelPosition(m_Ild2.GetPosition().X, m_Ild2.GetPosition().Y);
            WalledTile playerTile = board.GetTileFromPixelPosition(player.X, player.Y);

            if (ild1Tile == playerTile || ild2Tile == playerTile)
            {
                Collision(player);
                player.TakeoverTileSound.Play();
            }

        }

        private void TileCollisionTest(KKPlayer player)
        {
            WalledTile playerTile = board.GetTileFromPixelPosition(player.X, player.Y);

            if (RefuelPositions[player] == playerTile)
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
                if (RefuelPositions.ContainsValue(playerTile))
                {
                    MoveRefuelPosition(Adversary[player]);
                }
            }

            if (player.Ammunition > 0 && player.EjerskabsType != EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex])
            {
                KKPlayer adversary = Adversary[player];

                if (EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex] == adversary.EjerskabsType)
                {
                    adversary.EjedeFelter--;
                }
                EjerskabsOversigt[playerTile.HorizontalIndex, playerTile.VerticalIndex] = player.EjerskabsType;

                if (player == SeedSack)
                {
                    playerTile.ContentGameImage = GameImages.GetBlomstImage();
                }
                else if (player == OilDrum)
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
        }

        private WalledTile MoveRefuelPosition(KKPlayer player)
        {

            WalledTile newPosition = RefuelPositions[SeedSack];
            do
            {
                newPosition = board.Tiles[random.Next(), random.Next()];
            } while (RefuelPositions.ContainsValue(newPosition));

            RefuelPositions[player] = newPosition;
            RefuelImage[player].SetPosition(RefuelPositions[player].Center);

            if (SinglePlayer && player == OilDrum)
            {
                directionController.TargetTile = RefuelPositions[player];
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
                this.Pause();
                m_mainGameTune.Pause();
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                this.Resume();
                m_mainGameTune.Resume();
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

            if (!this.IsPaused)
            {

                //if (!m_singlePlayer)
                //{
                    //foreach (KKPlayer player in Players)
                    //{
                    //    CalculatePlayerMove(gameTime, player);
                    //}

                //}
                //else
                //{
                //    CalculatePlayerMove(gameTime, SeedSack);
                //    fireController.Update(gameTime, OilDrum);
                //}
                oilController.Update(gameTime, OilDrum);
                sackController.Update(gameTime, SeedSack);
                fireController.Update(gameTime, m_Ild1);
                fireController.Update(gameTime, m_Ild2);

      
                scoreDifference = SeedSack.EjedeFelter - OilDrum.EjedeFelter;
                float scoreDifferenceFactor = Math.Abs(scoreDifference) / 200;
                switch (Math.Sign(scoreDifference))
                {
                    case -1: // frø er foran
                        SeedSack.Health -= scoreDifferenceFactor;
                        break;

                    case 1: // OilDrum er foran
                        OilDrum.Health -= scoreDifferenceFactor;
                        break;
                }
                float maxSpeed = .4F;
                float lowestHealth = Math.Min(SeedSack.Health, OilDrum.Health);
                if (lowestHealth < 50 && !m_GameOver)
                {
                    m_mainGameTune.Pitch = maxSpeed - (lowestHealth / 50 * maxSpeed);
                }
                base.Update(gameTime);
            }
        }


        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(background);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();

            if (SeedSack.Health <= 0.5F)
            {

                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.OILWINSCENE);
                SpriteBatch.End();
                return;
            }
            else if (OilDrum.Health <= 0.5F)
            {
                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.FLOWERWINSCENE);
                SpriteBatch.End();
                return;
            }

            base.Draw(gameTime);

            int shadowOffset = 9;

            for (int y = OilDrum.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(oilSpill, ammoPlacering[0, y].GetOffsetCopy(shadowOffset), shadow);
                SpriteBatch.Draw(oilSpill, ammoPlacering[0, y], Color.White);
            }
            for (int y = SeedSack.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(flower, ammoPlacering[1, y].GetOffsetCopy(shadowOffset), shadow);
                SpriteBatch.Draw(flower, ammoPlacering[1, y], Color.White);
            }

            int olieBarRectangleHeight = (int)(640 * OilDrum.Health / 100);
            int SeedSackRectangleHeight = (int)(640 * SeedSack.Health / 100);

            Rectangle olieBarRectangle = new Rectangle(140, 700 - olieBarRectangleHeight, healthBarTexture.Width, olieBarRectangleHeight);
            Rectangle SeedSackBarRectangle = new Rectangle(850, 700 - SeedSackRectangleHeight, healthBarTexture.Width, SeedSackRectangleHeight);

            int SeedSackHealthBarIndex = (int)(SeedSack.Health / 20);
            int OilDrumHealthBarIndex = (int)(OilDrum.Health / 20);

            if (SeedSackHealthBarIndex > 4) SeedSackHealthBarIndex = 4;
            if (OilDrumHealthBarIndex > 4) OilDrumHealthBarIndex = 4;
            if (SeedSackHealthBarIndex < 0) SeedSackHealthBarIndex = 0;
            if (OilDrumHealthBarIndex < 0) OilDrumHealthBarIndex = 0;

            SpriteBatch.Draw(this.scorePicOilBarrel, new Rectangle(10, 10, 120, 150), Color.White);
            SpriteBatch.Draw(this.scorePicFlowerSack, new Rectangle(890, 10, 120, 150), Color.White);

            Color player1ScoreColor = Color.White;
            Color player2ScoreColor = Color.White;

            if (scoreDifference > 0)
            {
                player1ScoreColor = negativeScoreColor;
                player2ScoreColor = positiveScoreColor;
            }
            else if (scoreDifference < 0)
            {
                player2ScoreColor = negativeScoreColor;
                player1ScoreColor = positiveScoreColor;

            }

            string strScoreDifference = ((-1) * scoreDifference).ToString();
            float scoreWidth = font.MeasureString(strScoreDifference).X;
            Vector2 scorePosition = new Vector2(50 - scoreWidth/2, 55);
            Vector2 scoreShadowPosition = new Vector2(55 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(font, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(font, strScoreDifference, scorePosition, player1ScoreColor);

            strScoreDifference = scoreDifference.ToString();
            scoreWidth = font.MeasureString(strScoreDifference).X;
            scorePosition = new Vector2(940 - scoreWidth / 2, 55);
            scoreShadowPosition = new Vector2(945 - scoreWidth / 2, 60);

            SpriteBatch.DrawString(font, strScoreDifference, scoreShadowPosition, Color.Black);
            SpriteBatch.DrawString(font, strScoreDifference, scorePosition, player2ScoreColor);      

            SpriteBatch.Draw(healthBarTexture, olieBarRectangle, healthColors[OilDrumHealthBarIndex]);
            SpriteBatch.Draw(healthBarTexture, SeedSackBarRectangle, healthColors[SeedSackHealthBarIndex]);
            SpriteBatch.End();

        }


        #endregion


        #region Scene class overrides

        public override void OnEntered()
        {
            Reset();
            m_mainGameTune.Play();

        }
        public override void OnLeft()
        {
            m_mainGameTune.Stop();

        }
        public override void Reset()
        {

            
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown];
            RefuelPositions[SeedSack] = board.Tiles[m_wheelbarrowBeginTilePosition.X, m_wheelbarrowBeginTilePosition.Y];
            RefuelPositions[OilDrum] = board.Tiles[m_oilTowerBeginTilePosition.X, m_oilTowerBeginTilePosition.Y];
            wheelBarrow1.SetPosition(RefuelPositions[SeedSack].Center);
            oilTower1.SetPosition(RefuelPositions[OilDrum].Center);

            foreach (KKPlayer player in Players)
            {
                player.Reset();
            }


            sackController = new KeyboardController(board, KeyboardController.KeySet.ArrowKeys);
            directionController = new SimpleDirectionController(board);
            directionController.TargetTile = RefuelPositions[OilDrum];

            if (SinglePlayer)
            {
                oilController = directionController;
                random = new RealRandom(1, 8);
            }
            else
            {
                oilController = new KeyboardController(board, KeyboardController.KeySet.WASD);
                random = new RealRandom(0, 9);
            }
            oilController.TileCenterCrossed += new MoveEventHandler(TileCenterCrossed);
            oilController.UnitMoved += new MoveEventHandler(UnitMoved);
            sackController.TileCenterCrossed += new MoveEventHandler(TileCenterCrossed);
            sackController.UnitMoved += new MoveEventHandler(UnitMoved);

            
            //flytter ild tilbage til udgangspositioner
            m_Ild1.Reset();
            m_Ild2.Reset();

            m_mainGameTune.Pitch = 0;
            board.Reset();


        #endregion

        }
    }
}

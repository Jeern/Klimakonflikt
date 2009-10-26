#region Usings
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
using GameDev.GameBoard;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

	#endregion


namespace KlimaKonflikt.Scenes
{
    public class MainScene : Scene
    {

        
        private AIController m_AIController = new AIController();
        private bool m_GameOver = false;
        private bool m_GameStarting = true;
        private bool m_EndTunePlaying = false;
        private bool m_StartTunePlaying = true;
        private bool m_CreditsDisplayed = false;

        int tilesAcross = 10, tilesDown = 10;
        

        private DateTime endTime = DateTime.MaxValue;
        
        SoundEffect plantFrø, olieDryp, frøTankning, olieTankning;
        SoundEffectInstance m_mainGameTune;

        Point m_oilTowerBeginTilePosition, m_wheelbarrowBeginTilePosition;

        GameBoard board;
        
        KKPlayer frøPose, olieTønde;
        KKMonster m_Ild1, m_Ild2;

        Ejerskab[,] EjerskabsOversigt;

        RealRandom random = new RealRandom(1, 8);

        float m_speedFactor = 1.0F;


        #region Graphics

        Texture2D tileFloor, oilSpill, flower, healthBarTexture;
        GameImage oilTowerImage1, wheelBarrowImage, completeFloorGameImage;
        Sprite oilTower1, wheelBarrow1;
        SpriteFont font;
        WalledTile oilTowerTile, wheelBarrowTile;
        Rectangle[,] ammoPlacering;
        Color[] healthColors;
        Color shadow = new Color(0, 0, 0, .6F);
        Color background = new Color(50, 50, 50);

        #endregion


        public MainScene() : base(SceneNames.MAINSCENE)
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


            IEnumerable<GameBoard> boards = LevelLoader.GetLevels(Game, staticFloor, SpriteBatch, staticFloor.CurrentTexture.Width);
            //board = new GameBoard(this, staticFloor, SpriteBatch, "Board", tilesAcross, tilesDown, 64);
            board = boards.ToArray()[0];

            Texture2D completeFloor = Game.Content.Load<Texture2D>("full_level");
            completeFloorGameImage = new GameImage(completeFloor);
            board.CompleteBackground = completeFloorGameImage;
            board.SetPosition(new Point(200, 50));

            m_oilTowerBeginTilePosition = new Point(4,4);

            m_wheelbarrowBeginTilePosition = new Point(5, 5);


            font = Game.Content.Load<SpriteFont>("Arial");
            Texture2D wheelBarrowTexture = Game.Content.Load<Texture2D>("wheelbarrel");
            oilTowerImage1 = GameImages.GetOlieTaarnImage(Game.Content);
            oilTower1 = new Sprite(oilTowerImage1, 0, board.Tiles[m_oilTowerBeginTilePosition.X, m_oilTowerBeginTilePosition.Y].Center);

            wheelBarrowImage = new GameImage(wheelBarrowTexture);

            wheelBarrow1 = new Sprite(wheelBarrowImage, 0, board.Tiles[m_wheelbarrowBeginTilePosition.X, m_wheelbarrowBeginTilePosition.Y].Center);

            oilTower1.GameImageOffset = new Point(0, -20);
            frøPose = new KKPlayer(GameImages.GetFlowersackImage(Game.Content),.2F, board.Tiles[9, 9].Center, 10);
            olieTønde = new KKPlayer(GameImages.GetOilBarrelImage(Game.Content), .2F, board.Tiles[0, 0].Center, 10);
            m_Ild1 = new KKMonster(GameImages.GetIldImage(Game.Content), .23F, board.Tiles[0, 9].Center,
                new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20);
            m_Ild2 = new KKMonster(GameImages.GetIldImage(Game.Content), .25F, board.Tiles[9, 0].Center,
                new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                new RandomSequencer(0, 2), Direction.Down, Direction.Right, Direction.Up), 20);


            Components.Add(board);

            this.Components.Add(wheelBarrow1);
            this.Components.Add(oilTower1);
            Components.Add(frøPose);
            Components.Add(olieTønde);
            Components.Add(m_Ild1);
            Components.Add(m_Ild2);

            Reset();
        }

        public override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here

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


        }



        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                SceneManager.ChangeScene(SceneNames.CREDITSSCENE);
            }
            //if (m_CreditsDisplayed)
            //{
            //    m_CreditsDisplayed = false;
            //    //endTunePlayer.Play();
            //}

            //if (m_StartTunePlaying && !m_GameStarting)
            //{
            //    //startTunePlayer.Stop();
            //    //annoyingTunePlayer.Play();
            //    m_StartTunePlaying = false;
            //}

            //if (m_GameStarting && Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //    m_GameStarting = false;
            //}

            //if (m_EndTunePlaying)
            //    return;

            if (keyboardState.IsArrowKeyDown())
            {
                frøPose.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionArrowKeys());
                //Console.WriteLine(player1WantedDirection);
            }

            if (keyboardState.IsWASDKeyDown())
            {
                olieTønde.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionWASDKeys());
                //Console.WriteLine(player1WantedDirection);
            }

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                this.m_speedFactor = 0.6F;
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                this.m_speedFactor = 0.8F;
            }
            else if (keyboardState.IsKeyDown(Keys.D3))
            {
                this.m_speedFactor = 1F;
            }
            else if (keyboardState.IsKeyDown(Keys.D4))
            {
                this.m_speedFactor = 1.4F;
            }
            else if (keyboardState.IsKeyDown(Keys.D5))
            {
                this.m_speedFactor = 2F;
            }

            CalculatePlayerMove(gameTime, frøPose);
            CalculatePlayerMove(gameTime, olieTønde);
            CalculateAIMove(gameTime, board, m_Ild1);
            CalculateAIMove(gameTime, board, m_Ild2);

            CollisionTest();    

            //m_BlomstImage.Update(gameTime);
            //m_OlieImage.Update(gameTime);

            float scoreDifference = frøPose.EjedeFelter - olieTønde.EjedeFelter;
            float scoreDifferenceFactor = Math.Abs(scoreDifference) / 200;
            switch (Math.Sign(scoreDifference))
        	{
                case -1: // frø er foran
                    frøPose.Health -= scoreDifferenceFactor;
                    break;

                case 1: // olietønde er foran
                    olieTønde.Health -= scoreDifferenceFactor;
                    break;
	        }
            float maxSpeed = .4F;
            float lowestHealth = Math.Min(frøPose.Health, olieTønde.Health);
            if (lowestHealth < 50 && !m_GameOver)
            {
                m_mainGameTune.Pitch = maxSpeed - (lowestHealth / 50 * maxSpeed);
            }
            //else if (m_GameOver)
            //{
            //    m_mainGameTune.Stop();
            //    m_EndTunePlaying = true;
            //}
            base.Update(gameTime);
        }


        public void CalculateAIMove(GameTime gameTime, GameBoard board, KKMonster monster)
        {
            int pixelsToMove = (int)(monster.Speed * gameTime.ElapsedGameTime.Milliseconds * m_speedFactor);
            Point newPosition = monster.GetNewPosition(monster.Direction, pixelsToMove);
            Point oldPosition = monster.GetPosition();

            Point centerOfPlayersTile = board.GetTileFromPixelPosition(monster.GetPosition().X, monster.GetPosition().Y).Center;
            WalledTile tile = board.GetTileFromPixelPosition(monster.GetPosition());


            if (GeometryTools.IsBetweenPoints(centerOfPlayersTile, newPosition, oldPosition))
            {
                //we are going to cross the center
                //first move to center
                Point tempPosition = centerOfPlayersTile;
                monster.SetPosition(centerOfPlayersTile);

                int pixelMovesLeft = int.MinValue;
                DirectionChanger deltaMoves = DirectionHelper4.Offsets[monster.Direction];
                if (deltaMoves.DeltaX != 0) //we are moving horizontally
                {
                    pixelMovesLeft = Math.Abs(newPosition.X - oldPosition.X);
                }
                else //we are moving vertically
                {
                    pixelMovesLeft = Math.Abs(newPosition.Y - oldPosition.Y);
                }


                Direction wantedDirection = monster.WantedDirection(tile);
                Direction monsterDirection = monster.Direction;

                if (wantedDirection != Direction.None)
                {
                    if (!tile.HasBorder(wantedDirection))
                    {
                        monster.Direction = wantedDirection;
                        monster.Move(monster.Direction, pixelMovesLeft);
                    }
                    else
                    {
                        //monster.AllowDirectionChange();
                        if (!tile.HasBorder(monsterDirection))
                        {
                            monster.Move(monsterDirection, pixelMovesLeft);
                        }
                    }
                }
            }
            else
            {
                //monster.WantedDirection(tile);
                monster.X = newPosition.X;
                monster.Y = newPosition.Y;
            }
        }



        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(background);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();

            //if (m_GameStarting)
            //{
            //    SpriteBatch.Begin();
            //    SpriteBatch.Draw(m_Splash, new Rectangle(0, 0, 1024, 768), Color.White);
            //    SpriteBatch.End();
            //    return;
            //}

            //if (endTime != DateTime.MaxValue && endTime.AddSeconds(5.0) < DateTime.Now)
            //{
            //    //m_CreditsDisplayed = true;
            //    SpriteBatch.End();
            //    SceneManager.ChangeScene("Splash");
            //    //base.Draw(gameTime);
            //    //SpriteBatch.Draw(m_CreditSplash, new Rectangle(0, 0, 1024, 768), Color.White);
                
            //    return;
            //}

            if (frøPose.Health <= 0.5F)
            {

                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.OILWINSCENE);
                if (endTime == DateTime.MaxValue)
                    endTime = DateTime.Now;
                //base.Draw(gameTime);
                //SpriteBatch.Draw(m_WinOil, new Rectangle(0, 0, 1024, 768), Color.White);
                SpriteBatch.End();
                return;
            }
            else if (olieTønde.Health <= 0.5F)
            {
                m_GameOver = true;
                SceneManager.ChangeScene(SceneNames.FLOWERWINSCENE);
                if (endTime == DateTime.MaxValue)
                    endTime = DateTime.Now;
                //base.Draw(gameTime);
                //SpriteBatch.Draw(m_WinFlower, new Rectangle(0, 0, 1024, 768), Color.White);
                SpriteBatch.End();
                return;
            }

            base.Draw(gameTime);

            int shadowOffset = 9;
            SpriteBatch.DrawString(font, olieTønde.EjedeFelter.ToString(), new Vector2(20, 50), Color.White);
            SpriteBatch.DrawString(font, frøPose.EjedeFelter.ToString(), new Vector2(910, 50), Color.White);

            for (int y = olieTønde.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(oilSpill, ammoPlacering[0, y].GetOffsetCopy(shadowOffset), shadow);
                SpriteBatch.Draw(oilSpill, ammoPlacering[0, y], Color.White);
            }
            for (int y = frøPose.Ammunition - 1; y >= 0; y--)
            {
                SpriteBatch.Draw(flower, ammoPlacering[1, y].GetOffsetCopy(shadowOffset), shadow);
                SpriteBatch.Draw(flower, ammoPlacering[1, y], Color.White);
            }

            int olieBarRectangleHeight = (int)(640 * olieTønde.Health / 100);
            int frøPoseRectangleHeight = (int)(640 * frøPose.Health / 100);

            Rectangle olieBarRectangle = new Rectangle(140, 700 - olieBarRectangleHeight, healthBarTexture.Width, olieBarRectangleHeight);
            Rectangle frøPoseBarRectangle = new Rectangle(850, 700 - frøPoseRectangleHeight, healthBarTexture.Width, frøPoseRectangleHeight);

            int frøPoseHealthBarIndex = (int)(frøPose.Health / 20);
            int olieTøndeHealthBarIndex = (int)(olieTønde.Health / 20);

            if (frøPoseHealthBarIndex > 4) frøPoseHealthBarIndex = 4;
            if (olieTøndeHealthBarIndex > 4) olieTøndeHealthBarIndex = 4;
            if (frøPoseHealthBarIndex < 0) frøPoseHealthBarIndex = 0;
            if (olieTøndeHealthBarIndex < 0) olieTøndeHealthBarIndex = 0;

            SpriteBatch.Draw(healthBarTexture, olieBarRectangle, healthColors[olieTøndeHealthBarIndex]);
            SpriteBatch.Draw(healthBarTexture, frøPoseBarRectangle, healthColors[frøPoseHealthBarIndex]);
            SpriteBatch.End();

        }

            private void CollisionTest()
        {
            Tile ild1Tile = board.GetTileFromPixelPosition(m_Ild1.GetPosition().X, m_Ild1.GetPosition().Y);
            Tile ild2Tile = board.GetTileFromPixelPosition(m_Ild2.GetPosition().X, m_Ild2.GetPosition().Y);
            Tile frøTile = board.GetTileFromPixelPosition(frøPose.GetPosition().X, frøPose.GetPosition().Y);
            Tile olieTile = board.GetTileFromPixelPosition(olieTønde.GetPosition().X, olieTønde.GetPosition().Y);

            if (ild1Tile == frøTile || ild2Tile == frøTile)
            {
                Collision(frøPose);
                plantFrø.Play();
            }
            if (ild1Tile == olieTile || ild2Tile == olieTile)
            {
                Collision(olieTønde);
                olieDryp.Play();
            }
        }

        private void Collision(KKPlayer player)
        {
            player.Ammunition = 0;
        }


        private void CalculatePlayerMove(GameTime gameTime, KKPlayer player)
        {

            //if (frøPose.Direction == Direction.None)
            //{
            //    frøPose.Direction = frøPose.WantedDirection;
            //}

            //player1Position.Move(player1WantedDirection, player1Speed * gameTime.ElapsedGameTime.Milliseconds);
            int pixelsToMove = (int)(player.Speed * gameTime.ElapsedGameTime.Milliseconds * m_speedFactor);
            Point newPosition = player.GetNewPosition(player.Direction, pixelsToMove);
            Point oldPosition = player.GetPosition();
            //Console.WriteLine("Dir: " + player1Direction + ", wanted: " + player1WantedDirection);

            Point centerOfPlayersTile = board.GetTileFromPixelPosition(player.GetPosition().X, player.GetPosition().Y).Center;
            WalledTile tile = board.GetTileFromPixelPosition(player.GetPosition());


            if (GeometryTools.IsBetweenPoints(centerOfPlayersTile, newPosition, oldPosition))
            {
                //Console.WriteLine("BETWEEN");
                //we are going to cross the center
                //first move to center
                Point tempPosition = centerOfPlayersTile;
                player.SetPosition(centerOfPlayersTile);

                if ((player == olieTønde && oilTowerTile == tile) || (player == frøPose && wheelBarrowTile == tile ))
                {
                    if (player.Ammunition < 10)
                    {

                    
                    player.Ammunition = 10;
                    
                    if (player == olieTønde)
                    {
                        olieTankning.Play();
                        oilTowerTile = GetNewRefuelPosition();
                        oilTower1.SetPosition(oilTowerTile.Center);
                    }
                    else if (player == frøPose)
                    {
                        frøTankning.Play();
                        wheelBarrowTile = GetNewRefuelPosition();
                        wheelBarrow1.SetPosition(wheelBarrowTile.Center);
                    }
                    }
                }
                else
                {


                    if (player.Ammunition > 0)
                    {
                        if (player == frøPose)
                        {
                            //****** SÆT BLOMST
                            if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] != Ejerskab.Blomst)
                            {
                                if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] == Ejerskab.Olie)
                                {
                                    olieTønde.EjedeFelter--;
                                }
                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Blomst;
                                tile.ContentGameImage = GameImages.GetBlomstImage();
                                frøPose.EjedeFelter++;
                                plantFrø.Play();
                                player.Ammunition--;
                            }
                        }
                        else if (player == olieTønde)
                        {
                            //****** SÆT olietønde
                            if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] != Ejerskab.Olie)
                            {
                                if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] == Ejerskab.Blomst)
                                {
                                    frøPose.EjedeFelter--;
                                }

                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Olie;
                                tile.ContentGameImage = GameImages.GetOlieImage();
                                olieDryp.Play();
                                olieTønde.EjedeFelter++;
                                player.Ammunition--;
                            }
                        }
                    }
                }


                //Console.WriteLine(tile);
                //calculate how much move we have left
                int pixelMovesLeft = int.MinValue;
                DirectionChanger deltaMoves = DirectionHelper4.Offsets[player.Direction];
                if (deltaMoves.DeltaX != 0) //we are moving horizontally
                {
                    pixelMovesLeft = Math.Abs(newPosition.X - oldPosition.X);
                }
                else //we are moving vertically
                {
                    pixelMovesLeft = Math.Abs(newPosition.Y - oldPosition.Y);
                }


                //TODO: check whether the wanteddirection is clear
                //
                Direction wantedDirection = player.WantedDirection;
                Direction playerDirection = player.Direction;

                if (wantedDirection != Direction.None)
                {
                    if (!tile.HasBorder(wantedDirection))
                    {
                        player.Direction = wantedDirection;
                        player.Move(player.Direction, pixelMovesLeft);
                    }
                    else
                    {
                        if (!tile.HasBorder(playerDirection))
                        {
                            player.Move(playerDirection, pixelMovesLeft);
                        }
                    }
                }
            }
            else
            {
                player.X = newPosition.X;
                player.Y = newPosition.Y;
            }


            //Console.WriteLine("X: " + player1Position.X + ", Y: " + player1Position.Y);
            //Console.WriteLine("player1Direction: " + player1Direction +  " newPosition: " + newPosition);

        }

        private WalledTile GetNewRefuelPosition()
        {

            WalledTile newPosition = oilTowerTile;
            do
            {
                newPosition = board.Tiles[random.Next(), random.Next()];
            } while (newPosition == oilTowerTile || newPosition == wheelBarrowTile);

            return newPosition;
        }

        public override void OnEntered() 
        {
            Reset();
            m_mainGameTune.Play();

        }
        public override void OnLeft() {
            m_mainGameTune.Stop();

        }
        public override void Reset()
        {
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown];
            wheelBarrowTile = board.Tiles[m_wheelbarrowBeginTilePosition.X, m_wheelbarrowBeginTilePosition.Y];
            oilTowerTile = board.Tiles[m_oilTowerBeginTilePosition.X, m_oilTowerBeginTilePosition.Y];
            wheelBarrow1.SetPosition(wheelBarrowTile.Center);
            oilTower1.SetPosition(oilTowerTile.Center);

            frøPose.Reset();
            olieTønde.Reset();
            board.Reset();
        }


    }
}

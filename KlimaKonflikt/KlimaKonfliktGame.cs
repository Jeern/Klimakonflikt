using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using GameDev.Core.Sequencing;


namespace KlimaKonflikt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class KlimaKonfliktGame : Microsoft.Xna.Framework.Game
    {
        SoundEffectInstance annoyingTunePlayer;

        Rectangle[,] ammoPlacering;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color[] healthColors;
        Texture2D healtBarTexture;

        private AIController m_AIController = new AIController();

        GameBoard board;

        GameImage oilTowerImage1, wheelBarrowImage, completeFloorGameImage;
        Color shadow = new Color(0, 0, 0, .6F);
        KKPlayer fr�Pose, olieT�nde;
        KKMonster m_Ild;

        Color background = new Color(50, 50, 50);
        KeyboardState keyboardState;


        Ejerskab[,] EjerskabsOversigt;
        

        Sprite oilTower1, wheelBarrow1;

        WalledTile oilTowerTile, wheelBarrowTile;

        Texture2D tileFloor;

        Texture2D oilSpill, flower;
        
        SoundEffect plantFr�, olieDryp, fr�Tankning, olieTankning;

        SpriteFont font;

        RealRandom random = new RealRandom(1, 8);
        public KlimaKonfliktGame()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;

            this.graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

        //Texture2D fr�PoseBillede; // , olieT�ndeBillede;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            healtBarTexture = Content.Load<Texture2D>("bar");
            oilSpill = Content.Load<Texture2D>("Olie/ThePatch0030");
            flower = Content.Load<Texture2D>("Blomst/Blomst0030");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileFloor = Content.Load<Texture2D>("64x64");

            GameImage staticFloor = new GameImage(tileFloor);
            


//            fr�PoseBillede = Content.Load<Texture2D>("flowersack");
//            olieT�ndeBillede = Content.Load<Texture2D>("oilbarrel");

            int tilesAcross = 10, tilesDown = 10;
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown];
            IEnumerable<GameBoard> boards = LevelLoader.GetLevels(this, staticFloor, spriteBatch, staticFloor.CurrentTexture.Width);
            //board = new GameBoard(this, staticFloor, spriteBatch, "Board", tilesAcross, tilesDown, 64);
            board = boards.ToArray ()[0];
            
            Texture2D completeFloor = Content.Load<Texture2D>("full_level");
            completeFloorGameImage = new GameImage(completeFloor);
            board.CompleteBackground = completeFloorGameImage;
            board.SetPosition(new Point(200, 50));

            wheelBarrowTile = board.Tiles[5, 5]; 
            oilTowerTile =  board.Tiles[4,4];

            font = Content.Load<SpriteFont>("Arial");
            Texture2D wheelBarrowTexture = Content.Load<Texture2D>("wheelbarrel");
            oilTowerImage1 = GameImages.GetOlieTaarnImage(Content);
            oilTower1 = new Sprite(this, oilTowerImage1, spriteBatch, 0, oilTowerTile.Center);
            oilTower1.GameImageOffset = new Point(0, -20);

            wheelBarrowImage = new GameImage(wheelBarrowTexture);
            
            wheelBarrow1 = new Sprite(this, wheelBarrowImage, spriteBatch, 0, wheelBarrowTile.Center);

            fr�Pose = new KKPlayer(this, GameImages.GetFlowersackImage(Content), spriteBatch, .2F, board.Tiles[9, 9].Center, 10);
            olieT�nde = new KKPlayer(this, GameImages.GetOilBarrelImage(Content), spriteBatch, .2F, board.Tiles[0, 0].Center, 10);
            m_Ild = new KKMonster(this, GameImages.GetIldImage(Content), spriteBatch, .2F, board.Tiles[0, 9].Center, 
                new GameDev.Core.Sequencing.SequencedIterator<Direction>(
                new RandomSequencer(0, 3), Direction.Down, Direction.East, Direction.Left, Direction.North), 500);

            SoundEffect happyGameTune = Content.Load<SoundEffect>(@"GameTunes\klimakonflikt_ingametune");
            annoyingTunePlayer = happyGameTune.CreateInstance();
            annoyingTunePlayer.IsLooped = true;
            annoyingTunePlayer.Play();

            plantFr� = Content.Load<SoundEffect>("froe_plantes");
            olieDryp = Content.Load<SoundEffect>("olieplet_spildes");
            fr�Tankning = Content.Load<SoundEffect>("tankfroe");
            olieTankning = Content.Load<SoundEffect>("tankolie");

            Components.Add(board);

            this.Components.Add(wheelBarrow1);
            this.Components.Add(oilTower1);
            Components.Add(fr�Pose);
            Components.Add(olieT�nde);
            Components.Add(m_Ild);


            //ildImage = GameImages.GetIldImage(Content);
            //ild = new Sprite(this, ildImage, spriteBatch, .3F, board.Tiles[4,4].Center );
            //Components.Add(ild);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (keyboardState.IsArrowKeyDown())
            {
                fr�Pose.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionArrowKeys());
                //Console.WriteLine(player1WantedDirection);
            }

            if (keyboardState.IsWASDKeyDown())
            {
                olieT�nde.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionWASDKeys());
                //Console.WriteLine(player1WantedDirection);
            }


            CalculatePlayerMove(gameTime, fr�Pose);
            CalculatePlayerMove(gameTime, olieT�nde);
            m_AIController.CalculateAIMove(gameTime, board, m_Ild);
            //m_BlomstImage.Update(gameTime);
            //m_OlieImage.Update(gameTime);

            float scoreDifference = fr�Pose.EjedeFelter - olieT�nde.EjedeFelter;
            float scoreDifferenceFactor = Math.Abs(scoreDifference) / 200;
            switch (Math.Sign(scoreDifference))
        	{
                case -1: // fr� er foran
                    fr�Pose.Health -= scoreDifferenceFactor;
                    break;

                case 1: // oliet�nde er foran
                    olieT�nde.Health -= scoreDifferenceFactor;
                    break;
	        }
            float maxSpeed = .4F;
            float lowestHealth = Math.Min(fr�Pose.Health, olieT�nde.Health);
            if (lowestHealth < 50)
            {
                annoyingTunePlayer.Pitch = maxSpeed - (lowestHealth / 50 * maxSpeed);
            }

            base.Update(gameTime);
        }

        private void CalculatePlayerMove(GameTime gameTime, KKPlayer player)
        {

            //if (fr�Pose.Direction == Direction.None)
            //{
            //    fr�Pose.Direction = fr�Pose.WantedDirection;
            //}

            //player1Position.Move(player1WantedDirection, player1Speed * gameTime.ElapsedGameTime.Milliseconds);
            int pixelsToMove = (int)(player.Speed * gameTime.ElapsedGameTime.Milliseconds);
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

                if ((player == olieT�nde && oilTowerTile == tile) || (player == fr�Pose && wheelBarrowTile == tile ))
                {
                    if (player.Ammunition < 10)
                    {

                    
                    player.Ammunition = 10;
                    
                    if (player == olieT�nde)
                    {
                        olieTankning.Play();
                        oilTowerTile = GetNewRefuelPosition();
                        oilTower1.SetPosition(oilTowerTile.Center);
                    }
                    else if (player == fr�Pose)
                    {
                        fr�Tankning.Play();
                        wheelBarrowTile = GetNewRefuelPosition();
                        wheelBarrow1.SetPosition(wheelBarrowTile.Center);
                    }
                    }
                }
                else
                {


                    if (player.Ammunition > 0)
                    {
                        if (player == fr�Pose)
                        {
                            //****** S�T BLOMST
                            if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] != Ejerskab.Blomst)
                            {
                                if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] == Ejerskab.Olie)
                                {
                                    olieT�nde.EjedeFelter--;
                                }
                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Blomst;
                                tile.ContentGameImage = GameImages.GetBlomstImage(Content);
                                fr�Pose.EjedeFelter++;
                                plantFr�.Play();
                                player.Ammunition--;
                            }
                        }
                        else if (player == olieT�nde)
                        {
                            //****** S�T oliet�nde
                            if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] != Ejerskab.Olie)
                            {
                                if (EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] == Ejerskab.Blomst)
                                {
                                    fr�Pose.EjedeFelter--;
                                }

                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Olie;
                                tile.ContentGameImage = GameImages.GetOlieImage(Content);
                                olieDryp.Play();
                                olieT�nde.EjedeFelter++;
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
            } while (newPosition == oilTowerTile || newPosition == wheelBarrowTile );

            return newPosition;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);

            int shadowOffset = 9;
            spriteBatch.DrawString(font, olieT�nde.EjedeFelter.ToString(), new Vector2(20,50),Color.White);
            spriteBatch.DrawString(font, fr�Pose.EjedeFelter.ToString(), new Vector2(910, 50), Color.White);

            for (int y = olieT�nde.Ammunition-1; y >= 0; y--)
            {
                spriteBatch.Draw(oilSpill, ammoPlacering[0, y].GetOffsetCopy(shadowOffset), shadow);
                spriteBatch.Draw(oilSpill, ammoPlacering[0, y], Color.White);
            }
            for (int y = fr�Pose.Ammunition-1; y >= 0; y--)
            {
                spriteBatch.Draw(flower, ammoPlacering[1, y].GetOffsetCopy(shadowOffset), shadow);
                spriteBatch.Draw(flower, ammoPlacering[1, y], Color.White);
            }

            int olieBarRectangleHeight = (int)(640 * olieT�nde.Health / 100);
            int fr�PoseRectangleHeight = (int)(640 * fr�Pose.Health / 100);

            Rectangle olieBarRectangle = new Rectangle(140, 700 - olieBarRectangleHeight, healtBarTexture.Width, olieBarRectangleHeight);
            Rectangle fr�PoseBarRectangle = new Rectangle(850, 700 - fr�PoseRectangleHeight, healtBarTexture.Width, fr�PoseRectangleHeight);
            
            int fr�PoseHealthBarIndex = (int)(fr�Pose.Health / 20);
            int olieT�ndeHealthBarIndex = (int)(olieT�nde.Health / 20);

            if (fr�PoseHealthBarIndex > 4) fr�PoseHealthBarIndex = 4;
            if (olieT�ndeHealthBarIndex > 4) olieT�ndeHealthBarIndex = 4;

            spriteBatch.Draw(healtBarTexture, olieBarRectangle, healthColors[olieT�ndeHealthBarIndex]);
            spriteBatch.Draw(healtBarTexture, fr�PoseBarRectangle, healthColors[fr�PoseHealthBarIndex]);
            spriteBatch.End();
        }
    }
}

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

        private AIController m_AIController = new AIController();

        GameBoard board;

        GameImage oilTowerImage1, wheelBarrowImage, completeFloorGameImage, ildImage;

        KKPlayer frøPose, olieTønde;
        KKMonster m_Ild;

        Color background = new Color(50, 50, 50);
        KeyboardState keyboardState;

        Ejerskab[,] EjerskabsOversigt;
        int antalEjetAfFrøpose, antalEjetAfOlietønde;

        Sprite oilTower1, wheelBarrow1, ild;

        WalledTile oilTowerTile, wheelBarrowTile;

        Texture2D tileFloor;

        Texture2D oilSpill, flower;
        
        SoundEffect plantFrø, olieDryp, frøTankning, olieTankning;

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

            base.Initialize();
            ammoPlacering = new Rectangle[2, 10];
            int ammoSize = 100;
            int ammoBottomOffset = 600;
            for (int y = 0; y < 10; y++)
            {
                ammoPlacering[0, y] = new Rectangle(50, ammoBottomOffset - 50 * y, ammoSize, ammoSize);
                ammoPlacering[1, y] = new Rectangle(880, ammoBottomOffset - 50 * y, ammoSize, ammoSize);
            }

        }

        //Texture2D frøPoseBillede; // , olieTøndeBillede;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            oilSpill = Content.Load<Texture2D>("Olie/ThePatch0030");
            flower = Content.Load<Texture2D>("Blomst/Blomst0030");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileFloor = Content.Load<Texture2D>("64x64");

            GameImage staticFloor = new GameImage(tileFloor);
            


//            frøPoseBillede = Content.Load<Texture2D>("flowersack");
//            olieTøndeBillede = Content.Load<Texture2D>("oilbarrel");

            int tilesAcross = 10, tilesDown = 10;
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown];
            IEnumerable<GameBoard> boards = LevelLoader.GetLevels(this, staticFloor, spriteBatch, staticFloor.CurrentTexture.Width);
            //board = new GameBoard(this, staticFloor, spriteBatch, "Board", tilesAcross, tilesDown, 64);
            board = boards.ToArray ()[0];
            
            Texture2D completeFloor = Content.Load<Texture2D>("full_level");
            completeFloorGameImage = new GameImage(completeFloor);
            board.CompleteBackground = completeFloorGameImage;
            board.SetPosition(new Point(180, 50));

            wheelBarrowTile = board.Tiles[5, 5]; 
            oilTowerTile =  board.Tiles[4,4];

            font = Content.Load<SpriteFont>("Arial");
            Texture2D wheelBarrowTexture = Content.Load<Texture2D>("wheelbarrel");
            oilTowerImage1 = GameImages.GetOlieTaarnImage(Content);
            oilTower1 = new Sprite(this, oilTowerImage1, spriteBatch, 0, oilTowerTile.Center);
            oilTower1.GameImageOffset = new Point(0, -20);

            wheelBarrowImage = new GameImage(wheelBarrowTexture);
            
            wheelBarrow1 = new Sprite(this, wheelBarrowImage, spriteBatch, 0, wheelBarrowTile.Center);

            frøPose = new KKPlayer(this, GameImages.GetFlowersackImage(Content), spriteBatch, .2F, board.Tiles[9, 9].Center, 10);
            olieTønde = new KKPlayer(this, GameImages.GetOilBarrelImage(Content), spriteBatch, .2F, board.Tiles[0, 0].Center, 10);
            m_Ild = new KKMonster(this, GameImages.GetIldImage(Content), spriteBatch, .2F, board.Tiles[0, 9].Center, 10);

            SoundEffect happyGameTune = Content.Load<SoundEffect>(@"GameTunes\klimakonflikt_ingametune");
            annoyingTunePlayer = happyGameTune.CreateInstance();
            annoyingTunePlayer.IsLooped = true;
            annoyingTunePlayer.Play();

            plantFrø = Content.Load<SoundEffect>("froe_plantes");
            olieDryp = Content.Load<SoundEffect>("olieplet_spildes");
            frøTankning = Content.Load<SoundEffect>("tankfroe");
            olieTankning = Content.Load<SoundEffect>("tankolie");

            Components.Add(board);

            this.Components.Add(wheelBarrow1);
            this.Components.Add(oilTower1);
            Components.Add(frøPose);
            Components.Add(olieTønde);
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
                frøPose.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionArrowKeys());
                //Console.WriteLine(player1WantedDirection);
            }

            if (keyboardState.IsWASDKeyDown())
            {
                olieTønde.WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirectionWASDKeys());
                //Console.WriteLine(player1WantedDirection);
            }


            CalculatePlayerMove(gameTime, frøPose);
            CalculatePlayerMove(gameTime, olieTønde);
            m_AIController.CalculateAIMove(gameTime, board, m_Ild);
            //m_BlomstImage.Update(gameTime);
            //m_OlieImage.Update(gameTime);

            base.Update(gameTime);
        }

        private void CalculatePlayerMove(GameTime gameTime, KKPlayer player)
        {

            //if (frøPose.Direction == Direction.None)
            //{
            //    frøPose.Direction = frøPose.WantedDirection;
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
                                    antalEjetAfOlietønde--;
                                }
                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Blomst;
                                tile.ContentGameImage = GameImages.GetBlomstImage(Content);
                                antalEjetAfFrøpose++;
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
                                    antalEjetAfFrøpose--;
                                }

                                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Olie;
                                tile.ContentGameImage = GameImages.GetOlieImage(Content);
                                olieDryp.Play();
                                antalEjetAfOlietønde++;
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


            spriteBatch.DrawString(font, "Points: " + antalEjetAfOlietønde, new Vector2(20,50),Color.White);
            spriteBatch.DrawString(font, "Points: " + antalEjetAfFrøpose, new Vector2(870, 50), Color.White);

            for (int y = olieTønde.Ammunition-1; y >= 0; y--)
            {
                spriteBatch.Draw(oilSpill, ammoPlacering[0, y], Color.White);


            }
            for (int y = frøPose.Ammunition-1; y >= 0; y--)
            {
                spriteBatch.Draw(flower, ammoPlacering[1, y], Color.White);
            }
            spriteBatch.End();
        }

    }
}

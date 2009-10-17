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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameBoard board;

        GameImage oilTowerImage, wheelBarrowImage;

        //private GameImage m_BlomstImage;
        //private GameImage m_OlieImage;

        KKPlayer frøPose, olieTønde;

        KeyboardState keyboardState;
        //Placeable player1Position;
        //float player1Speed = 0.3F;
        //Direction player1Direction, player1WantedDirection;

        Ejerskab[,] EjerskabsOversigt;
        int antalEjetAfFrøpose, antalEjetAfOlietønde;

        Sprite oilTower1, oilTower2, wheelBarrow1, wheelBarrow2;

        List<WalledTile> oilTowerTiles, wheelBarrowTiles;

        Texture2D tileFloor;

        SoundEffect plantFrø, olieDryp, frøTankning, olieTankning;

        public KlimaKonfliktGame()
        {


            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;

            //this.graphics.IsFullScreen = true;


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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileFloor = Content.Load<Texture2D>("64x64");

            GameImage staticFloor = new GameImage(tileFloor);

            Texture2D frøPoseBillede = Content.Load<Texture2D>("flowersack");
            Texture2D olieTøndeBillede = Content.Load<Texture2D>("oilbarrel");

            int tilesAcross = 10, tilesDown = 10;
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown];
            board = new GameBoard(this, staticFloor, spriteBatch, "Board", tilesAcross, tilesDown, 64);
            board.SetPosition(new Point(100, 0));

            wheelBarrowTiles = new List<WalledTile>();
            oilTowerTiles = new List<WalledTile>();

            wheelBarrowTiles.Add(board.Tiles[5, 0]);
            wheelBarrowTiles.Add(board.Tiles[4, 9]);

            oilTowerTiles.Add( board.Tiles[0, 5]);
            oilTowerTiles.Add( board.Tiles[9, 4]);


            Texture2D oilTowerTexture = Content.Load<Texture2D>("oil_tower");
            Texture2D wheelBarrowTexture = Content.Load<Texture2D>("wheelbarrel");


            oilTowerImage = new GameImage(oilTowerTexture);
            oilTower1 = new Sprite(this, oilTowerImage, spriteBatch, 0, oilTowerTiles[0].Center);
            
            oilTower2 = new Sprite(this, oilTowerImage, spriteBatch, 0, oilTowerTiles[1].Center);

            wheelBarrowImage = new GameImage(wheelBarrowTexture);
            
            
            wheelBarrow1 = new Sprite(this, wheelBarrowImage, spriteBatch, 0, wheelBarrowTiles[0].Center);
            wheelBarrow2 = new Sprite(this, wheelBarrowImage, spriteBatch, 0, wheelBarrowTiles[1].Center);



            frøPose = new KKPlayer(this, new GameImage(frøPoseBillede), spriteBatch, .2F, board.Tiles[9, 9].Center, 10);
            olieTønde = new KKPlayer(this, new GameImage(olieTøndeBillede), spriteBatch, .2F, board.Tiles[0, 0].Center, 10);


            plantFrø = Content.Load<SoundEffect>("froe_plantes");
            olieDryp = Content.Load<SoundEffect>("olieplet_spildes");
            frøTankning = Content.Load<SoundEffect>("tankfroe");
            olieTankning = Content.Load<SoundEffect>("tankolie");



            //m_BlomstImage = GameImages.GetBlomstImage(Content);
            //m_OlieImage = GameImages.GetOlieImage(Content);

            // TODO: use this.Content to load your game content here
            
            Components.Add(board);

            this.Components.Add(wheelBarrow1);
            this.Components.Add(wheelBarrow2);
            this.Components.Add(oilTower1);
            this.Components.Add(oilTower2);
            Components.Add(frøPose);
            Components.Add(olieTønde);


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
            //m_BlomstImage.Update(gameTime);
            //m_OlieImage.Update(gameTime);

            base.Update(gameTime);
        }

        private void CalculatePlayerMove(GameTime gameTime, KKPlayer player)
        {

            if (frøPose.Direction == Direction.None)
            {
                frøPose.Direction = frøPose.WantedDirection;
            }

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

                if ((player == olieTønde && oilTowerTiles.Contains(tile)) || (player == frøPose && wheelBarrowTiles.Contains(tile)))
                {
                    if (player.Ammunition < 10)
                    {

                    
                    player.Ammunition = 10;

                    if (player == olieTønde)
                    {
                        olieTankning.Play();
                    }
                    else if (player == frøPose)
                    {
                        frøTankning.Play();
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);


            //spriteBatch.DrawString(
            //spriteBatch.Draw(frøPose.GameImage.CurrentTexture, new Rectangle(frøPose.X - frøPose.GameImage.CurrentTexture.Width / 2, frøPose.Y - frøPose.GameImage.CurrentTexture.Height / 2, frøPose.GameImage.CurrentTexture.Width, frøPose.GameImage.CurrentTexture.Height), Color.White);
            //spriteBatch.Draw(olieTønde.GameImage.CurrentTexture, new Rectangle(olieTønde.X - olieTønde.GameImage.CurrentTexture.Width / 2, olieTønde.Y - olieTønde.GameImage.CurrentTexture.Height / 2, olieTønde.GameImage.CurrentTexture.Width, olieTønde.GameImage.CurrentTexture.Height), Color.White);
            //spriteBatch.Draw(m_BlomstImage.CurrentTexture, new Rectangle(200, 200, 40, 40), Color.White);
            //spriteBatch.Draw(m_OlieImage.CurrentTexture, new Rectangle(260, 260, 40, 40), Color.White);
            spriteBatch.End();
        }
    }
}

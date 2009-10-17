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

        //private GameImage m_BlomstImage;
        //private GameImage m_OlieImage;

        KeyboardState keyboardState;
        Placeable player1Position;
        float player1Speed = 0.3F;
        Direction player1Direction, player1WantedDirection;

        Ejerskab[,] EjerskabsOversigt;

        Texture2D tileFloor, player1;

        

        public KlimaKonfliktGame()
        {
            player1Position = new Placeable(this);
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth =1024;
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
            
            

            //m_BlomstImage = GameImages.GetBlomstImage(Content);
            //m_OlieImage = GameImages.GetOlieImage(Content);

            // TODO: use this.Content to load your game content here
            tileFloor = Content.Load<Texture2D>("64x64");

            GameImage staticFloor = new GameImage(tileFloor);
            player1 = Content.Load<Texture2D>("flowersack");
            int tilesAcross = 10, tilesDown = 10;
            EjerskabsOversigt = new Ejerskab[tilesAcross, tilesDown]; 
            board = new GameBoard(this, staticFloor, spriteBatch, "Board", tilesAcross,tilesDown,64);
            Components.Add(board);
            player1Position.SetPosition(board.Tiles[0, 0].Center);
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


                player1WantedDirection = DirectionHelper4.LimitDirection(keyboardState.GetDirection());
                //Console.WriteLine(player1WantedDirection);
            }
            CalculatePlayer1sMove(gameTime);
            //m_BlomstImage.Update(gameTime);
            //m_OlieImage.Update(gameTime);

            base.Update(gameTime);
        }

        private void CalculatePlayer1sMove(GameTime gameTime)
        {

            if (player1Direction == Direction.None)
            {
                player1Direction = player1WantedDirection;
            }
            
            //player1Position.Move(player1WantedDirection, player1Speed * gameTime.ElapsedGameTime.Milliseconds);
            int pixelsToMove = (int)(player1Speed * gameTime.ElapsedGameTime.Milliseconds);
            Point newPosition = player1Position.GetNewPosition( player1Direction, pixelsToMove);
            Point oldPosition = player1Position.GetPosition();
            //Console.WriteLine("Dir: " + player1Direction + ", wanted: " + player1WantedDirection);
            
            Point centerOfPlayersTile = board.GetTileFromPixelPosition(player1Position.X, player1Position.Y).Center;
            WalledTile tile = board.GetTileFromPixelPosition(player1Position.GetPosition());
            

            if (GeometryTools.IsBetweenPoints(centerOfPlayersTile, newPosition, oldPosition))
            {
                //Console.WriteLine("BETWEEN");
                //we are going to cross the center
                //first move to center
                Point tempPosition = centerOfPlayersTile;
                player1Position.SetPosition(centerOfPlayersTile);

                //****** SÆT BLOMST
                EjerskabsOversigt[tile.HorizontalIndex, tile.VerticalIndex] = Ejerskab.Blomst;
                tile.ContentGameImage = GameImages.GetBlomstImage(Content);


                Console.WriteLine(tile);
                //calculate how much move we have left
                int pixelMovesLeft = int.MinValue;
                DirectionChanger deltaMoves = DirectionHelper4.Offsets[player1Direction];
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
                if (player1WantedDirection != Direction.None)
                {
                    if (!tile.HasBorder(player1WantedDirection))
                    {
                        player1Direction = player1WantedDirection;
                        player1Position.Move(player1Direction, pixelMovesLeft);
                    }
                    else
                    {
                        if (!tile.HasBorder(player1Direction))
                        {
                            player1Position.Move(player1Direction, pixelMovesLeft);
                        }
                    }
                }
            }
            else
            {
                player1Position.X = newPosition.X;
                player1Position.Y = newPosition.Y;

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
            spriteBatch.Draw(player1, new Rectangle(player1Position.X - player1.Width/2, player1Position.Y - player1.Height /2, player1.Width, player1.Height), Color.White);
            //spriteBatch.Draw(m_BlomstImage.CurrentTexture, new Rectangle(200, 200, 40, 40), Color.White);
            //spriteBatch.Draw(m_OlieImage.CurrentTexture, new Rectangle(260, 260, 40, 40), Color.White);
            spriteBatch.End();
        }
    }
}

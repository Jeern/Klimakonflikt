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

        KeyboardState keyboardState;
        Placeable player1Position;
        int player1Speed = 1;
        Direction player1Direction, player1WantedDirection;

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

            // TODO: use this.Content to load your game content here
            tileFloor = Content.Load<Texture2D>("64x64");
            player1 = Content.Load<Texture2D>("crosshair");
            board = new GameBoard(this, tileFloor, spriteBatch);
            Components.Add(board);
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


            player1WantedDirection = keyboardState.GetDirection();
            //if(keyboardState.IsArrowKeyDown()) player1WantedDirection = DirectionHelper4.GetDirection(
            //if (keyboardState.IsKeyDown(Keys.Up)) { player1WantedDirection = DirectionHelper4.KeyboardDirections[Keys.Up]; }
            //if (keyboardState.IsKeyDown(Keys.Right)) { player1WantedDirection = DirectionHelper4.KeyboardDirections[Keys.Right]; }
            //if (keyboardState.IsKeyDown(Keys.Down)) { player1WantedDirection = DirectionHelper4.KeyboardDirections[Keys.Down]; }
            //if (keyboardState.IsKeyDown(Keys.Left)) { player1WantedDirection = DirectionHelper4.KeyboardDirections[Keys.Left]; }

            CalculatePlayer1sMove(gameTime);

            base.Update(gameTime);
        }

        private void CalculatePlayer1sMove(GameTime gameTime)
        {
            player1Position.Move(player1WantedDirection, player1Speed * gameTime.ElapsedGameTime.Milliseconds);
            Point newPosition = DirectionHelper4.GetNewPosition(player1Position, player1Direction);
            
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
            spriteBatch.Draw(player1, new Rectangle(player1Position.X, player1Position.Y, player1.Width, player1.Height), Color.White);
            spriteBatch.End();
        }
    }
}

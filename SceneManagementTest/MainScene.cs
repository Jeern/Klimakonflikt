using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using Microsoft.Xna.Framework.Input;

namespace SceneManagementTest
{
    public class MainScene : Scene
    {
        Texture2D indie;
        Vector2 position;

        public MainScene() : base("Main")
        {
            
            position = new Vector2(200, 200);
            indie = Game.Content.Load<Texture2D>("Indie9000");   
        }

        public override void Draw(GameTime gameTime)
        {
            if (!IsPaused)
            {
                
                SpriteBatch.Begin();

                SpriteBatch.Draw(indie,new Vector2(100,100), Color.Wheat);

                SpriteBatch.End();
            }
            
        }

        public override void Initialize()
        {
          
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left))
            {
                position.X += 4;
            }

            // TODO: Add your update logic here
        }

        public override void OnEntered()
        {
            throw new NotImplementedException();
        }
        public override void OnLeft()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;

namespace SceneManagementTest
{
    public class StaticImageScene : Scene
    {
        protected GameImage BackgroundImage { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        public StaticImageScene(Game game, string sceneName, Texture2D backgroundTexture) : this(game, sceneName, new GameImage(backgroundTexture)) {}

        public StaticImageScene(Game game, string sceneName, GameImage backgroundGameImage) : base(sceneName, game)
        {
            this.BackgroundImage = backgroundGameImage;
        }

        public override void Draw(GameTime gameTime)
        {
            if (DestinationRectangle == Rectangle.Empty)
            {
                DestinationRectangle = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea;
            }
                SpriteBatch.Begin();
                SpriteBatch.Draw(BackgroundImage.CurrentTexture, DestinationRectangle, Color.White);
                SpriteBatch.End();
        }

        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}

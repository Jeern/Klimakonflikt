using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev.Core;
using GameDev.Core.Graphics;


namespace GameDev.Core.SceneManagement
{
    public class StaticImageScene : Scene
    {
        protected GameImage BackgroundImage { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        public StaticImageScene(string sceneName, Texture2D backgroundTexture) : this(sceneName, new GameImage(backgroundTexture)) {}

        public StaticImageScene(string sceneName, GameImage backgroundGameImage) : base(sceneName)
        {
            this.BackgroundImage = backgroundGameImage;
        }

        public StaticImageScene(string sceneName, string nameOfTextureResource)
            : this( sceneName, new GameImage(GameDevGame.Current.Content.Load<Texture2D>(nameOfTextureResource))) {}


        public override void OnEntered()
        {
        }

        public override void OnLeft()
        {
        }
        
        public override void Draw(GameTime gameTime)
        {
            if (DestinationRectangle == Rectangle.Empty)
            {
                DestinationRectangle = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea;
            }
                Game.SpriteBatch.Begin();
                Game.SpriteBatch.Draw(BackgroundImage.CurrentTexture, DestinationRectangle, Color.White);
                Game.SpriteBatch.End();
        }

        public override void Reset()
        {
            //TODO: implement reset on GameImage
            //this.BackgroundImage.Reset();
        }

    }
}

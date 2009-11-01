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

        public StaticImageScene(string sceneName, Color backgroundColor) : base (sceneName)
        {
            this.BackgroundColor = backgroundColor;
        }

        public StaticImageScene(string sceneName, Texture2D backgroundTexture) : this(sceneName, new GameImage(backgroundTexture)) {}

        public StaticImageScene(string sceneName, GameImage backgroundGameImage) : this(sceneName, Color.Black)
        {
            this.BackgroundImage = backgroundGameImage;
        }

        public StaticImageScene(string sceneName, string nameOfTextureResource)
            : this( sceneName, new GameImage(GameDevGame.Current.Content.Load<Texture2D>(nameOfTextureResource))) {}


        public override void Draw(GameTime gameTime)
        {
            if (DestinationRectangle == Rectangle.Empty)
            {
                if (GameDevGame.Current.GraphicsDeviceManager.IsFullScreen)
                {
                    DestinationRectangle = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea;
                }
                else
                {
                    Rectangle bounds = GameDevGame.Current.Window.ClientBounds;
                    bounds.Offset(- bounds.Left, - bounds.Top);
                    DestinationRectangle = bounds;
                }

                
            }
                GameDevGame.Current.SpriteBatch.Begin();
                if (BackgroundImage != null)
                {
                    GameDevGame.Current.SpriteBatch.Draw(BackgroundImage.CurrentTexture, DestinationRectangle, Color.White);
                }
                else
                {
                    GameDevGame.Current.GraphicsDevice.Clear(BackgroundColor);
                }
                foreach (DrawableGameComponent component in Components)
                {
                    component.Draw(gameTime);
                }
                
                GameDevGame.Current.SpriteBatch.End();
        }

        public override void Reset()
        {
            //TODO: implement reset on GameImage
            //this.BackgroundImage.Reset();
        }

    }
}

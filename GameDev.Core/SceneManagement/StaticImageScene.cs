using GameDev.Core.Graphics;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

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
#if SILVERLIGHT
                    Rectangle bounds = GameDevGame.Current.Window.ClientBounds;
                    bounds.Offset(- bounds.Left, - bounds.Top);
                    DestinationRectangle = bounds;
#else
                //if (GameDevGame.Current.GraphicsDeviceManager.IsFullScreen)
                //{
                //    DestinationRectangle = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.TitleSafeArea;

                //}
                //else
                //{
                    Rectangle bounds = GameDevGame.Current.Window.ClientBounds;
                    bounds.Offset(- bounds.Left, - bounds.Top);
                    DestinationRectangle = bounds;
//                }
#endif

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

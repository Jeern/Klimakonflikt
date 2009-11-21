using GameDev.Core;
using GameDev.Core.SceneManagement;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Audio;
using SilverArcade.SilverSprite.Input;
using SilverlightHelpers;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
#endif

namespace KlimaKonflikt.Scenes
{
    public class IntroScene : StaticImageScene
    {
        SoundEffectInstance m_introgametune;
        
        public IntroScene() : base (SceneNames.INTROSCENE, @"SceneBackdrops\Intro")
        {
            SoundEffect effect = GameDevGame.Current.Content.Load<SoundEffect>(@"GameTunes\IntroTune");
            m_introgametune = effect.CreateInstance();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (UpdatedKeyboardState.IsKeyDown(Keys.Enter))
            {
                SceneManager.ChangeScene(SceneNames.MENUSCENE);
            }

            if (UpdatedKeyboardState.IsKeyDown(Keys.Escape))
            {
                GameDevGame.Current.Exit();
            }

        }

        public override void OnEntered()
        {
            base.OnEntered();
            m_introgametune.Play();
        }

        public override void OnLeft()
        {
            base.OnLeft();
            m_introgametune.Stop();
        }
    }
}

using GameDev.Core;
using GameDev.Core.SceneManagement;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Audio;
using SilverlightHelpers;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
#endif

namespace KlimaKonflikt.Scenes
{
    public class InstructionsScene : StaticImageScene
    {

        SoundEffectInstance m_creditsTune;

        
        public InstructionsScene()
            : base(SceneNames.INSTRUCTIONSSCENE, @"SceneBackdrops\Instructions")
        {
            SoundEffect effect = GameDevGame.Current.Content.Load<SoundEffect>(@"GameTunes\CreditsTune");
            m_creditsTune = effect.CreateInstance();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (NoKeysPressed)
            {
                if (UpdatedKeyboardState.GetPressedKeys().Length > 0)
                {
                    SceneManager.ChangeScene(SceneNames.MENUSCENE);
                } 
            }

        }

        public override void OnEntered()
        {
            base.OnEntered();
            m_creditsTune.Play();
        }

        public override void OnLeft()
        {
            base.OnLeft();
            m_creditsTune.Stop();
        }
    }
}

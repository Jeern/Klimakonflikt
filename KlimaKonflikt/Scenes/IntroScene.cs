#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

	#endregion

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

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

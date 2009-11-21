﻿using GameDev.Core.SceneManagement;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Input;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endif

namespace KlimaKonflikt.Scenes
{
    public class OilWinScene : StaticImageScene
    {
        
        public OilWinScene() : base (SceneNames.OILWINSCENE, @"SceneBackdrops\Oil_win")
        {}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (NoKeysPressed)
            {
                if (UpdatedKeyboardState.IsKeyDown(Keys.Enter))
                {
                    SceneManager.ChangeScene(SceneNames.MENUSCENE);
                }
                else if (UpdatedKeyboardState.IsKeyDown(Keys.Escape))
                {
                    SceneManager.ChangeScene(SceneNames.CREDITSSCENE);
                }
            }

        }
    }
}

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
    public class OilWinScene : StaticImageScene
    {
        
        public OilWinScene() : base (SceneNames.OILWINSCENE, @"SceneBackdrops\Oil_win")
        {}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


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

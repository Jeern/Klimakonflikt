﻿#region Usings
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
    public class FlowerWinScene :  SceneBase
    {

        public FlowerWinScene()
            : base(SceneNames.FLOWERWINSCENE, @"SceneBackdrops\Flower_win")
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (state.IsKeyDown(Keys.Enter))
            {
                SceneManager.ChangeScene(SceneNames.MAINSCENE);
            }
            else if (state.IsKeyDown(Keys.Escape))
            {
                SceneManager.ChangeScene(SceneNames.CREDITSSCENE);
            }

        }
    }
}
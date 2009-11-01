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
    public class KKMenuScene :  MenuScene
    {

        SoundEffectInstance m_creditsTune;

        
        public KKMenuScene()
            : base(SceneNames.MENUSCENE, new GameMenu(), Color.Black)
        {
            SoundEffect effect = GameDevGame.Current.Content.Load<SoundEffect>(@"GameTunes\CreditsTune");
            m_creditsTune = effect.CreateInstance();
            this.Menu.MenuItemActivated += new GameDev.Core.Menus.MenuItemHandler(Menu_MenuItemActivated);
        }

        void Menu_MenuItemActivated(GameDev.Core.Menus.MenuItem sender, EventArgs e)
        {

            switch (sender.Name)
            {

                case GameMenu.MENU_STARTSINGLEPLAYER:
                    ((MainScene)SceneManager.GetScene(SceneNames.MAINSCENE)).SinglePlayer = true;
                    SceneManager.ChangeScene(SceneNames.MAINSCENE);

                    break;

                case GameMenu.MENU_START2PLAYER:

                    ((MainScene)SceneManager.GetScene(SceneNames.MAINSCENE)).SinglePlayer = false;
                    SceneManager.ChangeScene(SceneNames.MAINSCENE);
                    break;

                case GameMenu.MENU_EXIT:
                    GameDevGame.Current.Exit();
                    break;

                case GameMenu.MENU_TOGGLEFULLSCREEN:
                    GameDevGame.Current.GraphicsDeviceManager.ToggleFullScreen();
                    break;

                case GameMenu.MENU_CREDITS:
                    SceneManager.Current.ChangeScene(SceneNames.CREDITSSCENE);
                    break;

                case GameMenu.MENU_INSTRUCTIONS:
                    SceneManager.Current.ChangeScene(SceneNames.INSTRUCTIONSSCENE);
                    break;
            }
	}


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (NoKeysPressed)
            {if(UpdatedKeyboardState.IsKeyDown(Keys.Escape))
                {
                    GameDevGame.Current.Exit();  
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

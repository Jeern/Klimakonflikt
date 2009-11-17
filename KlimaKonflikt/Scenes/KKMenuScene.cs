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
using GameDev.Core.Menus;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

	#endregion

namespace KlimaKonflikt.Scenes
{
    public class KKMenuScene :  MenuScene
    {

        SoundEffectInstance m_creditsTune;

        
        public KKMenuScene()
            : base(SceneNames.MENUSCENE, new KKGameMenu(), Color.Black)
        {
            SoundEffect effect = GameDevGame.Current.Content.Load<SoundEffect>(@"GameTunes\CreditsTune");
            m_creditsTune = effect.CreateInstance();
            this.Menu.MenuItemActivated += new GameDev.Core.Menus.MenuItemHandler(Menu_MenuItemActivated);
        }

        void Menu_MenuItemActivated(GameDev.Core.Menus.MenuItem sender, EventArgs e)
        {
            int numberOfWinsToWinGame = ((SliderMenuItem)Menu.GetMenuItem(KKGameMenu.MENU_NUMBEROFWINS)).CurrentValue;

            switch (sender.Name)
            {

                case KKGameMenu.MENU_STARTSINGLEPLAYER:
                    MainScene mainScene = (MainScene)SceneManager.GetScene(SceneNames.MAINSCENE);
                       mainScene.SinglePlayer = true;
                    mainScene.NumberOfWinsToWinGame = numberOfWinsToWinGame;
                    SceneManager.ChangeScene(mainScene);
                    break;

                case KKGameMenu.MENU_START2PLAYER:
                    mainScene = (MainScene)SceneManager.GetScene(SceneNames.MAINSCENE);
                    mainScene.SinglePlayer = false;
                    mainScene.NumberOfWinsToWinGame = numberOfWinsToWinGame;
                    SceneManager.ChangeScene(mainScene);
                    break;

                case KKGameMenu.MENU_EXIT:
                    GameDevGame.Current.Exit();
                    break;

                case KKGameMenu.MENU_TOGGLEFULLSCREEN:
                    GameDevGame.Current.GraphicsDeviceManager.ToggleFullScreen();
                    break;

                case KKGameMenu.MENU_CREDITS:
                    SceneManager.Current.ChangeScene(SceneNames.CREDITSSCENE);
                    break;

                case KKGameMenu.MENU_INSTRUCTIONS:
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

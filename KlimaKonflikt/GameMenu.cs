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
using GameDev.Core.Menus;
using KlimaKonflikt.Scenes;

#endregion

namespace KlimaKonflikt
{
   public class GameMenu :  MenuBase
    {


       public static readonly string MENU_STARTSINGLEPLAYER = "Start single player game"; 
       public static readonly string MENU_START2PLAYER = "Start two player game";
       public static readonly string MENU_INSTRUCTIONS = "Instructions";
       public static readonly string MENU_CREDITS = "Credits";
       public static readonly string MENU_EXIT = "Exit game";
       int topMargin = 150;

       SpriteFont menuFont;
       TextMenuItem mnuStart1PlayerGame , mnuStart2PlayerGame, mnuInstructions, mnuCredits, mnuExit;
       public GameMenu()
       {
           menuFont = GameDevGame.Current.Content.Load<SpriteFont>("MenuText");

           //mnuStart1PlayerGame = new TextMenuItem(MENU_STARTSINGLEPLAYER, menuFont, MENU_STARTSINGLEPLAYER);
           mnuStart2PlayerGame = new TextMenuItem(MENU_START2PLAYER, menuFont, MENU_START2PLAYER);
           mnuInstructions = new TextMenuItem(MENU_INSTRUCTIONS, menuFont, MENU_INSTRUCTIONS);
           mnuCredits = new TextMenuItem(MENU_CREDITS, menuFont, MENU_CREDITS);
           mnuExit = new TextMenuItem(MENU_EXIT, menuFont, MENU_EXIT);

           //this.AddMenuItem(mnuStart1PlayerGame);
           this.AddMenuItem(mnuStart2PlayerGame);
           this.AddMenuItem(mnuInstructions);
           this.AddMenuItem(mnuCredits);
           this.AddMenuItem(mnuExit);

           //mnuStart1PlayerGame.Selectable = false;
           mnuStart2PlayerGame.Activated += new MenuItemHandler(mnuStart2PlayerGame_Activated);
           mnuCredits.Activated += new MenuItemHandler(mnuCredits_Activated);
           mnuExit.Activated += new MenuItemHandler(mnuExit_Activated);
       }


           void mnuExit_Activated(MenuItem sender, EventArgs e)
           {
               GameDevGame.Current.Exit();
           }

           void mnuCredits_Activated(MenuItem sender, EventArgs e)
           {
               SceneManager.Current.ChangeScene(SceneNames.CREDITSSCENE);
           }

           void mnuStart2PlayerGame_Activated(MenuItem sender, EventArgs e)
           {
               SceneManager.Current.ChangeScene(SceneNames.MAINSCENE);
           }

        public override void ArrangeMenuItems()
        {
            int counter = 0;
            foreach (MenuItem  item in m_menuItems)
            {
                item.Position = new Vector2(200, topMargin + 100 * counter);
                counter++;
            }
            

        }
    }
}

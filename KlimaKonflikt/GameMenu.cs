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


       public const string MENU_STARTSINGLEPLAYER = "Start single player game";
       public const string MENU_START2PLAYER = "Start two player game";
       public const string MENU_INSTRUCTIONS = "Instructions";
       public const string MENU_CREDITS = "Credits";
       public const string MENU_TOGGLEFULLSCREEN = "Toggle fullscreen";
       public const string MENU_EXIT = "Exit game";
       
       int topMargin = 150;

       SpriteFont menuFont;
       TextMenuItem mnuStart1PlayerGame , mnuStart2PlayerGame, mnuInstructions, mnuCredits,mnuToggleFullscreen, mnuExit;
       public GameMenu()
       {
           menuFont = GameDevGame.Current.Content.Load<SpriteFont>("MenuText");

           mnuStart1PlayerGame = new TextMenuItem(MENU_STARTSINGLEPLAYER, menuFont, MENU_STARTSINGLEPLAYER);
           mnuStart2PlayerGame = new TextMenuItem(MENU_START2PLAYER, menuFont);
           mnuInstructions = new TextMenuItem(MENU_INSTRUCTIONS, menuFont);
           mnuCredits = new TextMenuItem(MENU_CREDITS, menuFont);
           mnuToggleFullscreen = new TextMenuItem(MENU_TOGGLEFULLSCREEN, menuFont);
           mnuExit = new TextMenuItem(MENU_EXIT, menuFont);

           this.AddMenuItem(mnuStart1PlayerGame);
           this.AddMenuItem(mnuStart2PlayerGame);
           this.AddMenuItem(mnuInstructions);
           this.AddMenuItem(mnuCredits);
           this.AddMenuItem(mnuToggleFullscreen);
           this.AddMenuItem(mnuExit);

       }


        public override void ArrangeMenuItems()
        {
            int counter = 0;
            foreach (MenuItem  item in m_menuItems)
            {
                item.Position = new Vector2(200, topMargin + 70 * counter);
                counter++;
            }
            

        }
    }
}

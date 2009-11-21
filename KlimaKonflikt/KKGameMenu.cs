using GameDev.Core;
using GameDev.Core.Menus;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace KlimaKonflikt
{
   public class KKGameMenu :  MenuBase
    {


       public const string MENU_STARTSINGLEPLAYER = "Start single player game";
       public const string MENU_START2PLAYER = "Start two player game";
       public const string MENU_LETHALFIRE = "Fire is lethal";
       public const string MENU_INSTRUCTIONS = "Instructions";
       public const string MENU_CREDITS = "Credits";
       public const string MENU_TOGGLEFULLSCREEN = "Toggle fullscreen";
       public const string MENU_NUMBEROFWINS = "Number of wins";
       public const string MENU_EXIT = "Exit game";
       
       int topMargin = 130;

       SpriteFont menuFont;
       TextMenuItem mnuStart1PlayerGame , mnuStart2PlayerGame, mnuInstructions, mnuCredits,mnuToggleFullscreen, mnuExit;
       IntervalMenuItem mnuNumberOfWins;
       CheckBoxMenuItem mnuFireIsLethal;

       public KKGameMenu()
       {
           menuFont = GameDevGame.Current.Content.Load<SpriteFont>("MenuText");

           mnuStart1PlayerGame = new TextMenuItem(MENU_STARTSINGLEPLAYER, menuFont, MENU_STARTSINGLEPLAYER);
           mnuStart2PlayerGame = new TextMenuItem(MENU_START2PLAYER, menuFont);
           mnuFireIsLethal = new CheckBoxMenuItem(MENU_LETHALFIRE, menuFont, MENU_LETHALFIRE);
           mnuFireIsLethal.Checked = true;
           mnuNumberOfWins = new IntervalMenuItem(MENU_NUMBEROFWINS, menuFont, MENU_NUMBEROFWINS, 7, 1, 1);
           mnuNumberOfWins.CurrentValue = 3;
           mnuInstructions = new TextMenuItem(MENU_INSTRUCTIONS, menuFont);
           mnuCredits = new TextMenuItem(MENU_CREDITS, menuFont);
           mnuToggleFullscreen = new TextMenuItem(MENU_TOGGLEFULLSCREEN, menuFont);
           mnuExit = new TextMenuItem(MENU_EXIT, menuFont);

           this.AddMenuItem(mnuStart1PlayerGame);
           this.AddMenuItem(mnuStart2PlayerGame);
           this.AddMenuItem(mnuNumberOfWins);
           this.AddMenuItem(mnuFireIsLethal);
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
                item.Position = new Vector2(200, topMargin + 65 * counter);
                counter++;
            }
            

        }
    }
}

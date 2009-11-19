using GameDev.Core.Graphics;
using GameDev.Core.Menus;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework.Graphics;
#endif

namespace GameDev.Core.SceneManagement
{
    public class MenuScene : StaticImageScene
    {
        MenuBase m_menu = null;
        public MenuBase Menu
        {
            set
            {

                if (m_menu != null)
                {
                    this.Components.Remove(m_menu);
                }
                m_menu = value;
                this.Components.Add(m_menu);
            }
            get
            {
                return m_menu;
            }
        }


        public MenuScene(string name, MenuBase menu, GameImage backgroundImage)
            : base(name, backgroundImage)
        {
            this.Menu = menu;
        }

        public MenuScene(string name, MenuBase menu, Texture2D backgroundTexture)
            : base(name, backgroundTexture)
        {
            this.Menu = menu;
        }


        public MenuScene(string name, MenuBase menu, Color backgroundColor)
            : base(name, backgroundColor)
        {
            this.Menu = menu;
        }
    }
}

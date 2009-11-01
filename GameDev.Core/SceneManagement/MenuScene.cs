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
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
using GameDev.Core.Menus;
#endregion

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

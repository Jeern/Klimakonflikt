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
    public class MenuScene : Scene
    {

        MenuBase m_menu = null;

        public MenuScene(string name, MenuBase menu) : base(name)
        {
            this.m_menu = menu;
        }

        public override void OnEntered()
        {
        }

        public override void OnLeft()
        {
        }

        public override void Reset()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_menu.Update(gameTime);

        }
    }
}

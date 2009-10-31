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
#endregion

namespace GameDev.Core.Menus
{
    public abstract class MenuBase : DrawableGameComponent
    {

        DateTime m_lastKeyboardInput = DateTime.MinValue;
        private List<MenuItem> m_menuItems { get; set; }
        public int KeyboardDelay { get; set; }

        private int m_selectedIndex;

        public int SelectedIndex
        {
            set
            {
                if (m_menuItems.Count > 0)
                {
                    if (value >= 0 && value < m_menuItems.Count)
                    {
                        m_selectedIndex = value;
                    }
                    else
                        if (value < 0)
                        {
                            m_selectedIndex = m_menuItems.Count - 1;
                        }
                    if (value >= m_menuItems.Count)
                    {
                        m_selectedIndex = m_menuItems.Count - 1;
                    }
                }
                else
                {
                    throw new Exception("Unable to set index - no items in menulist");
                }
            }
            get 
            {
                return m_selectedIndex;
            }
        }

        public void AddMenuItem(MenuItem item)
        {
            this.m_menuItems.Add(item);
            if (m_menuItems.Count == 1)
            {
                item.IsSelected = true;
            }
            ArrangeMenuItems();
        }

        public void RemoveMenuItem(MenuItem item)
        {
            this.m_menuItems.Remove(item);
            EnsureSelectionBoundaries();
            ArrangeMenuItems();
        }

        private void EnsureSelectionBoundaries()
        {
            if (m_selectedIndex >= m_menuItems.Count)
            {
                m_selectedIndex = m_menuItems.Count - 1;
            }

        }

        public void RemoveMenuItemAt(int index)
        {
            this.m_menuItems.RemoveAt(index);
            EnsureSelectionBoundaries();
            ArrangeMenuItems();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (MenuItem item in m_menuItems)
            {
                item.Draw(gameTime);
            }
        }

        public MenuBase() : base(GameDevGame.Current)
        {
            KeyboardDelay = 250; //milliseconds
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (MenuItem item in m_menuItems)
            {
                item.Update(gameTime);
            }
            
            if ((DateTime.Now - m_lastKeyboardInput).TotalMilliseconds > KeyboardDelay)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Tab))
                {
                    SelectedIndex++;
                }
                if (state.IsKeyDown(Keys.Up) ||
                    (state.IsKeyDown(Keys.Tab) && (state.IsKeyDown(Keys.RightShift)||state.IsKeyDown(Keys.RightShift))))
                {
                    SelectedIndex--;
                }
            }

        }

        protected void ResetLastKeyboardTime()
        {
            m_lastKeyboardInput = DateTime.Now;
        }

        public abstract void ArrangeMenuItems();

    }
}

using System;
using System.Collections.Generic;
using GameDev.Core.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.Menus
{
    public abstract class MenuBase : DrawableGameComponent
    {

        public event MenuItemHandler MenuItemActivated;


        DateTime m_lastKeyboardInput = DateTime.MinValue;
        protected List<MenuItem> m_menuItems { get; set; }
        public int KeyboardDelay { get; set; }
        private int m_selectedIndex;


        public MenuItem GetMenuItem(string menuItemName)
        {
            foreach (MenuItem item in m_menuItems)
            {
                if (item.Name == menuItemName)
                {
                    return item;
                }
            }
            return null;
        }

        public MenuItem SelectedMenuItem
        {
            set
            {
                if (m_menuItems.Contains(value))
                {
                    SelectedIndex = m_menuItems.IndexOf(value);
                }
            }

            get { return m_menuItems[m_selectedIndex]; }
        }


        public int SelectedIndex
        {
            set
            {

                m_menuItems[m_selectedIndex].IsSelected = false;
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
                        m_selectedIndex = 0;
                    }
                    m_menuItems[m_selectedIndex].IsSelected = true;
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
            item.Activated +=new MenuItemHandler(OnMenuItemActivated);
            ArrangeMenuItems();
        }

        public void RemoveMenuItem(MenuItem item)
        {
            item.Activated -= new MenuItemHandler(OnMenuItemActivated);
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
            MenuItem item = this.m_menuItems[index];
            RemoveMenuItem(item);
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
            KeyboardDelay = 500; //milliseconds
            m_menuItems = new List<MenuItem>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
            foreach (MenuItem item in m_menuItems)
            {
                item.Update(gameTime);
            }

            if (SceneManager.Current.CurrentScene.NoKeysPressed)
            {

                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length == 0)
                {
                    m_lastKeyboardInput = DateTime.MinValue;
                }

                if ((DateTime.Now - m_lastKeyboardInput).TotalMilliseconds > KeyboardDelay)
                {

                    if (state.IsKeyDown(Keys.Down) || (state.IsKeyDown(Keys.Tab) && ! (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))))
                    {
                        SelectedIndex++;
                        ResetLastKeyboardTime();
                    }
                    if (state.IsKeyDown(Keys.Up) || (state.IsKeyDown(Keys.Tab) && (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))))
                    {
                        SelectedIndex--;
                        ResetLastKeyboardTime();
                    }


                    if (state.IsKeyDown(Keys.Home))
                    {
                        SelectedIndex = 0;
                    }
                    if (state.IsKeyDown(Keys.End))
                    {
                        SelectedIndex = m_menuItems.Count - 1;
                    }
                }
                if (state.IsKeyDown(Keys.Enter) || state.IsKeyDown(Keys.Space))
                {
                    SelectedMenuItem.Activate();
                } 
            }
        }

        protected void ResetLastKeyboardTime()
        {
            m_lastKeyboardInput = DateTime.Now;
        }

        protected void OnMenuItemActivated(MenuItem item, EventArgs e)
        {
            if (MenuItemActivated != null)
            {
                MenuItemActivated(item, e);
            }
 
        }

        public abstract void ArrangeMenuItems();

    }
}

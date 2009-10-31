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
    public delegate void MenuItemHandler(MenuItem sender, EventArgs e);

    public class MenuItem : DrawableGameComponent
    {

        #region Properties
        
        public string Name { get; set; }
        private bool m_isSelected;
        public int Top { get; set; }
        public Vector2 Position { get; set; }
        
        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                bool oldValue = m_isSelected;
                m_isSelected = value;
                if (oldValue != IsSelected)
                {
                    if (IsSelected)
                    {
                        OnSelected();
                    }
                    else
                    {
                        OnDeSelected();
                    }
                }
            }
        }

        #endregion

        public event MenuItemHandler Selected;
        public event MenuItemHandler Deselected;


        #region Constructors
        
        public MenuItem(string name, Vector2 position)
            : base(GameDevGame.Current)
        {
            this.Name = name;
            this.Position = position;
        }

        #endregion
        

        #region Events

        protected void OnSelected()
        {
            if (this.Selected != null)
            {
                this.Selected(this, EventArgs.Empty);
            }
        }

        protected void OnDeSelected()
        {
            if (this.Deselected != null)
            {
                this.Deselected(this, EventArgs.Empty);
            }
        } 
        #endregion

    }
}

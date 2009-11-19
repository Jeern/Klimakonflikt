using System;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Menus
{
    public delegate void MenuItemHandler(MenuItem sender, EventArgs e);

    public abstract class MenuItem : DrawableGameComponent
    {

        #region Properties

        public int MillisecondsBetweenKeyReceives { get; set; }
        private DateTime m_lastKeyboardReceive = DateTime.MinValue;
        protected bool NeedsPositionRecalculation { get; set; }
        public string Name { get; set; }
        private bool m_isSelected;
        public int Top { get; set; }
        public Vector2 Position { get; set; }
        private bool m_centered = true;
        public bool Selectable { get; set; }
        public bool Centered
        {
            get
            {
                return m_centered;
            }
            set
            {
                m_centered = value;
                NeedsPositionRecalculation = true;
            }
        }

        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if (Selectable)
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
        }

        #endregion

        public event MenuItemHandler Selected;
        public event MenuItemHandler Deselected;
        public event MenuItemHandler Activated;


        #region Constructors

        public MenuItem(string name, Vector2 position, bool centered)
            : base(GameDevGame.Current)
        {
            this.Name = name;
            this.Position = position;
            this.Centered = centered;
            MillisecondsBetweenKeyReceives = 200;
            this.Selectable = true;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (NeedsPositionRecalculation)
            {
                RecalculatePosition();
                NeedsPositionRecalculation = false;
            }
        }

        public void Activate()
        {
            OnActivate();
        }

        protected void OnActivate()
        {
            if (Activated != null)
            {
                Activated(this, EventArgs.Empty);
            }
        }
        public bool IsReadyForKeyboardInteraction
        {
            get { return ((TimeSpan)(DateTime.Now - m_lastKeyboardReceive)).TotalMilliseconds > MillisecondsBetweenKeyReceives; }
        }

        public void ResetKeyboardIntervalTimer()
        {
            m_lastKeyboardReceive = DateTime.Now;
        }
        protected abstract void RecalculatePosition();
    }
}

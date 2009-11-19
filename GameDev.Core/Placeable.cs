#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core
{
    public class Placeable : DrawableGameComponent, IPlaceable
    {
        private int _x;
        private int _y;
        public Placeable() : this(0,0)
        {

        }

        public Placeable(int x, int y):base(GameDevGame.Current)
        {
            X = x;
            Y = y;
        }

        #region IPlaceable Members

        public int X
        {
            get { return _x; }
            set { _x = value;}
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public void Move(Direction direction, float distance)
        {
            DirectionHelper4.Offset(this, direction, distance);
        }


     
        #endregion

        public override string ToString()
        {
            return this.GetType() + " (x:" + X + ",y:" + Y + ")";
        }

    }
}

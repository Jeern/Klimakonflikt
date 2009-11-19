using System;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Events
{
    public class TimerCondition : Condition
    {
        DateTime begin = DateTime.MinValue;
        TimeSpan elapsedTime;
        int _millisecondsPause;


        public TimerCondition(int millisecondPause)
        {
            this._millisecondsPause = millisecondPause;
        }

        public override void Update(GameTime gameTime)
        {
            if (begin == DateTime.MinValue)
            {
                begin = DateTime.Now;
            }
            else
            {
                elapsedTime += gameTime.ElapsedGameTime;
            }
        }

        public override void Reset()
        {
            base.Reset();
            this.begin = DateTime.MinValue;
        }

        public override bool IsCurrentlyFulfilled()
        {
            return (elapsedTime.TotalMilliseconds > this._millisecondsPause);
        }
    }
}
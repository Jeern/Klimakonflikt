using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameDev.Core;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;
using GameDev.Core.Sequencing;
using System;

namespace KlimaKonflikt
{
    public class KKMonster : Sprite
    {
        public Direction WantedDirection 
        {
            get { return m_DirectionIterator.Current; }
        }

        private SequencedIterator<Direction> m_DirectionIterator;
        private SequencedIterator<int> m_DelayMillisecondsIterator;

        public KKMonster(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, SequencedIterator<int> delayMillisecondsIterator)
            : base(game, gameImage, spriteBatch, speed, startingPosition)
        {
            m_DirectionIterator = directionIterator;
            m_DelayMillisecondsIterator = delayMillisecondsIterator;
        }

        public KKMonster(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, int delayMilliseconds)
            : this(game, gameImage, spriteBatch, speed, startingPosition, directionIterator, new SequencedIterator<int>(new StaticSequencer(), delayMilliseconds))
        {
        }

        private TimeSpan m_LastChanged = TimeSpan.MinValue;

        public override void Update(GameTime time)
        {
            TimeSpan newTime = time.TotalGameTime;
            if (m_LastChanged == TimeSpan.MinValue || m_LastChanged.Add(new TimeSpan(0, 0, 0, 0, m_DelayMillisecondsIterator.Current)) <= newTime)
            {
                m_LastChanged = newTime;
                m_DelayMillisecondsIterator.MoveNext();
                m_DirectionIterator.MoveNext();
            }
            base.Update(time);
        }


    }        
}

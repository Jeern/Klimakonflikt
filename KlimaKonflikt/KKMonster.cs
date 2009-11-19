using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.Sequencing;
using Microsoft.Xna.Framework;

namespace KlimaKonflikt
{
    public class KKMonster : Sprite
    {

        private SequencedIterator<Direction> m_DirectionIterator;
        private SequencedIterator<int> m_DelayMillisecondsIterator;
        
        
        public KKMonster(GameImage gameImage, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, SequencedIterator<int> delayMillisecondsIterator)
            : base(gameImage, speed, startingPosition)
        {
            m_DirectionIterator = directionIterator;
            m_DelayMillisecondsIterator = delayMillisecondsIterator;
        }

        public KKMonster(GameImage gameImage, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, int delayMilliseconds)
            : this(gameImage, speed, startingPosition, directionIterator, new SequencedIterator<int>(new StaticSequencer(), delayMilliseconds))
        {
        }

    }        
}

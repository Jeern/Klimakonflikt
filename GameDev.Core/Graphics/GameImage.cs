using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameDev.Core.Sequencing;

namespace GameDev.Core.Graphics
{
    public class GameImage
    {
        private SequencedIterator<GameDevTexture> m_ImageIterator;
        private SequencedIterator<int> m_DelayMillisecondsIterator;

        public GameImage(GameDevTexture texture) 
            : this(new SequencedIterator<GameDevTexture>(new StaticSequencer(), texture), 0)
        {
        }

        public GameImage(SequencedIterator<GameDevTexture> imageIterator, int delayMilliseconds) 
            : this(imageIterator, new SequencedIterator<int>(new StaticSequencer(), delayMilliseconds))
        {
            
        }

        public GameImage(SequencedIterator<GameDevTexture> imageIterator, SequencedIterator<int> delayMillisecondsIterator)
        {
            m_ImageIterator = imageIterator;
            m_DelayMillisecondsIterator = delayMillisecondsIterator;
        }

        private TimeSpan m_LastChanged = TimeSpan.MinValue;

        public void Update(GameTime time)
        {
            TimeSpan newTime = time.TotalGameTime;
            if (m_LastChanged.Add(new TimeSpan(0, 0, 0, 0, m_DelayMillisecondsIterator.Current)) <= newTime)
            {
                m_LastChanged = newTime;
                m_DelayMillisecondsIterator.MoveNext();
                m_ImageIterator.MoveNext(); 
            }
        }

        public GameDevTexture CurrentTexture
        {
            get { return m_ImageIterator.Current; }
        }        
    }
}

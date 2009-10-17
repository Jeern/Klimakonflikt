using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameDev.Core.Sequencing;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.Graphics
{
    public class GameImage
    {
        private SequencedIterator<Texture2D> m_ImageIterator;
        private SequencedIterator<int> m_DelayMillisecondsIterator;

        public GameImage(Texture2D texture)
            : this(new SequencedIterator<Texture2D>(new StaticSequencer(), texture), 0)
        {
        }

        public GameImage(SequencedIterator<Texture2D> imageIterator, int delayMilliseconds) 
            : this(imageIterator, new SequencedIterator<int>(new StaticSequencer(), delayMilliseconds))
        {
            
        }

        public GameImage(SequencedIterator<Texture2D> imageIterator, SequencedIterator<int> delayMillisecondsIterator)
        {
            m_ImageIterator = imageIterator;
            m_DelayMillisecondsIterator = delayMillisecondsIterator;
        }

        private TimeSpan m_LastChanged = TimeSpan.MinValue;

        public void Update(GameTime time)
        {
            TimeSpan newTime = time.TotalGameTime;
            if (m_LastChanged == TimeSpan.MinValue || m_LastChanged.Add(new TimeSpan(0, 0, 0, 0, m_DelayMillisecondsIterator.Current)) <= newTime)
            {
                m_LastChanged = newTime;
                m_DelayMillisecondsIterator.MoveNext();
                m_ImageIterator.MoveNext(); 
            }
        }

        public Texture2D CurrentTexture
        {
            get { return m_ImageIterator.Current; }
        }        
    }
}

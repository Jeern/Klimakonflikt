using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using GameDev.Core;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;
using GameDev.Core.Sequencing;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using GameDev.GameBoard;
using System.Collections.Generic;

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

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.SceneManagement
{
    public class TimedSceneLink : SceneLink
    {

        DateTime begin = DateTime.MinValue;
        TimeSpan elapsedTime;
        int _millisecondsPause;


        public TimedSceneLink(IScene targetScene, int millisecondPause) : base(targetScene)
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

                if (elapsedTime.TotalMilliseconds > this._millisecondsPause)
                {
                    this.begin = DateTime.MinValue;
                    GoToLink();
                }
                
            }
        }
    }
}

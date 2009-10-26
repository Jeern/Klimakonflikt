using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Events
{
    public class GameExitReaction : Reaction
    {

        public override void React(Condition sender)
        {
            GameDevGame.Current.Exit();
        }

    }
}

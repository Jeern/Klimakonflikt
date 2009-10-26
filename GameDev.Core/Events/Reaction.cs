using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDev.Core.Events
{
    public abstract class Reaction
    {
        
        public abstract void React(Condition sender);

        public virtual void React()
        {
            React(null);
        }
    }
}

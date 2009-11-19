#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Events
{

    public abstract class Condition : GameComponent
    {
        public event GameDevEventHandler ConditionFulfilled;

        public bool HasBeenFulfilled { get; private set; }

        public Condition() : base(GameDevGame.Current) {}

        protected void OnConditionFulfilled()
        {
            if (ConditionFulfilled != null)
            {
                ConditionFulfilled(this);
            }
        }

        public virtual void Reset()
        {
            this.HasBeenFulfilled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //ensure that this is the first event 
            if (!HasBeenFulfilled && IsCurrentlyFulfilled())
            {
                HasBeenFulfilled = true;
                OnConditionFulfilled();
            }
        }

        public abstract bool IsCurrentlyFulfilled();

    }
}

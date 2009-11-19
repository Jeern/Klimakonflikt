#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Events
{
    public class OneConditionToManyReactionsLink : GameComponent
    {
        private ManyConditionsToManyReactionsLink crc;

        public OneConditionToManyReactionsLink() : base(GameDevGame.Current)
        {
            crc = new ManyConditionsToManyReactionsLink();
        }

        public OneConditionToManyReactionsLink(Condition condition, params Reaction[] reactions)
            : this()
        {
            Condition = condition;

            foreach (Reaction reaction in reactions)
            {
                AddReaction(reaction);
            }
            
        }


        public Condition Condition
        {
            get { return crc.Conditions[0]; }
            set
            {
                crc.Conditions.Clear();
                crc.Conditions.Add(value);
            }
        }

        public virtual void AddReaction(Reaction reactionToAdd)
        {
            crc.AddReaction(reactionToAdd);
        }

        public virtual void RemoveReaction(Reaction reactionToRemove)
        {
            crc.RemoveReaction(reactionToRemove);
        }

        public override void Update(GameTime gameTime)
        {
                Condition.Update(gameTime);
        }

    }
}

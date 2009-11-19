#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Events
{
    public class ManyConditionsToOneReactionLink : GameComponent
    {
        private ManyConditionsToManyReactionsLink crc;

        public ManyConditionsToOneReactionLink() : base(GameDevGame.Current)
        {
            crc = new ManyConditionsToManyReactionsLink();
        }

        public ManyConditionsToOneReactionLink(Condition condition, Reaction reaction)
            : this(reaction, condition)
        {
        }

        public ManyConditionsToOneReactionLink(Reaction reaction, params Condition[] conditions) : this()
        {
            Reaction = reaction;
            foreach (Condition condition in conditions)
            {
                AddCondition(condition);
            }
            
        }




        public Reaction Reaction
        {
            get { return crc.Reactions[0]; }
            set
            {
                crc.Reactions.Clear();
                crc.Reactions.Add(value);
            }
        }

        public virtual void AddCondition(Condition conditionToAdd)
        {
            crc.AddCondition(conditionToAdd);
        }

        public virtual void RemoveCondition(Condition conditionToRemove)
        {
            crc.RemoveCondition(conditionToRemove);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Condition condition in crc.Conditions)
            {
                condition.Update(gameTime);
            }
        }

    }
}

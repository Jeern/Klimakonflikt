using System.Collections.Generic;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.Core.Events
{
    public class ManyConditionsToManyReactionsLink : GameComponent
    {
        internal List<Condition> Conditions;
        internal List<Reaction> Reactions;

        public ManyConditionsToManyReactionsLink() : base (GameDevGame.Current)
        {
            Conditions = new List<Condition>();
            Reactions = new List<Reaction>();
        }

        public ManyConditionsToManyReactionsLink(Condition condition, Reaction reaction)
            : this()
        {
            AddCondition(condition);
            AddReaction(reaction);
        }


        public void AddCondition(Condition conditionToAdd)
        {
            this.Conditions.Add(conditionToAdd);
            foreach (Reaction reaction in Reactions)
            {
                Add(conditionToAdd, reaction);
            }
        }

        public void RemoveCondition(Condition conditionToRemove)
        {
            foreach (Reaction reaction in Reactions)
            {
                Remove(conditionToRemove, reaction);
            }
        }

        public void AddReaction(Reaction reactionToAdd)
        {
            this.Reactions.Add(reactionToAdd);
            foreach (Condition condition in Conditions)
            {
                Add(condition, reactionToAdd);
            }
        }

        public void RemoveReaction(Reaction reactionToRemove)
        {
            foreach (Condition condition in Conditions)
            {
                Remove(condition, reactionToRemove);
            }
        }

        private void Add(Condition condition, Reaction reaction)
        {
            condition.ConditionFulfilled +=new GameDevEventHandler(reaction.React);
        }

        private void Remove(Condition condition, Reaction reaction)
        {
            condition.ConditionFulfilled -= new GameDevEventHandler(reaction.React);
        }


        public override void Update(GameTime gameTime)
        {
            foreach (Condition condition in Conditions)
            {
                condition.Update(gameTime);
            }
        }
    }
}

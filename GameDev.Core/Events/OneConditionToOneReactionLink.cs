using Microsoft.Xna.Framework;


namespace GameDev.Core.Events
{
    public class OneConditionToOneReactionLink : GameComponent
    {
        private Condition _condition;
        private Reaction _reaction;

        public Condition Condition {
            get { return _condition; }
            set{
                Remove(Condition, Reaction);
                _condition = value;
                Add(Condition, Reaction);
            }
        }

       
        public Reaction Reaction {
            get { return _reaction; }
            set
            {
                Remove(Condition, Reaction);
                _reaction = value;
                Add(Condition, Reaction);
            }
        }

        public OneConditionToOneReactionLink(Condition condition, Reaction reaction) : base(GameDevGame.Current)
        {
            this.Condition = condition;
            this.Reaction = reaction;
        }

        protected void Add(Condition condition, Reaction reaction)
        {
            if (condition != null && reaction != null)
            {
                Condition.ConditionFulfilled += new GameDevEventHandler(reaction.React);
            }
        }

        private void Remove(Condition Condition, Reaction Reaction)
        {
            if (Condition != null && Reaction != null)
            {
                Condition.ConditionFulfilled -= new GameDevEventHandler(Reaction.React);
            }
        }

        public override void Update(GameTime gameTime)
        {
                Condition.Update(gameTime);
        }


    }
}


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

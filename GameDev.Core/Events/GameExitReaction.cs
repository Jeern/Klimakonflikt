
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

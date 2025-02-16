/*
public class LinkAttackingState : ILinkState
    {
        private int attackFrameCounter = 0;
        private const int attackDuration = 10;

        public void Enter(Link link)
        {
            link.sprite.SetState(LinkAction.Attacking, link.CurrentDirection);
        }

        public void Update(Link link)
        {
            attackFrameCounter++;
            if (attackFrameCounter >= attackDuration)
            {
                link.ChangeState(new LinkIdleState());
            }
        }

        public void Exit(Link link) { }
    }
*/

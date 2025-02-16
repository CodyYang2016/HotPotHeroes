/*
public class LinkDamagedState : ILinkState
    {
        private int damageFrameCounter = 0;
        private const int damageDuration = 15;

        public void Enter(Link link)
        {
            link.sprite.SetState(LinkAction.Damaged, link.CurrentDirection);
        }

        public void Update(Link link)
        {
            damageFrameCounter++;
            if (damageFrameCounter >= damageDuration)
            {
                link.ChangeState(new LinkIdleState());
            }
        }

        public void Exit(Link link) { }
    }
*/

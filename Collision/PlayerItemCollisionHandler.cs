using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test;

    public class PlayerItemCollisionHandler
    {        
        public void HandleCollisionList(Link player, List<IItem> _active)
        {
            foreach (var block in _active)
                {
                    HandleCollision(block);
                }
        }

    public void HandleCollision(IItem item)
    {
        if (CollisionDetect3.isTouchingLeft(Link.Instance, item))
        {
            Link.Instance.TakeDamage();
            item.Use();
        }

        if (CollisionDetect3.isTouchingRight(Link.Instance, item))
        {
            Link.Instance.TakeDamage();
            item.Use();
        }

        if (CollisionDetect3.isTouchingBottom(Link.Instance, item))
        {
            Link.Instance.TakeDamage();
            item.Use();
        }

        if (CollisionDetect3.isTouchingTop(Link.Instance, item))
        {
            Link.Instance.TakeDamage();
            item.Use();
        }
    }

}

    


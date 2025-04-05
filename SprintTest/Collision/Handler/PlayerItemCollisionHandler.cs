using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test;

    public class PlayerItemCollisionHandler
    {
    public void HandleCollisionList(List<IItem> _active)
    {
        if (_active == null) return;

        foreach (var item in _active)
        {
            HandleCollision(item);
        }
    }


    public void HandleCollision(IItem item)
    {
        if (item != null)
        {
            if (CollisionDetectItem.isTouchingLeft(item))
            {
                // Link.Instance.TakeDamage();
            }

            if (CollisionDetectItem.isTouchingRight(item))
            {
                // Link.Instance.TakeDamage();
            }

            if (CollisionDetectItem.isTouchingBottom(item))
            {
                // Link.Instance.TakeDamage();
            }

            if (CollisionDetectItem.isTouchingTop(item))
            {
                // Link.Instance.TakeDamage();
            }
        }
    }

}

    


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using System.Diagnostics;
using sprint0Test.Items;

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
            if (CollisionDetectItem.isTouching(item) && item.IsCollected != true)
            {
                item.Collect();
                if (item.BehaviorType == ItemBehaviorType.Collectible)
                {
                    if (!(item is Bomb bomb))
                    {
                        Link.Instance.AddItem(item);
                    }
                    else if(bomb.State == Bomb.BombState.InInventory) Link.Instance.AddItem(item);
                }
                else { 
                    Link.Instance.Consume(item);
                }
            }
        }
    }

}

    


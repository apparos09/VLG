using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The item class.
    public class Item : FloorEntity
    {
        // Called when the item is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            if(entity is Player)
            {
                Player player = (Player)entity;
                // ...
                // TODO: give player item
            }
        }
    }
}
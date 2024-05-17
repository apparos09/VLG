using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The item class.
    public class Item : FloorEntity
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the group.
            group = entityGroup.item;
        }

        // Called when the item is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // The entity is a player.
            if(entity is Player)
            {
                Player player = (Player)entity;
                // ...
                // TODO: give player item
            }
        }

        // Kills the player.
        public override void KillEntity()
        {
            Destroy(gameObject);
        }

        // This function is called when the MonoBehaviour will be destroyed.
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
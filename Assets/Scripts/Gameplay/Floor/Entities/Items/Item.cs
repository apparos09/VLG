using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The item class.
    public class Item : FloorEntity
    {
        // The enum for the itme.
        public enum itemType { none, key, weapon }

        // The item type for the object.
        public itemType item = itemType.none;

        // The idle animation for the item.
        public string idleAnim = "";

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the group.
            group = entityGroup.item;

            // Play idle animation.
            animator.Play(idleAnim);
        }

        // Call this function when the item is given to a player.
        public virtual void OnItemGiven(Player player)
        {
            // Destroy the item object.
            KillEntity();
        }

        // Called when the item is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // The entity is a player.
            if(entity is Player)
            {
                // Get the player and give them this item.
                Player player = (Player)entity;
                player.GiveItem(this);
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
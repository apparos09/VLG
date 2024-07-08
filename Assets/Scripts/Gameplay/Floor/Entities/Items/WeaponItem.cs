using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // An item that gives the player a weapon.
    public class WeaponItem : Item
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // If the item type is not set, set it to weapon.
            if (item == itemType.none)
                item = itemType.weapon;

            // The item exists, so the player should not be able to attack.
            gameManager.player.enabledAttack = false;
        }

        // Call this function when the item is given to a player.
        public override void OnItemGiven(Player player)
        {
            base.OnItemGiven(player);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();     

            // Disalbe the player's attack since the item's bene reset.
            gameManager.player.enabledAttack = false;
        }

        // This function is called when the MonoBehaviour will be destroyed.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // The item is gone, so make sure the player's attack is enabled.
            gameManager.player.enabledAttack = true;
        }
    }
}
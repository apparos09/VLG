using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A floor enemy.
    public class Enemy : FloorEntity
    {
        // If 'true', the level geometry is ignored for enemy movement.
        [Tooltip("If true, the enemy ignores the floor geometry for movement.")]
        public bool ignoreGeometry = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            group = entityGroup.enemy;
        }

        // Called when the entity is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // If the entity is a player.
            if(entity is Player)
            {
                // Gets the player.
                Player player = (Player)entity;

                // TODO: attack player.

                // Resets the player's position.
                player.ResetEntity();
            }
        }

       
    }
}
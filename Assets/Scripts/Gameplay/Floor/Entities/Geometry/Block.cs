using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A generic block for the game.
    public class Block : FloorEntity
    {

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            group = entityGroup.geometry;
        }

        // Called to see if this block is valid to use for the provided entity.
        public virtual bool UsableBlock(FloorEntity entity)
        {
            return true;
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // ...
        }

        // Kills the block - by default it does nothing.
        public override void KillEntity()
        {
            Destroy(gameObject);
        }

        // This function is called when the MonoBehaviour will be destroyed
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Checks for removal.
            bool removed = false;

            // Goes through every row and column to remove the enemy from the array.
            for (int r = 0; r < floorManager.floorGeometry.GetLength(0) && !removed; r++)
            {
                for (int c = 0; c < floorManager.floorGeometry.GetLength(1) && !removed; c++)
                {
                    if (floorManager.floorGeometry[r, c] == this)
                    {
                        floorManager.floorGeometry[r, c] = null;
                        removed = true;
                        break;
                    }
                }
            }
        }
    }
}
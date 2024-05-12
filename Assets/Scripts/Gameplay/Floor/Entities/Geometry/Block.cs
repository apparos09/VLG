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

        // Called to see if this block is valid to use.
        public virtual bool UsableBlock()
        {
            return true;
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // ...
        }
    }
}
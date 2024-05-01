using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A generic block for the game.
    public class Block : FloorAsset
    {

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Called to see if this block is valid to use.
        public virtual bool UsableBlock()
        {
            return true;
        }

        // Called when an element interact with this block, which is usually the player.
        public virtual void OnBlockInteract(FloorAsset asset)
        {
            // ...
        }

        // Resets the asset.
        public override void ResetAsset()
        {
            // Resets the position.
            SetFloorPosition(resetPos, false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
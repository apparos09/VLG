using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The goal block.
    public class GoalBlock : Block
    {
        // The goal for the game.
        public Goal goal;

        // Called when an element interacts with the goal.
        public override void OnBlockInteract(FloorAsset asset)
        {
            base.OnBlockInteract(asset);

            // The player interacted with the block.
            if (asset is Player)
            {
                goal.TryEnterGoal(asset as Player);
            }
        }
    }
}
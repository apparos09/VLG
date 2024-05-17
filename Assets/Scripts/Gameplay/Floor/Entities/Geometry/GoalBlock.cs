using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace VLG
{
    // The goal block.
    public class GoalBlock : Block
    {
        // The goal for the game.
        public Goal goal;

        // Called when a ButtonBlock has its button clicked.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            // Toggles the goal's locked/unlocked state.
            goal.ToggleLocked();
        }

        // Called when an element interacts with the goal.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // The player interacted with the block.
            if (entity is Player)
            {
                goal.TryEnterGoal(entity as Player);
            }
        }
    }
}
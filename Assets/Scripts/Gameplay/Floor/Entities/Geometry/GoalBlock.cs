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

        // Sets the locked state.
        public void SetLocked(bool value)
        {
            goal.SetLocked(value);
        }

        // Locks the goal.
        public void LockGoal()
        {
            goal.SetLocked(true);
        }

        // Unlocks the goal.
        public void UnlockGoal()
        {
            goal.SetLocked(true);
        }

        // Toggles the locked setting for the goal.
        public void ToggleLocked()
        {
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
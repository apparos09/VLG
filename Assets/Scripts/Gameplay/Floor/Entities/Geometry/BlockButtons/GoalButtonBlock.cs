using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Portal goal toggle button
    public class GoalButtonBlock : ButtonBlock
    {
        // NOTE: blocks are not destroyed on reset, so this shouldn't cause issues.
        // The list of goal blocks.
        private List<GoalBlock> goalBlocks;

        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the portal blocks and adds them to this button.
            goalBlocks = AddAllFloorEntitiesOfType<GoalBlock>(true);

            // Resets the entity so that the goal is turned off by default.
            ResetEntity();
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Disable the goal at the start.
            foreach (GoalBlock goalBlock in goalBlocks)
            {
                // Gets the goal.
                Goal goal = goalBlock.goal;

                // Makes the goal unusable at the start.
                if (goal.objective == Goal.goalType.button)
                {
                    goal.SetUsable(false);
                }
            }
        }
    }
}

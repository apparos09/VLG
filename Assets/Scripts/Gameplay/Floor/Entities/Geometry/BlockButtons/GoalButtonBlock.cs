using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Portal goal toggle button
    public class GoalButtonBlock : ButtonBlock
    {
        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the portal blocks and adds them to this button.
            AddAllFloorEntitiesOfType<GoalBlock>(true);
        }
    }
}

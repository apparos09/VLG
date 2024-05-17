using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Portal lock toggle button
    public class PortalButtonBlock : ButtonBlock
    {
        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the portal blocks and adds them to this button.
            AddAllFloorEntitiesOfType<PortalBlock>(true);
        }
    }
}
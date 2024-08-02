using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Portal lock toggle button
    public class PortalButtonBlock : ButtonBlock
    {
        // NOTE: blocks are not destroyed on reset, so this shouldn't cause issues.
        // The list of portal blocks.
        private List<PortalBlock> portalBlocks;

        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the portal blocks and adds them to this button.
            portalBlocks = AddAllFloorEntitiesOfType<PortalBlock>(true);

            // Resets the entity so that the portals are off by default.
            ResetEntity();
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Locks all portals by default.
            foreach (PortalBlock portal in portalBlocks)
            {
                portal.LockPortal();
            }
                
        }
    }
}
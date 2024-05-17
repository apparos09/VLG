using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A hazard toggle button.
    public class HazardButtonBlock : ButtonBlock
    {
        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the hazard blocks and adds them to this button.
            AddAllFloorEntitiesOfType<HazardBlock>(true);
        }


    }
}

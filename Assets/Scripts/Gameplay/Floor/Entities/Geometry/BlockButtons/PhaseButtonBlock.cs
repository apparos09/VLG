using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VLG
{
    // The phase toggle button.
    public class PhaseButtonBlock : ButtonBlock
    {
        // Called on the first update frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Finds all the phase blocks and adds them to this button.
            AddAllFloorEntitiesOfType<PhaseBlock>(true);
        }

    }
}

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
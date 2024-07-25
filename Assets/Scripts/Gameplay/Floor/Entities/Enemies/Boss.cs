using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A boss in the game.
    public class Boss : Enemy
    {
        [Header("Boss")]
        // The boss's health. A boss dies in a fixed amount of hits.
        public float health = 1;

        // Used to call post start.
        private bool calledPostStart = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Called on the first frame.
        protected virtual void PostStart()
        {
            calledPostStart = true;
        }

        // Update is called once per frame
        protected override void Update()
        {
            // If PostStart hasn't been called, call it.
            if(!calledPostStart)
            {
                PostStart();
            }

            base.Update();
        }
    }
}
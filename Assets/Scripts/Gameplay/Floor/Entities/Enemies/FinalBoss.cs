using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VLG
{
    // The final boss of the game.
    public class FinalBoss : Boss
    {
        // Outlines the lighting strike pattern.
        public struct LightningStrikeLayout
        {
            // The positions for the lighting strikes. False means no strike, true means strike.
            // public int[,] layout = new int[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];
            public bool[,] positions;
        }

        // Gets set to 'true' when the final boss is attacking.
        private bool attacking = false;

        [Header("Final Boss")]

        // The lighting strike prefab
        public LightningStrike lightningStrikePrefab;

        // The lightning strike pattern.
        private Queue<LightningStrikeLayout> lightningQueue = new Queue<LightningStrikeLayout>();

        // The pool of lighitng strikes to pull from.
        private Queue<LightningStrike> lightningStrikePool = new Queue<LightningStrike>();

        // The lighting timer max.
        private float lightningTimerMax = 3.0F;

        // The countdown timer for firing off lightning.
        private float lightningTimer = 0.0F;

        // The number of active lighting attacks.
        private int activeLightningCount = 0;

        [Header("Final Boss/Animations")]

        // Animation for raising the final boss.
        public string riseAnim = "";

        // Animation for dropping the final boss.
        public string dropAnim = "";


        // Awake is called when the script is being loaded
        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // The phase count for the final boss.
            phaseCount = 5;

            // Set to phase 1.
            if (phase <= 0)
                phase = 1;
        }

        // Called on the first frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Test
            // // Lighting Strike Test - Make Sure to Remove
            // SpawnLightingStrike(new Vector2Int(6, 0));

            // Starts the phase.
            StartPhase();

        }

        // Spawns a lighting strike at the provided position.
        public void SpawnLightingStrike(Vector2Int strikePos)
        {
            // The lighting strike object.
            LightningStrike strike;

            // Checks if there's already a saved lighting strike object.
            if(lightningStrikePool.Count > 0)
            {
                strike = lightningStrikePool.Dequeue();
            }
            else // No object.
            {
                // Generate a new one.
                strike = Instantiate(lightningStrikePrefab);

                // Parent
                strike.transform.parent = transform.parent;

                // Return the lighting strike object to the pool when it's done.
                strike.OnLightingStrikeEndAddCallback(ReturnLightingStrike);
            }

            // Activates the object and triggers a ligthing strike.
            strike.gameObject.SetActive(true);
            strike.TriggerLightningStrike(strikePos);

            // There is active lightning.
            activeLightningCount++;
        }

        // Returns the lighting strike to the pool.
        public void ReturnLightingStrike(LightningStrike strike)
        {
            strike.gameObject.SetActive(false);
            lightningStrikePool.Enqueue(strike);

            // There is not active lightning.
            activeLightningCount--;
        }

        // Spawns lightning strikes with the provided layout.
        public void SpawnLightningStrikes(LightningStrikeLayout layout)
        {
            // No positions to use.
            if (layout.positions == null)
                return;

            // Goes through the layout.
            for(int r = 0; r < layout.positions.GetLength(0); r++) // Row
            {
                for(int c = 0; c < layout.positions.GetLength(1); c++) // Col
                {
                    // If this is a position to strike at.
                    if (layout.positions[r, c] == true)
                    {
                        Vector2Int gridPos = new Vector2Int(r, c);
                        SpawnLightingStrike(gridPos);
                    }
                }
            }
        }

        // Activates the hazards based on the current phase.
        public void ActivateHazards()
        {
            // The hazard sets.
            Vector2Int rowSet1 = new Vector2Int(0, 9);
            Vector2Int rowSet2 = new Vector2Int(1, 8);
            Vector2Int rowSet3 = new Vector2Int(2, 7);

            // The rows to be activated.
            Queue<Vector2Int> rowQueue = new Queue<Vector2Int>();

            // Checks what rows should be enabled for the phases.
            switch(phase)
            {
                case 0:
                case 1: // Clear
                    rowQueue.Clear();
                    break;

                case 2:
                    rowQueue.Enqueue(rowSet1);
                    break;

                case 3:
                    rowQueue.Enqueue(rowSet1);
                    rowQueue.Enqueue(rowSet2);
                    break;

                case 4:
                case 5:
                    rowQueue.Enqueue(rowSet1);
                    rowQueue.Enqueue(rowSet2);
                    rowQueue.Enqueue(rowSet3);
                    break;
            }

            // While there are still rows.
            while (rowQueue.Count > 0)
            {
                // Gets the rows.
                Vector2Int rows = rowQueue.Dequeue();

                // Columns
                for (int col = 0; col < floorManager.floorGeometry.GetLength(1); col++)
                {
                    // If this is a hazard block.
                    if (floorManager.floorGeometry[rows.x, col] is HazardBlock)
                    {
                        // Gets the hazard.
                        HazardBlock hazard = (HazardBlock)floorManager.floorGeometry[rows.x, col];

                        // Enable the hazard.
                        if(!hazard.IsHazardOn())
                            hazard.EnableHazard(true);
                    }

                    // If this is a hazard block.
                    if (floorManager.floorGeometry[rows.y, col] is HazardBlock)
                    {
                        // Gets the hazard.
                        HazardBlock hazard = (HazardBlock)floorManager.floorGeometry[rows.y, col];

                        // Enable the hazard.
                        if (!hazard.IsHazardOn())
                            hazard.EnableHazard(true);
                    }

                }
            }
        }

        // Disables all floor hazards.
        public void DisableAllHazards()
        {
            // Row
            for (int row = 0; row < floorManager.floorGeometry.GetLength(0); row++)
            {
                // Column
                for (int col = 0; col < floorManager.floorGeometry.GetLength(1); col++)
                {
                    // If this is a hazard block.
                    if (floorManager.floorGeometry[row, col] is HazardBlock)
                    {
                        // Gets the hazard.
                        HazardBlock hazard = (HazardBlock)floorManager.floorGeometry[row, col];

                        // Disable the hazard.
                        hazard.DisableHazard();
                    }

                }
            }
        }

        // Generates the lightning pattern based on the provided phase.
        public void GenerateLightningPattern()
        {
            //// The template.
            //LightningStrikeLayout layoutTemp = new LightningStrikeLayout();
            //layoutTemp.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false},
            //    { false, false, false, false, false, false, false, false, false, false}
            //};

            // Layout 1
            LightningStrikeLayout layout1 = new LightningStrikeLayout();
            layout1.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { true, true, true, true, true, true, true, true, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, true, true, true, true, true, true, true, true, true},
            };

            // Layout 2
            LightningStrikeLayout layout2 = new LightningStrikeLayout();
            layout2.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true},
                { false, true, false, true, false, true, false, true, false, true}
            };


            // Clears the queue.
            lightningQueue.Clear();

            // Checks the phase to show what the pattern should be.
            switch (phase)
            {
                case 0:
                case 1:
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout1);
                    break;

                case 2:
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout1);
                    break;

                case 3:
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout1);
                    break;

                case 4:
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout1);
                    break;

                case 5:
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout1);
                    break;
            }
        }

        // Sets the max of the lightning timer based on the current phase.
        public void SetLightningTimerMax()
        {
            // Checks the phase for the new lighting max value.
            switch(phase)
            {
                case 0:
                case 1:
                default:
                    lightningTimerMax = 3.0F;
                    break;

                case 2:
                    lightningTimerMax = 2.50F;
                    break;

                case 3:
                    lightningTimerMax = 2.25F;
                    break;

                case 4:
                    lightningTimerMax = 2.0F;
                    break;

                case 5:
                    lightningTimerMax = 1.75F;
                    break;
            }
        }


        // PHASE

        // Start the next phase.
        public void StartPhase()
        {
            // Activates the hazards, generates the lightning pattern, and sets the timer max.
            ActivateHazards();
            GenerateLightningPattern();
            SetLightningTimerMax();

            // The boss should attack.
            // attacking = true; // Triggered by animation.

            // Raise the boss.
            animator.Play(riseAnim);
        }

        // End the phase.
        public void EndPhase()
        {
            // No longer attacking.
            attacking = false;

            // Lower the boss.
            animator.Play(dropAnim);
        }

        // ATTACK/DAMAGE //
        // Start attacking.
        public void StartAttacking()
        {
            attacking = true;

            // Start shooting lightning instantly.
            lightningTimer = 0;
        }

        // Stop attacking.
        public void StopAttacking()
        {
            attacking = false;
        }

        // Called when damage has been taken.
        public override void OnDamageTaken()
        {
            base.OnDamageTaken();

            // Starts the next phase.
            phase++;
            StartPhase();
        }

        // Run the AI for the Final Boss
        public override void UpdateAI()
        {
            base.UpdateAI();

            // If the boss isn't attacking, return.
            if (!attacking)
                return;

            // Gets set to 'true' if lightning has been struck.
            bool struckLightning = false;

            // While there are lightning strikes left.
            if(lightningQueue.Count > 0)
            {
                struckLightning = true;

                // Reduce timer.
                lightningTimer -= Time.deltaTime;
                
                // Fire off next pattern.
                if(lightningTimer <= 0)
                {
                    lightningTimer = 0;

                    // Gets the layout.
                    LightningStrikeLayout layout = lightningQueue.Dequeue();

                    // Spawn the lightning strikes.
                    SpawnLightningStrikes(layout);

                    // Set to the max.
                    lightningTimer = lightningTimerMax;
                }
            }

            // If there is still active lightning, consider the boss to still be attacking.
            if(!struckLightning)
            {
                struckLightning = activeLightningCount > 0;
            }

            // No attacks were done, so end the phase.
            if(!struckLightning)
            {
                EndPhase();
            }

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

        }

        // This function is called when the MonoBehaviour will be destroyed.
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}
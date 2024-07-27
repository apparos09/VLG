using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The final boss of the game.
    public class FinalBoss : Boss
    {
        // Outlines the lighting strike pattern.
        protected struct LightningStrikePattern
        {
            // The positions for the lighting strikes.
            public List<Vector2Int> positions;
        }

        [Header("Final Boss")]

        // The lighting strike prefab
        public LightningStrike lightningStrikePrefab;

        // The delay for the lighting strike patterns.
        private float strikeDelay = 1.0F;

        // The three lightning strike patterns.
        private Queue<LightningStrikePattern> phase1StrikePatterns = new Queue<LightningStrikePattern>();
        private Queue<LightningStrikePattern> phase2StrikePatterns = new Queue<LightningStrikePattern>();
        private Queue<LightningStrikePattern> phase3StrikePatterns = new Queue<LightningStrikePattern>();

        // The pool of lighitng strikes to pull from.
        private Queue<LightningStrike> lightningStrikePool = new Queue<LightningStrike>();

        // Constructor
        private FinalBoss()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // TOOD: load lightning patterns.
        }

        // Called on the first frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Lighting Strike Test - Make Sure to Remove
            SpawnLightingStrike(new Vector2Int(6, 0));

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
        }

        // Returns the lighting strike to the pool.
        public void ReturnLightingStrike(LightningStrike strike)
        {
            strike.gameObject.SetActive(false);
            lightningStrikePool.Enqueue(strike);
        }

        // Activates the hazards based on the current phase.
        public void ActivateHazards()
        {

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

        // Called when damage has been taken.
        public override void OnDamageTaken()
        {
            base.OnDamageTaken();

            // TODO: change phase.
        }

        // Run the AI for the Final Boss
        public override void UpdateAI()
        {
            base.UpdateAI();

            // Checks what phase the final boss in is.
            switch(phase)
            {
                case 0:
                case 1:
                default: // Phase 1

                    break;
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
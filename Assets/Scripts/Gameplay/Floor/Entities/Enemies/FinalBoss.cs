using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // TODO: have an intro animation that plays when the fight starts to give the player time to react.
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

        // The list of objects being used for attacks.
        private List<GameObject> attackObjects = new List<GameObject>();

        [Header("Final Boss")]

        // LIGHTNING
        [Header("Final Boss/Lightning")]
        // The lighting strike prefab
        public LightningStrike lightningStrikePrefab;

        // If the lighting strikes are enabled.
        public bool enabledLightningStrikes = true;

        // The lightning strike pattern.
        private Queue<LightningStrikeLayout> lightningQueue = new Queue<LightningStrikeLayout>();

        // The pool of laser strikes to pull from.
        private Queue<LightningStrike> lightningStrikePool = new Queue<LightningStrike>();

        // The lighting timer max.
        private float lightningTimerMax = 3.0F;

        // The countdown timer for firing off lightning.
        private float lightningTimer = 0.0F;

        // The number of active lightning attacks.
        private int activeLightningStrikesCount = 0;

        // LASER 
        [Header("Tracking Laser")]
        // The laser strike prefab.
        public TrackingLaser trackingLaserPrefab;

        // If the tracking lasers are enabled.
        public bool enabledTrackingLasers = true;

        // The pool of laser strikes to pull from.
        private Queue<TrackingLaser> trackingLasersPool = new Queue<TrackingLaser>();

        // The laser timer max.
        private float laserTimerMax = 3.0F;

        // The countdown timer for starting a laser attack.
        private float laserTimer = 0.0F;

        // The number of active tracking laser attacks.
        private int activeTrackingLasersCount = 0;

        [Header("Final Boss/Animations")]

        // The intro animation.
        public string introAnim = "";

        // If the intro animation should be used.
        private bool useIntroAnim = true;

        // Animation for raising the final boss.
        public string riseAnim = "";

        // Animation for dropping the final boss.
        public string dropAnim = "";

        [Header("Final Boss/Audio")]

        // Dragon roar sfx.
        public AudioClip dragonRoarSfx;

        // Dragon fly up sfx.
        public AudioClip dragonFlyUpSfx;

        // Dragon fly down SFX.
        public AudioClip dragonFlyDownSfx;

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

            // If the intro animation will play, stop the BGM.
            // This is to prevent a short play before it's cut-off.
            if (useIntroAnim)
                gameManager.gameAudio.bgmSource.Stop();
        }

        // Called on the first frame.
        protected override void PostStart()
        {
            base.PostStart();

            // Test
            // // Lighting Strike Test - Make Sure to Remove
            // SpawnLightingStrike(new Vector2Int(6, 0));

            // Plays the intro animation, or starts the phase.
            if(useIntroAnim)
                PlayBossIntroAnimation();
            else
                StartPhase();
        }

        // LIGHTNING //
        // Spawns a lighting strike at the provided position.
        public LightningStrike SpawnLightningStrike(Vector2Int strikePos)
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
            activeLightningStrikesCount++;

            // If the attack object is not in the list, add it.
            if (!attackObjects.Contains(strike.gameObject))
                attackObjects.Add(strike.gameObject);

            return strike;
        }

        // Returns the lighting strike to the pool.
        public void ReturnLightingStrike(LightningStrike strike)
        {
            strike.gameObject.SetActive(false);
            lightningStrikePool.Enqueue(strike);

            // Reduce active count.
            activeLightningStrikesCount--;
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
                        SpawnLightningStrike(gridPos);
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

            // Layout 1 - Alt Rows (V1)
            LightningStrikeLayout layout1 = new LightningStrikeLayout();
            layout1.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { true, false, true, false, true, false, true, false, true, false},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, true},
                { true, false, true, false, true, false, true, false, true, false},
            };

            // Layout 2 - Alt Rows (V2)
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

            // Layout 3 - Alt Cols (V1)
            LightningStrikeLayout layout3 = new LightningStrikeLayout();
            layout3.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false}
            };

            // Layour 4 - Alt Cols (V2)
            LightningStrikeLayout layout4 = new LightningStrikeLayout();
            layout4.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true},
                { false, false, false, false, false, false, false, false, false, false},
                { true, true, true, true, true, true, true, true, true, true}
            };

            // Layout 5 (Left to Right)
            LightningStrikeLayout layout5 = new LightningStrikeLayout();
            layout5.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true}
            };

            // Layout 6 - Diagonal (Right to Left)
            LightningStrikeLayout layout6 = new LightningStrikeLayout();
            layout6.positions = new bool[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS]{
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false},
                { false, true, false, true, false, true, false, true, false, true},
                { true, false, true, false, true, false, true, false, true, false}
            };


            // Clears the queue.
            lightningQueue.Clear();

            // Checks the phase to show what the pattern should be.
            switch (phase)
            {
                case 0:
                case 1: // 4
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);

                    break;

                case 2: // 6
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);
                    lightningQueue.Enqueue(layout5);
                    lightningQueue.Enqueue(layout6);
                    break;

                case 3: // 8
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);
                    lightningQueue.Enqueue(layout5);
                    lightningQueue.Enqueue(layout6);

                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    break;

                case 4: // 10
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);
                    lightningQueue.Enqueue(layout5);
                    lightningQueue.Enqueue(layout6);

                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);
                    break;

                case 5: // 12
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);
                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);

                    lightningQueue.Enqueue(layout5);
                    lightningQueue.Enqueue(layout6);
                    lightningQueue.Enqueue(layout1);
                    lightningQueue.Enqueue(layout2);

                    lightningQueue.Enqueue(layout3);
                    lightningQueue.Enqueue(layout4);
                    lightningQueue.Enqueue(layout5);
                    lightningQueue.Enqueue(layout6);
                    break;
            }
        }

        // Sets the max of the lightning timer based on the current phase.
        public void SetLightningStrikeTimerMax()
        {
            // Checks the phase for the new lighting max value.
            switch (phase)
            {
                case 0:
                case 1:
                default:
                    lightningTimerMax = 3.50F;
                    break;

                case 2:
                    lightningTimerMax = 3.25F;
                    break;

                case 3:
                    lightningTimerMax = 3.00F;
                    break;

                case 4:
                    lightningTimerMax = 2.75F;
                    break;

                case 5:
                    lightningTimerMax = 2.50F;
                    break;
            }
        }



        // LASERS //
        // Spawns a laser strike at the provided position.
        public TrackingLaser SpawnTrackingLaser()
        {
            // The laser strike object.
            TrackingLaser laser;

            // Checks if there's already a saved lightning strike object.
            if (trackingLasersPool.Count > 0)
            {
                laser = trackingLasersPool.Dequeue();
            }
            else // No object.
            {
                // Generate a new one.
                laser = Instantiate(trackingLaserPrefab);

                // Parent
                laser.transform.parent = transform.parent;

                // Return the lighting strike object to the pool when it's done.
                laser.OnLaserStrikeEndAddCallback(ReturnTrackingLaser);
            }

            // Activates the object and triggers a tracking.
            laser.gameObject.SetActive(true);
            laser.StartTrackingPlayer();

            // There is tracking lightning.
            activeTrackingLasersCount++;

            // If the attack object is not in the list, add it.
            if (!attackObjects.Contains(laser.gameObject))
                attackObjects.Add(laser.gameObject);

            return laser;
        }

        // Returns the tracking laser to the pool.
        public void ReturnTrackingLaser(TrackingLaser laser)
        {
            laser.gameObject.SetActive(false);
            trackingLasersPool.Enqueue(laser);

            // Reduce active count.
            activeTrackingLasersCount--;
        }

        // Sets the max of the laser timer based on the current phase.
        public void SetTrackingLaserTimerMax()
        {
            // Checks the phase for the new laser timer max value.
            switch (phase)
            {
                case 0:
                case 1:
                default:
                    laserTimerMax = 5.00F;
                    break;

                case 2:
                    laserTimerMax = 4.75F;
                    break;

                case 3:
                    laserTimerMax = 4.50F;
                    break;

                case 4:
                    laserTimerMax = 4.25F;
                    break;

                case 5:
                    laserTimerMax = 4.00F;
                    break;
            }
        }

        // HAZARDS //

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

       
        // PHASE

        // Start the next phase.
        public void StartPhase()
        {
            // Activates the hazards, generates the lightning pattern.
            ActivateHazards();
            GenerateLightningPattern();

            // Set the timers to max.
            SetLightningStrikeTimerMax();
            SetTrackingLaserTimerMax();

            // Lightning strikes are always enabled.
            enabledLightningStrikes = true;

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

            // Delay the start of the round.
            lightningTimer = 1.5F;
            laserTimer = 2.5F;
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

        // ANIMATIONS //
        // Called to play the boss's intro animation.
        public void PlayBossIntroAnimation()
        {
            animator.Play(introAnim);
        }
        
        // Called when the boss intro animation starts.
        public void OnBossIntroAnimationStart()
        {
            // Play the attack animation, and stop the player from moving.
            PlayAttackAnimation();
            gameManager.player.enabledInputs = false;

            // Stops the background music.
            gameManager.gameAudio.bgmSource.Stop();
        }

        // Called when the boss intro animation is finished.
        public void OnBossIntroAnimationEnd()
        {
            // Play the idle animation.
            PlayIdleAnimation();

            // Start the phase.
            gameManager.player.enabledInputs = true;
            StartPhase();

            // Plays the background music. The clip should already be set.
            gameManager.gameAudio.bgmSource.Play();
        }


        // AUDIO
        // Plays the dragon roar sound effect.
        public void PlayDragonRoarSfx()
        {
            if (dragonRoarSfx != null)
                gameManager.gameAudio.PlaySoundEffect(dragonRoarSfx);
        }

        // Plays the dragon fly up sound effect.
        public void PlayDragonFlyUpSfx()
        {
            if (dragonFlyUpSfx != null)
                gameManager.gameAudio.PlaySoundEffect(dragonFlyUpSfx);
        }

        // Plays the dragon fly down sound effect.
        public void PlayDragonFlyDownSfx()
        {
            if (dragonFlyDownSfx != null)
                gameManager.gameAudio.PlaySoundEffect(dragonFlyDownSfx);
        }


        // Updates the AI

        // Run the AI for the Final Boss
        public override void UpdateAI()
        {
            base.UpdateAI();

            // If the boss isn't attacking, return.
            if (!attacking)
                return;

            // Gets set to 'true' if lightning has been struck.
            bool struckLightning = false;

            // If lightning strikes are enabled.
            if (enabledLightningStrikes)
            {
                // While there are lightning strikes left.
                if (lightningQueue.Count > 0)
                {
                    struckLightning = true;

                    // Reduce timer.
                    lightningTimer -= Time.deltaTime;

                    // Shoot lightning.
                    if (lightningTimer <= 0)
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
                if (!struckLightning)
                {
                    struckLightning = activeLightningStrikesCount > 0;
                }
            }

            //// NOTE: the boss comes down without chekcing if all the laser strikes were done.
            
            // If the lasers are enabled.
            if(enabledTrackingLasers)
            {
                // Reduce timer.
                laserTimer -= Time.deltaTime;

                // Activate laser.
                if (laserTimer <= 0)
                {
                    laserTimer = 0;

                    // Spawn a laser strike.
                    SpawnTrackingLaser();

                    // Set the timer to max.
                    laserTimer = laserTimerMax;
                }
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

            // Destroys all the attack objects.
            for(int i = 0; i < attackObjects.Count; i++)
            {
                // Destroy the object.
                if (attackObjects[i] != null)
                    Destroy(attackObjects[i].gameObject);
            }

            // Clears the attack objects.
            attackObjects.Clear();
        }

    }
}
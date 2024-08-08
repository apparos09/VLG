using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VLG
{
    // An attack that follows the player around before it strikes.
    public class TrackingLaser : Enemy
    {
        [Header("Laser Strike")]
        // The effect and the target.
        public SpriteRenderer targetSprite;
        public SpriteRenderer laserStrike;
        
        // If the player should be tracked.
        public bool trackPlayer = true;

        // The maximum time for tracking the player's position.
        public float trackingTimerMax = 5.0F;

        // The timer for tracking the player's position.
        public float trackingTimer = 0.0F;

        // The speed of the movement when tracking the player.
        [Tooltip("The speed of the movement when tracking the player.")]
        public float trackingSpeed = 1.0F;

        // If 'true', the laser tracks the player's position instantly.
        // If false, it lags behind the player.
        public bool instantTracking = true;

        // Laser Strike Start Animation
        public string laserStrikeAnim = "Laser Strike Animation";

        // The laser callback.
        public delegate void LaserStrikeCallback(TrackingLaser laser);

        // The laser start callback.
        private LaserStrikeCallback laserStartCallback;

        // The laser end callback.
        private LaserStrikeCallback laserEndCallback;

        // The laser sound effect.
        public AudioClip laserBlastSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set parent to the floor origin if it's not set already. 
            if (transform.parent == null)
                transform.parent = floorManager.floorOrigin.transform;

            // Start tracking the player from the beginning.
            StartTrackingPlayer();
        }

        // Start tracking the player.
        public void StartTrackingPlayer()
        {
            trackPlayer = true;
            trackingTimer = trackingTimerMax;

            // Activate Target
            // I don't think this works because they're tied to animations, so the laser is off by default.
            targetSprite.gameObject.SetActive(true);
            laserStrike.gameObject.SetActive(false);

            // Set to the player's position at the start.
            SetFloorPosition(gameManager.player.floorPos, true, false);

            // Go to the player's position.
            MoveTowardsPlayer();
        }

        // Set to the player position.
        public void MoveTowardsPlayer()
        {
            Player player = gameManager.player;

            // If the player's position should be instantly jumped to.
            if(instantTracking)
            {
                // Get the new position.
                Vector3 newPos = player.transform.position;
                newPos.y = transform.position.y;

                // Change saved floor position.
                SetFloorPosition(player.floorPos, false, false);

                // Set world position.
                transform.position = newPos;
            }
            else // Move towards player.
            {
                // Current
                Vector3 pos1 = transform.position;

                // Target
                Vector3 pos2 = player.transform.position;
                pos2.y = pos1.y;

                // Result
                Vector3 newPos = Vector3.MoveTowards(pos1, pos2, trackingSpeed * Time.deltaTime);

                // Change saved floor position.
                SetFloorPosition(player.floorPos, false, false);

                // Set world position.
                transform.position = newPos;
            }
        }

        // Shoots the Laser
        public void ShootLaser()
        {
            trackPlayer = false;
            trackingTimer = 0;

            // Play the animation.
            animator.Play(laserStrikeAnim);
        }

        // Called when the laser has started moving.
        public void OnLaserStrikeStart()
        {
            // Sprite and Model
            targetSprite.gameObject.SetActive(false);
            laserStrike.gameObject.SetActive(true);

            // Callbacks.
            if (laserStartCallback != null)
                laserStartCallback(this);
        }

        // Called when the laser has stopped moving.
        public void OnLaserStrikeEnd()
        {
            // Start tracking the player again.
            StartTrackingPlayer();

            // Callbacks.
            if (laserEndCallback != null)
                laserEndCallback(this);
        }

        // CALLBACKS //
        // Laser Strike Start Add Callback
        public void OnLaserStrikeStartAddCallback(LaserStrikeCallback callback)
        {
            laserStartCallback += callback;
        }

        // Laser Strike Start Remove Callback
        public void OnLaserStrikeStartRemoveCallback(LaserStrikeCallback callback)
        {
            laserStartCallback -= callback;
        }

        // Laser Strike Start Add Callback
        public void OnLaserStrikeEndAddCallback(LaserStrikeCallback callback)
        {
            laserEndCallback += callback;
        }

        // Laser Strike Start Remove Callback
        public void OnLaserStrikeEndRemoveCallback(LaserStrikeCallback callback)
        {
            laserEndCallback -= callback;
        }

        // Plays the laser shot sfx.
        public void PlayLaserBlastSfx()
        {
            if(laserBlastSfx != null)
                gameManager.gameAudio.PlaySoundEffect(laserBlastSfx);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // If the game is running.
            if(!gameManager.IsPaused())
            {
                // If the player should be tracked.
                if (trackPlayer)
                {
                    // If the tracking timer
                    if (trackingTimer > 0)
                    {
                        // Reduce Timer
                        trackingTimer -= Time.deltaTime;

                        // If less than 0, stop tracking and launch laser.
                        if (trackingTimer <= 0)
                        {
                            trackingTimer = 0.0F;

                            ShootLaser();
                        }
                        else
                        {
                            // Follow the player's position.
                            MoveTowardsPlayer();
                        }
                    }
                }
            }
            
            
        }
    }
}
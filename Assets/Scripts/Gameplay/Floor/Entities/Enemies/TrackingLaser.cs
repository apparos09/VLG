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
        public float trackingTimerMax = 3.0F;

        // The timer for tracking the player's position.
        public float trackingTimer = 0.0F;

        // Laser Strike Start Animation
        public string laserStrikeAnim = "Laser Strike Animation";

        // The laser callback.
        public delegate void LaserStrikeCallback(TrackingLaser laser);

        // The laser start callback.
        private LaserStrikeCallback laserStartCallback;

        // The laser end callback.
        private LaserStrikeCallback laserEndCallback;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Start tracking the player from the beginning.
            StartTrackingPlayer();
        }

        // Start tracking the player.
        public void StartTrackingPlayer()
        {
            trackPlayer = true;
            trackingTimer = trackingTimerMax;

            // Activate Target
            targetSprite.gameObject.SetActive(true);
            laserStrike.gameObject.SetActive(false);

            // Go to the player's position.
            SetToPlayerPosition();
        }

        // Set to the player position.
        public void SetToPlayerPosition()
        {
            // Get the new position.
            Vector3 newPos = gameManager.player.transform.position;
            newPos.y = transform.position.y;

            // Set.
            transform.position = newPos;
        }

        // Shoots the Laser
        public void ShootLaser()
        {
            trackPlayer = false;
            trackingTimer = 0;

            // Activate Laser
            targetSprite.gameObject.SetActive(false);
            laserStrike.gameObject.SetActive(true);
        }

        // Called when the laser has started moving.
        public void OnLaserStrikeStart()
        {
            // Sprite and Model
            targetSprite.gameObject.SetActive(false);
            laserStrike.gameObject.SetActive(true);

            animator.Play(laserStrikeAnim);

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
                            SetToPlayerPosition();
                        }
                    }
                }
            }
            
            
        }
    }
}
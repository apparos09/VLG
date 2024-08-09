using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A block with a hazard on it.
    public class HazardBlock : Block
    {
        [Header("Hazard Block")]

        // The spikes for the hazard block.
        public GameObject spikesModel;

        // The state of the hazard upon the game starting (on/off).
        [Tooltip("Determines the starting state of the hazard (on/off).")]
        public bool hazardOnDefault = true;

        // Determines if the hazard is on or off.
        protected bool hazardOn = true;

        [Header("Hazard Block/Animations")]

        // If animations are being used.
        public bool useAnimations = true;

        // The hazard on and off animations.
        public string hazardOnAnim = "Hazard Block - Hazard On Animation";
        public string hazardOffAnim = "Hazard Block - Hazard Off Animation";

        [Header("Hazard Block/Audio")]

        // If 'true', sounds can be overlayed.
        [Tooltip("If 'true', the same sounds can be overlayed with one another. If false, only one is allowed to play at a time.")]
        public bool overlaySameSounds = true;

        // The hazard block on sfx.
        public AudioClip hazardBlockOnSfx;

        // The hazard block on sfx timer.
        private static float hazardBlockOnSfxTimer = 0.0F;

        // The hazard block off sfx.
        public AudioClip hazardBlockOffSfx;

        // The hazard block off sfx timer.
        private static float hazardBlockOffSfxTimer = 0.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the default hazard value.
            SetHazardOn(hazardOnDefault, false);
        }

        // Returns a bool to indicate if the hazard is on.
        public bool IsHazardOn()
        {
            return hazardOn;
        }

        // Sets whether the hazard is enabled or disabled.
        public virtual void SetHazardOn(bool value, bool animate = true)
        {
            hazardOn = value;

            // Animate
            if(animate)
            {
                // Plays the proper animation based on the setting of the hazard.
                if (hazardOn)
                    PlayHazardOnAnimation();
                else
                    PlayHazardOffAnimation();
            }
            else // Don't animate
            {
                spikesModel.SetActive(hazardOn);
            }

            // Checks the entities on the block.
            CheckEntitiesOnBlock();
        }

        // Enables the hazard.
        public void EnableHazard(bool animate = true)
        {
            SetHazardOn(true, animate);
        }

        // Disables the hazard.
        public void DisableHazard(bool animate = true)
        {
            SetHazardOn(false, animate);
        }

        // Toggle the hazard on/off.
        public void ToggleHazard(bool animate = true)
        {
            SetHazardOn(!hazardOn);
        }

        // Called by a ButtonBlock event.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            base.OnButtonBlockClicked(entity);

            // Toggles the hazard on/off.
            ToggleHazard();
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // If the hazard is on.
            if (hazardOn)
            {
                entity.KillEntity();
            }
            
        }

        // Checks for an entity is standing on the hazard block.
        public void CheckEntitiesOnBlock()
        {
            // If the block has the hazard off, do nothing.
            if (!hazardOn)
                return;

            // If the floor position is valid.
            if (floorManager.IsFloorPositionValid(floorPos))
            {
                // If the player is standing on the block, and the hazard is active, kill the player.
                if (gameManager.player.floorPos == floorPos)
                {
                    gameManager.player.KillEntity();
                }

                // Tries to find the enemy.
                Enemy enemy = floorManager.GetFloorEnemyEntity(floorPos);

                // The enemy object exists.
                if (enemy != null)
                {
                    // If the enemy shouldn't ignore the geometry, kill the enemy. 
                    if (!enemy.ignoreGeometry)
                        enemy.KillEntity();
                }
            }

        }

        // ANIMATIONS //
        // Plays the hazard on animation.
        private void PlayHazardOnAnimation()
        {
            animator.Play(hazardOnAnim);
        }

        // Plays the hazard off animation.
        public void PlayHazardOffAnimation()
        {
            animator.Play(hazardOffAnim);
        }


        // AUDIO
        // Plays the hazard on sfx.
        public void PlayHazardBlockOnSfx()
        {
            // Sound effect is set.
            if (hazardBlockOnSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(hazardBlockOnSfx);
                }
                else
                {
                    // Sound clip not playing, so allow the SFX to play.
                    if (hazardBlockOnSfxTimer <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(hazardBlockOnSfx);
                        hazardBlockOnSfxTimer = hazardBlockOnSfx.length;
                    }
                }
            }
        }

        // Plays the hazard off sfx.
        public void PlayHazardBlockOffSfx()
        {
            // Sound effect is set.
            if (hazardBlockOffSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(hazardBlockOffSfx);
                }
                else
                {
                    // Sound clip not playing, so allow the SFX to play.
                    if (hazardBlockOffSfxTimer <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(hazardBlockOffSfx);
                        hazardBlockOffSfxTimer = hazardBlockOffSfx.length;
                    }
                }
            }
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // If the hazard was changed using an animation, it must be put back to its default...
            // Using an animation as well, for some reason.
            SetHazardOn(hazardOnDefault, useAnimations); // Set value to default.
        }

        // Update is called once per frame
        protected override void Update()
        {
            // Hazard On Timer
            if (hazardBlockOnSfxTimer > 0)
            {
                // Reduce timer by unscaled time since it's based on the length of the sound effect.
                hazardBlockOnSfxTimer -= Time.unscaledDeltaTime;

                // Bounds
                if (hazardBlockOnSfxTimer <= 0)
                    hazardBlockOnSfxTimer = 0.0F;
            }

            // Hazard Off Timer
            if (hazardBlockOffSfxTimer > 0)
            {
                // Reduce timer by unscaled time since it's based on the length of the sound effect.
                hazardBlockOffSfxTimer -= Time.unscaledDeltaTime;

                // Bounds
                if (hazardBlockOffSfxTimer <= 0)
                    hazardBlockOffSfxTimer = 0.0F;

            }

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The portal block.
    public class PortalBlock : Block
    {
        [Header("Portal Block")]

        // The end portal that this portal is connected to.
        [Tooltip("The destination portal.")]
        public PortalBlock destPortal;

        // Automically sets the end portal.
        [Tooltip("Automatically looks for the end portal, which is a portal that shares the same version.")]
        public bool autoSetDestPortal = true;

        // The tags for triggering a portal.
        [Tooltip("Valid interactions for the portal. If none are listed, all entities can use the portal")]
        public List<string> validTags = new List<string>();

        // Determines if the button is locked or unlocked.
        private bool locked = false;

        // The list of portals in the game.
        private static List<PortalBlock> portalBlocks = new List<PortalBlock>();

        // Gets set to 'true' when the post start function has been called.
        private bool calledPostStart = false;

        [Header("Portal Block/Animations")]

        // The idle animation.
        public string portalIdleAnim = "Portal Block - Idle Animation";

        // Portal Block - On Animation
        public string portalOnAnim = "Portal Block - On Animation";

        // Portal Block - Off Animation
        public string portalOffAnim = "Portal Block - Off Animation";

        [Header("Portal Block/Audio")]

        [Tooltip("If 'true', the same sounds can be overlayed with one another. If false, only one is allowed to play at a time.")]
        public bool overlaySameSounds = true;

        // Portal block activation sfx.
        public AudioClip portalActivateSfx;

        // Portal activation timer.
        private static float portalActivateSfxTimer = 0.0F;

        // Portal block deactivation sfx.
        public AudioClip portalDeactivateSfx;

        // Portal deactivation timer.
        private static float portalDeactivateSfxTimer = 0.0F;

        // Warp sfx - this doesn't need a timer since warps don't all happen at the same time.
        public AudioClip portalWarpSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Add to the list.
            if(!portalBlocks.Contains(this))
                portalBlocks.Add(this);

            // If the idle animation is set.
            if(portalIdleAnim != string.Empty)
            {
                PlayPortalIdleAnimation();
            }
        }

        // Called on the first update frame.
        protected virtual void PostStart()
        {
            // If the end portal needs to be set.
            if(destPortal == null && autoSetDestPortal)
            {
                SearchForDestinationPortal();
            }        

            // Post start has been called.
            calledPostStart = true;
        }

        // Search for the end portal.
        public void SearchForDestinationPortal()
        {
            // Set to null and search for it again.
            destPortal = null;

            // Goes through all he portalBlocks.
            foreach (PortalBlock portalBlock in portalBlocks)
            {
                // Add to the poral queue if the versions match.
                if (portalBlock != this)
                {
                    // If a valid end portal has been found, and the end portal is either not set...
                    // Or is set to this portal, add to the list.
                    if (portalBlock.version == version && (portalBlock.destPortal == null || portalBlock.destPortal == this))
                    {
                        // Sets the destination portal.
                        destPortal = portalBlock;
                        portalBlock.destPortal = this;
                        break;
                    }
                }

            }
        }

        // Is the button locked?
        public bool IsLocked()
        {
            return locked;
        }

        // Sets if the button is locked on unlocked.
        public void SetLocked(bool value)
        {
            locked = value;

            // Play animation
            if (locked) // Off
            {
                PlayPortalOffAnimation();
            }
            else // On
            {
                PlayPortalOnAnimation();
            }
        }

        // Locks the portal.
        public void LockPortal()
        {
            SetLocked(true);
        }

        // Unlocks the portal.
        public void UnlockPortal()
        {
            SetLocked(false);
        }

        // Toggles the portal being locked/unlocked.
        public void TogglePortalLocked()
        {
            SetLocked(!locked);
        }

        // Called by a ButtonBlock event.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            base.OnButtonBlockClicked(entity);

            TogglePortalLocked();
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // Don't do anything if the portal is locked.
            if (locked)
                return;

            // The end portal is not connected.
            if(destPortal == null)
            {
                Debug.LogError("No end portal is set, so warp can't happen.");
                return;
            }

            // Checks for valid tags.
            bool warp = validTags.Count == 0 || validTags.Contains(entity.tag);

            // If 'true', warp the entity.
            if (warp)
            {
                // Set the position.
                entity.SetFloorPosition(destPortal.floorPos, false, false);

                // Warp sound effect.
                PlayPortalWarpSfx();
            }
        }

        // ANIMATIONS
        // Play the Portal Idle Animation
        public void PlayPortalIdleAnimation()
        {
            animator.Play(portalIdleAnim);
        }

        // Play the Portal On Animation
        private void PlayPortalOnAnimation()
        {
            animator.Play(portalOnAnim);
        }

        // Play the Portal Off Animation
        private void PlayPortalOffAnimation()
        {
            animator.Play(portalOffAnim);
        }

        // AUDIO
        // Plays the activation sound.
        public void PlayPortalActivationSfx()
        {
            // Sound effect is set.
            if (portalActivateSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(portalActivateSfx);
                }
                else
                {
                    // Sound clip not playing, so allow the SFX to play.
                    if (portalActivateSfxTimer <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(portalActivateSfx);
                        portalActivateSfxTimer = portalActivateSfx.length;
                    }
                }
            }
        }

        // Plays the deactivation sound.
        public void PlayPortalDeactivationSfx()
        {
            // Sound effect is set.
            if (portalDeactivateSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(portalDeactivateSfx);
                }
                else
                {
                    // Sound clip not playing, so allow the SFX to play.
                    if (portalDeactivateSfxTimer <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(portalDeactivateSfx);
                        portalDeactivateSfxTimer = portalDeactivateSfx.length;
                    }
                }
            }
        }

        // Plays the warp sound.
        public void PlayPortalWarpSfx()
        {
            // Sound effect is set.
            if (portalWarpSfx != null)
            {
                // Play the sound effect.
                gameManager.gameAudio.PlaySoundEffect(portalWarpSfx);
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            // Calls the post start function if it hasn't been called already.
            if (!calledPostStart)
                PostStart();

            // Base Update
            base.Update();


            // Portal Activation Timer
            if (portalActivateSfxTimer > 0)
            {
                // Reduce timer by unscaled time since it's based on the length of the sound effect.
                portalActivateSfxTimer -= Time.unscaledDeltaTime;

                // Bounds
                if (portalActivateSfxTimer <= 0)
                    portalActivateSfxTimer = 0.0F;
            }

            // Portal Deactivation Timer
            if (portalDeactivateSfxTimer > 0)
            {
                // Reduce timer by unscaled time since it's based on the length of the sound effect.
                portalDeactivateSfxTimer -= Time.unscaledDeltaTime;

                // Bounds
                if (portalDeactivateSfxTimer <= 0)
                    portalDeactivateSfxTimer = 0.0F;
            }

        }

        // Remove from the switch block list.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the block list.
            if (portalBlocks.Contains(this))
                portalBlocks.Remove(this);
        }
    }
}
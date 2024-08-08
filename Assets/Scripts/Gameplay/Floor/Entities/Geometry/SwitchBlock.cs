using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // NOTE: you may not use this due to its mechanics being iffy and likely unnecessary.
    // For now, this is unused, and has not been tested.

    // The switch block - turns on/off when the player jumps.
    public class SwitchBlock : Block
    {
        [Header("Switch Block")]

        // Determines default state of the block (on/off)
        [Tooltip("If 'true', the block is on by default. If false, the block is off by default.")]
        public bool blockOnDefault = true;

        // The block is on/off
        protected bool blockOn = true;

        // If 'true', the block is usable when it's off. 
        [Tooltip("If 'true', the block is usable no matter the state.")]
        public bool usableWhenOff = false;

        // If 'true', the block kills the entity on it if it's off.
        [Tooltip("Kills the entity on the block when it's off if this is true.")]
        public bool killWhenOff = true;
        
        // The list of active switch blocks.
        public static List<SwitchBlock> switchBlocks = new List<SwitchBlock>();

        [Header("Switch Block/Animations")]

        // The on animation.
        public string onAnim = "Switch Block - On Animation";

        // The off animation.
        public string offAnim = "Switch Block - Off Animation";

        // If 'true', the blocks use the animations.
        private bool useAnims = true;

        [Header("Switch Block/Audio")]

        [Tooltip("If 'true', the same sounds can be overlayed with one another. If false, only one is allowed to play at a time.")]
        public bool overlaySameSounds = true;

        // The switch on SFX.
        public AudioClip switchOnSfx;

        // Calles for the switch on SFX.
        static private int switchOnCalls = 0;

        // The switch off SFX.
        public AudioClip switchOffSfx;

        // Calles for the switch off SFX.
        static private int switchOffCalls = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Default setting.
            SetBlockOn(blockOnDefault);

            // Add to the list.
            if(!switchBlocks.Contains(this))
                switchBlocks.Add(this);
        }

        // This function is called when the object has become enabled and active
        protected override void OnEnable()
        {
            base.OnEnable();

            // The switch block is enabled, so add it to the list.
            if (!switchBlocks.Contains(this))
                switchBlocks.Add(this);
        }

        // This function is called when the behaviour has become disabled or inactive
        protected override void OnDisable()
        {
            base.OnDisable();

            // The switch block is disabled, so remove it from the list.
            if (switchBlocks.Contains(this))
                switchBlocks.Remove(this);
        }

        // Gets the switch block count.
        public static int GetSwitchBlockCount()
        {
            return switchBlocks.Count;
        }

        // Returns a bool to indicate if the hazard is on.
        public bool IsBlockOn()
        {
            return blockOn;
        }

        // Sets whether the hazard is enabled or disabled.
        public virtual void SetBlockOn(bool value)
        {
            blockOn = value;

            // If the animations should be used.
            if(useAnims)
            {
                // Checks if the on or off animations should be used.
                if (blockOn) // On
                    animator.Play(onAnim);
                else // Off
                    animator.Play(offAnim);
            }
            else // No animation.
            {
                gameObject.SetActive(value);
            }

            // Checks for entities on the platform.
            if(!blockOn && killWhenOff) // The block is off.
            {
                // Player
                if (gameManager.player.floorPos == floorPos)
                {
                    gameManager.player.KillEntity();
                }

                // Enemy
                if (floorManager.floorEnemies[floorPos.x, floorPos.y] != null)
                {
                    floorManager.floorEnemies[floorPos.x, floorPos.y].KillEntity();
                }
            }
            
        }

        // Turns the block on.
        public void TurnBlockOn()
        {
            SetBlockOn(true);
        }

        // Turns the block off.
        public void TurnBlockOff()
        {
            SetBlockOn(false);
        }

        // Toggles the block.
        public void ToggleBlock()
        {
            SetBlockOn(!blockOn);
        }

        // Called to see if this block is valid to use.
        public override bool UsableBlock(FloorEntity entity)
        {
            // If the block is active, it can be used.
            return blockOn;
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // ...
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetBlockOn(blockOnDefault); // Set value to default.
        }

        // ANIMATION
        // Switch On Animation
        public void PlaySwitchOnAnimation()
        {
            animator.Play(onAnim);
        }

        // Switch On Animation End
        public void OnSwitchOnAnimationEnd()
        {
            switchOnCalls--;

            // Bounds
            if (switchOnCalls < 0)
                switchOnCalls = 0;
        }

        // Switch Off Animation
        public void PlaySwitchOffAnimation()
        {
            animator.Play(offAnim);
        }

        // Switch Off Animation End
        public void OnSwitchOffAnimationEnd()
        {
            switchOffCalls--;

            // Bounds
            if (switchOffCalls < 0)
                switchOffCalls = 0;
        }


        // AUDIO
        // Plays the switch on SFX.
        public void PlaySwitchOnSfx()
        {
            // Sound effect is set.
            if (switchOnSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(switchOnSfx);
                }
                else
                {
                    // No calls have been made, so allow the SFX to play.
                    if (switchOnCalls <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(switchOnSfx);
                        switchOnCalls++;
                    }
                }
            }
        }

        // Plays the switch off SFX.
        public void PlaySwitchOffSfx()
        {
            // Sound effect is set.
            if (switchOffSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(switchOffSfx);
                }
                else
                {
                    // No calls have been made, so allow the SFX to play.
                    if (switchOffCalls <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(switchOffSfx);
                        switchOffCalls++;
                    }
                }
            }
        }



        // Remove from the switch block list.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the block list.
            if(switchBlocks.Contains(this))
                switchBlocks.Remove(this);

            // If there are no switch blocks left, reset the call counts.
            if (GetSwitchBlockCount() <= 0)
            {
                switchOnCalls = 0;
                switchOffCalls = 0;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The phase block, which goes from tangible to intangible (and vice-versa)
    public class PhaseBlock : Block
    {
        [Header("PhaseBlock")]
        // The phase start upon the game starting (tangible/instangible).
        [Tooltip("Determines the starting state of the phase block (tangible/intangible).")]
        public bool tangibleDefault = true;

        // Sets if the phase block is tangible or intangible.
        protected bool tangible = true;

        // The block material.
        public Material blockMaterial;

        // The cover material.
        public Material coverMaterial;

        // The tangible animation.
        public string tangibleAnim;

        // The intangible animation.
        public string intangibleAnim;

        [Header("PhaseBlock/Audio")]

        // Phase In Sfx
        public AudioClip phaseInSfx;

        // Phase Out Sfx
        public AudioClip phaseOutSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the tangible state.
            SetTangible(tangibleDefault, false);
        }

        // Returns the tangibility of the object.
        public bool IsTangible()
        {
            return tangible;
        }

        // Sets that the object is tangible.
        public virtual void SetTangible(bool value, bool animate = true)
        {
            tangible = value;

            // gameObject.SetActive(value); // Old

            // New
            // Play Tangible/Intangible Animation
            if (tangible)
                PlayTangibleAnimation();
            else
                PlayIntangibleAnimation();

            // Checks for entity standing on the phase block.
            CheckEntitiesOnBlock();
        }    
        
        // Makes the block tangible.
        public void MakeTangible(bool animate = true)
        {
            SetTangible(true, animate);
        }

        // Makes the block intangible.
        public void MakeIntangible(bool animate = true)
        {
            SetTangible(false, animate);
        }

        // Toggles the tangibility of the phase block.
        public void ToggleTangible()
        {
            SetTangible(!tangible);
        }

        // Called to see if this block is valid to use.
        public override bool UsableBlock(FloorEntity entity)
        {
            // If the block is tangle, it is usable.
            // If the block is intangible, it is not.
            return tangible;
        }

        // Called by a ButtonBlock event.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            base.OnButtonBlockClicked(entity);

            // Toggle tangibility.
            ToggleTangible();
        }

        // Checks for an entity standing on the phase block.
        public void CheckEntitiesOnBlock()
        {
            // If the block is tangible, do nothing.
            if (tangible)
                return;

            // If the floor position is valid.
            if (floorManager.IsFloorPositionValid(floorPos))
            {
                // If the player is standing on the block, and the block is now intangible, kill the player.
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
        // Plays the tangible animation.
        public void PlayTangibleAnimation()
        {
            animator.Play(tangibleAnim);
        }

        // Plays the intangible animation.
        public void PlayIntangibleAnimation()
        {
            animator.Play(intangibleAnim);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetTangible(tangibleDefault, false); // Set value to default.
        }

        // Plays the phase in sound effect.
        public void PlayPhaseInSfx()
        {
            if (phaseInSfx != null)
                gameManager.gameAudio.PlaySoundEffect(phaseInSfx);
        }

        // Plays the phase out sound effect.
        public void PlayPhaseOutSfx()
        {
            if (phaseOutSfx != null)
                gameManager.gameAudio.PlaySoundEffect(phaseOutSfx);
        }
    }
}
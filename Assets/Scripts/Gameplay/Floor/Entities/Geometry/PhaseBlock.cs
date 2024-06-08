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

            // Play Tangible/Intangible Animation
            if (tangible)
                PlayTangibleAnimation();
            else
                PlayIntangibleAnimation();

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

        // Plays the tangible animation.
        private void PlayTangibleAnimation()
        {
            animator.Play("Phase Block - Tangible Animation");
        }

        // Plays the intangible animation.
        private void PlayIntangibleAnimation()
        {
            animator.Play("Phase Block - Intangible Animation");
        }


        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetTangible(tangibleDefault, false); // Set value to default.
        }
    }
}
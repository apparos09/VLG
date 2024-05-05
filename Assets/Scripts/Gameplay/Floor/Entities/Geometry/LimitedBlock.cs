using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A block with a limited number of uses.
    public class LimitedBlock : Block
    {
        [Header("LimitedBlock")]

        // The limited block.
        public int usesMax = 3;

        // The number of uses.
        protected int uses = 0;

        // If 'true', the block cannot be jumped on if it has no uses.
        [Tooltip("If true, the block is unusable if there are no uses left.")]
        public bool unusableIfNoUses = true;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the uses to the max value.
            SetUsesCountToMax(false);
        }


        // Returns the uses count.
        public int GetUsesCount()
        {
            return uses;
        }

        // Sets the uses count within the limits of 0 and usesMax.
        public virtual void SetUsesCount(int newCount, bool animate = true)
        {
            // Sets uses.
            uses = Mathf.Clamp(newCount, 0, usesMax);
        }

        // Adds to the number of uses.
        public void IncreaseUsesCount(int increase, bool animate = true)
        {
            SetUsesCount(uses + increase, animate);
        }

        // Reduces the number of uses.
        public void DecreaseUsesCount(int decrease, bool animate = true)
        {
            SetUsesCount(uses - decrease, animate);
        }

        // Sets the uses count to max.
        public void SetUsesCountToMax(bool animate = true)
        {
            SetUsesCount(usesMax, animate);
        }

        // Returns 'true' if there are uses left.
        public bool HasUsesLeft()
        {
            return uses > 0;
        }

        // Called to see if this block is valid to use.
        public override bool UsableBlock()
        {
            // If the block shouldn't be usuable if no uses are left.
            if(unusableIfNoUses)
            {
                // Checks if there are uses left.
                if(HasUsesLeft())
                {
                    return true;
                } 
                else // No uses left.
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // Reduce the uses by 1.
            if(entity is Player)
                DecreaseUsesCount(1);

        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetUsesCount(usesMax);
        }
    }
}
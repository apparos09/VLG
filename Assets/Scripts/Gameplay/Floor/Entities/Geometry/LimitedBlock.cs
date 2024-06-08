using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VLG
{
    // A block with a limited number of uses.
    public class LimitedBlock : Block
    {
        [Header("LimitedBlock")]

        // The limited block.
        // Every time an entity jumps on a block, its uses drops by 1.
        // When the uses count drops to 0, the block breaks, and the entity on it is killed.
        [Tooltip("The total amount of uses the block gets. This goes down everytime an entity jumps on it.")]
        public int usesMax = 4;

        // The number of uses.
        protected int uses = 0;

        // If 'true', the block cannot be jumped on if it has no uses.
        [Tooltip("If true, the block is usable even if there are no uses left.")]
        public bool usableIfNoUses = false;

        // The text for showing the amount of remaining uses.
        public TMP_Text usesText;

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

            // Sets the uses text.
            usesText.text = (uses - 1).ToString();

            // No uses left.
            if(uses == 0)
            {
                // Disable the block.
                gameObject.SetActive(false);

                // Kills any player or enemy that's on the block.

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

        // Increase the uses by 1.
        public void IncrementUsesCount(bool animate = true)
        {
            SetUsesCount(uses + 1, animate);
        }

        // Lower the uses by 1.
        public void DecrementUsesCount(bool animate = true)
        {
            SetUsesCount(uses - 1, animate);
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
        public override bool UsableBlock(FloorEntity entity)
        {
            // If there are uses left, return true.
            if(HasUsesLeft())
            {
                return true;
                
            }
            else
            {
                // Can be used no matter what.
                if (usableIfNoUses)
                {
                    return true;
                }
                else // Can no longer be used.
                {
                    return false;
                }
            }
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // Reduce the uses by 1.
            if(entity is Player)
            {
                DecrementUsesCount();
            }
            else if(entity is Enemy)
            {
                Enemy enemy = (Enemy)entity;

                if (!enemy.ignoreGeometry)
                    DecrementUsesCount();
            }

        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Turn on the object and set the uses count to max.
            gameObject.SetActive(true);
            SetUsesCount(usesMax);
        }
    }
}
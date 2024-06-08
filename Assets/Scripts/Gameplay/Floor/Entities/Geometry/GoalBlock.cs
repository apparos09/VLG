using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace VLG
{
    // The goal block.
    public class GoalBlock : Block
    {
        [Header("GoalBlock")]

        // The goal for the game.
        public Goal goal;

        // Automatically checks if the goal should be unlocked every frame.
        [Tooltip("If true, the goal's unlock condition is checked every frame.")]
        public bool autoCheckGoalCondition = true;

        // Start is called just before any of the Update methods are called for the first time.
        protected override void Start()
        {
            base.Start();

            // Play animation for the goal locking/unlocking.
            if(goal.HasConditions())
            {
                // Checks if the goal should is locked or not for playing animations.
                if (goal.IsLocked())
                    PlayGoalLockAnimation();
                else
                    PlayGoalUnlockAnimation();
            }
            else
            {
                // The goal is unlocked.
                PlayGoalUnlockAnimation();
            }
        }

        // Called when a ButtonBlock has its button clicked.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            // Toggles the goal's locked/unlocked state.
            goal.ToggleLocked();
        }

        // Called when an element interacts with the goal.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // The player interacted with the block.
            if (entity is Player)
            {
                goal.TryEnterGoal(entity as Player);
            }
        }

        // ANIMATIONS //
        // Unlock the goal animation.
       
        // Lock the goal animation.
        private void PlayGoalLockAnimation()
        {
            animator.Play("Goal Block - Lock Animation");
        }

        private void PlayGoalUnlockAnimation()
        {
            animator.Play("Goal Block - Unlock Animation");
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Lock the goal on reset if it has unlock conditions.
            if (goal.HasConditions())
                PlayGoalLockAnimation();
            else
                PlayGoalUnlockAnimation();
        }


        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // TODO: this doesn't need to be checked every frame.
            // It shouldn't be too inefficient, but maybe don't check this every frame.
           
            // Does the goal have unlock conditions?
            if(goal.HasConditions())
            {
                // If the goal unlock condition should be checked automatically.
                if (autoCheckGoalCondition)
                {
                    // Checks if the goal is locked.
                    if (goal.IsLocked())
                    {
                        // Checks if the goal condition is met.
                        // If the condition is met, unlock the goal.
                        if (goal.ConditionMet())
                        {
                            goal.UnlockGoal();
                            PlayGoalUnlockAnimation();
                        }

                    }
                }
            }
            
            
        }
    }
}
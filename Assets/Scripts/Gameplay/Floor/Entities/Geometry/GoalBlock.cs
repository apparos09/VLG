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

        // Tracks if the goal's unlock/lock condition has been met.
        private bool goalConMet = false;

        // Automatically checks if the goal should be unlocked every frame.
        [Tooltip("If true, the goal's unlock condition is checked every frame.")]
        public bool autoCheckGoalCondition = true;

        [Header("Animations")]
        // Locks the goal.
        public string goalLockAnim = "Goal Block - Lock Animation";

        // Unlocks the goal.
        public string goalUnlockAnim = "Goal Block - Unlock Animation";

        [Header("Audio")]
        // Lock SFX
        public AudioClip goalLockSfx;
        
        // Unlock Sfx
        public AudioClip goalUnlockSfx;

        // Allows post start to be called.
        private bool calledPostStart = false;

        // Start is called just before any of the Update methods are called for the first time.
        protected override void Start()
        {
            base.Start();
        }

        // Called on the first update frame.
        protected void PostStart()
        {
            // Checks if the goal's condition has been met, and if the goal is usable.
            if(goal.IsUsableAndConditionMet())
            {
                OnConditionMet(false);   
            }
            else
            {
                OnConditionFailed(false);
            }

            // Updates the objective text with the goal's objective tpye.
            gameManager.gameUI.UpdateObjectiveText();

            // Post Start Called
            calledPostStart = true;
        }


        // Called when the condition check for the goal has succeeded.
        public void OnConditionMet(bool playSound = true)
        {
            goalConMet = true;
            PlayGoalUnlockAnimation();

            // If the player is standing on the goal when it's unlocked, trigger the goal entry.
            if(gameManager.player.floorPos == floorPos)
            {
                floorManager.OnGoalTriggered();
            }

            // Plays the unlock sound effect.
            if (playSound)
                PlayGoalUnlockSfx();
        }

        // Called when the condition check for the goal has failed.
        public void OnConditionFailed(bool playSound = true)
        {
            goalConMet = false;
            PlayGoalLockAnimation();
            
            // Plays the lock sound effect.
            if (playSound)
                PlayGoalLockSfx();

        }

        // Called when a ButtonBlock has its button clicked.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            // Toggles the goal's locked/unlocked state if it's tied to a button.
            if (goal.objective == Goal.goalType.button)
                goal.ToggleUsable();
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
        public void PlayGoalLockAnimation()
        {
            animator.Play(goalLockAnim);
        }

        public void PlayGoalUnlockAnimation()
        {
            animator.Play(goalUnlockAnim);
        }

        // AUDIO
        // Plays the goal lock sfx.
        public void PlayGoalLockSfx()
        {
            if(goalLockSfx != null)
                gameManager.gameAudio.PlaySoundEffect(goalLockSfx);
        }

        // Plays the goal unlock sfx.
        public void PlayGoalUnlockSfx()
        {
            if (goalUnlockSfx != null)
                gameManager.gameAudio.PlaySoundEffect(goalUnlockSfx);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Call PostStart again so that the goal animation replays.
            calledPostStart = false;
        }


        // Update is called once per frame
        protected override void Update()
        {
            // Post Start hasn't been called yet, so call it.
            if (!calledPostStart)
                PostStart();

            // Base Update
            base.Update();

            // TODO: this doesn't need to be checked every frame.
            // It shouldn't be too inefficient, but maybe don't check this every frame.

            // If the goal condition should be auto-checked.
            if (autoCheckGoalCondition)
            {
                // If the goal is usable and the condition is met.
                bool goalCheck = goal.IsUsableAndConditionMet();

                // Checks if the goal condition has been met, and if the tracker value matches.
                if (goalCheck && !goalConMet) // Goal Available
                {
                    OnConditionMet();
                }
                else if(!goalCheck && goalConMet) // Goal Not Available
                {
                    OnConditionFailed();
                }
                
            }
            
            
        }
    }
}
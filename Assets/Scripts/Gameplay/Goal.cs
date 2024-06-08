using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VLG
{
    // The goal for the player.
    public class Goal : MonoBehaviour
    {
        // The type of the goal.
        public enum goalType { none, keys, enemy, button, boss }

        // The game manager.
        public GameplayManager gameManager;

        // The floor manager.
        public FloorManager floorManager;

        // The objective for this goal.
        public goalType objective = goalType.none; 

        // Locks/unlocks the goal.
        public bool locked = false;

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Gets the instance.
            if (floorManager == null)
                floorManager = FloorManager.Instance;

            // Gives the floor manager the goal.
            if (floorManager.goal == null)
                floorManager.goal = this;
        }

        // Checks if the portal is locked.
        public bool IsLocked()
        {
            return locked;
        }

        // Sets the locked state.
        public void SetLocked(bool value)
        {
            locked = value;
        }

        // Locks the goal.
        public void Lock()
        {
            SetLocked(true);
        }

        // Unlocks the goal.
        public void Unlock()
        {
            SetLocked(true);
        }

        // Toggles the locked setting for the goal.
        public void ToggleLocked()
        {
            SetLocked(!locked);
        }

        // Returns 'true', if the goal has unlock conditions.
        public bool HasConditions()
        {
            return objective != goalType.none;
        }

        // Called by the goal to check if its unlock condition has been met.
        public virtual bool ConditionMet()
        {
            // The value to be returned.
            bool result;

            // TODO: implement condition checking.
            switch (objective)
            {
                default: // Goal is available.
                case goalType.none:
                    result = true;

                    break;

                case goalType.keys: // Grab all the keys.
                    result = KeyItem.GetKeyItemCount() == 0;

                    break;

                case goalType.enemy: // Destory all enemies.
                    result = Enemy.GetEnemyCount() == 0;

                    break;

                case goalType.button: // Jump on a button.
                    result = locked;

                    break;

                case goalType.boss: // TODO: change win condition for the boss.
                    result = Enemy.GetEnemyCount() == 0;
                    break;
            }

            return result;
        }

        // Returns 'true' if the goal is unlocked, and if the condition is met.
        public bool IsUnlockedAndConditionMet()
        {
            return !locked && ConditionMet();
        }


        // Called when the player has interacted with the goal.
        public virtual bool TryEnterGoal(Player player)
        {
            // If the goal is locked, do nothing.
            if (locked)
                return false;

            // Enter the goal if the condition is mset.
            if(ConditionMet())
                gameManager.OnGoalEntered();

            // Return true.
            return true;
        }

        

        // Update is called once per frame
        void Update()
        {
            // ...
        }
    }
}
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
        public enum goalType { none, key, enemy, button, boss }

        // The game manager.
        public GameplayManager gameManager;

        // The floor manager.
        public FloorManager floorManager;

        // The objective for this goal.
        public goalType objective = goalType.none;

        // Shows if the goal is usable.
        [Tooltip("Shows if the goal is usable. Whether the goal is locked/unlocked hinges on the condition being met.")]
        public bool usable = false;

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

        // Checks if the goal is usable.
        public bool IsUsable()
        {
            return usable;
        }

        // Sets the usable state.
        public void SetUsable(bool value)
        {
            usable = value;
        }

        // Makes the goal usable.
        public void MakeUsable()
        {
            SetUsable(true);
        }

        // Makes the goal unusable.
        public void MakeUnusable()
        {
            SetUsable(false);
        }

        // Toggles the usable setting for the goal.
        public void ToggleUsable()
        {
            SetUsable(!usable);
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

                case goalType.key: // Grab all the key.
                    result = KeyItem.GetKeyItemCount() == 0;

                    break;

                case goalType.enemy: // Destory all enemies.
                    result = Enemy.GetEnemyCount() == 0;

                    break;

                case goalType.button: // Jump on a button.
                    result = usable;

                    break;

                case goalType.boss: // TODO: change win condition for the boss.
                    result = Enemy.GetEnemyCount() == 0;
                    break;
            }

            return result;
        }

        // Returns 'true' if the goal is usable, and if the condition is met.
        public bool IsUsableAndConditionMet()
        {
            return usable && ConditionMet();
        }


        // Called when the player has interacted with the goal.
        public virtual bool TryEnterGoal(Player player)
        {
            // If the goal is unusuable, do nothing.
            if (!usable)
                return false;

            // Enter the goal if the condition is mset.
            if(ConditionMet())
            {
                floorManager.OnGoalTriggered();
                return true;
            }
            else
            {
                return false;
            }
        }
      
    }
}
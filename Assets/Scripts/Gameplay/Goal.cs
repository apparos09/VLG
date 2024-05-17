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
        public void LockGoal()
        {
            SetLocked(true);
        }

        // Unlocks the goal.
        public void UnlockGoal()
        {
            SetLocked(true);
        }

        // Toggles the locked setting for the goal.
        public void ToggleLocked()
        {
            SetLocked(!locked);
        }


        // Called when the player has interacted with the goal.
        public virtual bool TryEnterGoal(Player player)
        {
            // If the goal is locked, do nothing.
            if (locked)
                return false;

            // TODO: add other conditions.

            // Enter the goal.
            gameManager.OnGoalEntered();

            // Return true.
            return true;
        }

        // Called by the goal to check if its unlock condition has been met.
        public virtual bool ConditionMet()
        {
            // TODO: implement condition checking.
            return true;
        }

        // Update is called once per frame
        void Update()
        {
            ConditionMet();
        }
    }
}
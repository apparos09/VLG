using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The goal for the player.
    public class Goal : MonoBehaviour
    {
        // The game manager.
        public GameplayManager gameManager;

        // Locks/unlocks the goal.
        public bool locked = false;

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
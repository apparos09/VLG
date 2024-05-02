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
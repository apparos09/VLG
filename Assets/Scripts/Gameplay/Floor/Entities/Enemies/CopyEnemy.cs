using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // An enemy that copies the player's movements, or reverses them.
    public class CopyEnemy : Enemy
    {
        [Header("CopyEnemy")]
        // The player that's being mimiced.
        public Player player;

        // List of copy enemies in the game.
        public static List<CopyEnemy> copyEnemies = new List<CopyEnemy>();

        // Reverses the copying on the x-axis
        [Tooltip("Goes in the opposite direction on the x-axis from the player")]
        public bool reverseX = false;

        // Reverses the copying on the y-axis
        [Tooltip("Goes in the opposite direction on the y-axis from the player")]
        public bool reverseY = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // The player needs to be set.
            if(player == null)
                player = GameplayManager.Instance.player;

            // Add to the list.
            copyEnemies.Add(this);
        }

        // Called when the enemy tries to copy the player
        public void OnPlayerCopy(bool success)
        {
            // ...
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        // Remove from the copy enemies list.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the list.
            if(copyEnemies.Contains(this))
                copyEnemies.Remove(this);
        }
    }
}
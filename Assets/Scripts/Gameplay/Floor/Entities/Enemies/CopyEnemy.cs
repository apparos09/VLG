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

        // TODO: implement animation.
        // [Header("CopyEnemy/Animation")]
        // 
        // // The attack animation for the enemy.
        // public string attackAnim = "";

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // The player needs to be set.
            if(player == null)
                player = GameplayManager.Instance.player;

            // Add to the list.
            if(!copyEnemies.Contains(this))
                copyEnemies.Add(this);
        }

        // Copies the provided movement.
        public bool CopyMovement(Vector2Int moveDirec)
        {
            // The direction of movement for the copy.
            Vector2Int copyMoveDirec = new Vector2Int();
            copyMoveDirec.x = reverseX ? moveDirec.x * -1 : moveDirec.x;
            copyMoveDirec.y = reverseY ? moveDirec.y * -1 : moveDirec.y;

            // The facing direction of the movement.
            if (copyMoveDirec.y > 0) // Face Up
            {
                transform.forward = Vector3.forward;
            }
            else if (copyMoveDirec.y < 0) // Face Down
            {
                transform.forward = Vector3.back;
            }
            else if (copyMoveDirec.x < 0) // Face Left
            {
                transform.forward = Vector3.left;
            }
            else if (copyMoveDirec.x > 0) // Face Right
            {
                transform.forward = Vector3.right;
            }

            // Try to move the copy.
            bool copySuccess = floorManager.TryEntityMovement(this, copyMoveDirec);

            // Called when the movement has been copied.
            OnCopyMovement(copySuccess);

            return copySuccess;
        }


        // Called when the enemy tries to copy the player
        public virtual void OnCopyMovement(bool success)
        {
            // ...
        }

        // Resets the asset.
        public override void ResetEntity()
        {
            // Resets the rotation.
            transform.rotation = Quaternion.identity;

            base.ResetEntity();
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
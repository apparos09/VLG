using JetBrains.Annotations;
using UnityEngine;


namespace VLG
{
    // The player for the game.
    public class Player : FloorAsset
    {
        // The gameplay manager.
        public GameplayManager gameManager;

        // Enables input from the player.
        public bool allowInput = true;

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();

            // Gets the instance if this is null.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Sets to this player.
            if (gameManager.player == null)
                gameManager.player = this;

        }

        // Updates the player movements.
        public void UpdateInput()
        {
            // The move direction.
            Vector2Int moveDirec = Vector2Int.zero;

            // Player Movement - Ver. 1 - Constant Movement
            //if (Input.GetAxisRaw("Vertical") != 0) // Up/Down
            //{
            //    // Checks direction.
            //    if(Input.GetAxisRaw("Vertical") == 1) // Up
            //    {
            //        moveDirec = Vector2Int.up;
            //    }
            //    else // Down
            //    {
            //        moveDirec = Vector2Int.down;
            //    }

            //}
            //else if (Input.GetAxisRaw("Horizontal") != 0) // Left/Right
            //{
            //    // Checks direction.
            //    if (Input.GetAxisRaw("Horizontal") == 1) // Right
            //    {
            //        moveDirec = Vector2Int.right;
            //    }
            //    else // Left
            //    {
            //        moveDirec = Vector2Int.left;
            //    }
            //}
            //else // No Movement
            //{
            //     moveDirec = Vector2Int.zero;
            //}

            // Player Movement - Ver. 2 - Button-Based (One Space per Input)
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) // Up
            {
                moveDirec = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) // Down
            {
                moveDirec = Vector2Int.down;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) // Left
            {
                moveDirec = Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) // Right
            {
                moveDirec = Vector2Int.right;
            }


            // There is movement.
            if (moveDirec != Vector2.zero)
            {
                gameManager.TryPlayerMovement(moveDirec);
            }



            // Reset Floor
            if(Input.GetKeyDown(KeyCode.R))
            {
                gameManager.ResetFloor();
            }
        }

        // Resets the asset.
        public override void ResetAsset()
        {
            base.ResetAsset();
        }

        // Update is called once per frame
        void Update()
        {
            // Updates the inputs from the player.
            if (allowInput)
                UpdateInput();
        }

    }
}
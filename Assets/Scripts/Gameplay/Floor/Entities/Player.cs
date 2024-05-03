using JetBrains.Annotations;
using UnityEngine;


namespace VLG
{
    // The player for the game.
    public class Player : FloorEntity
    {
        // The gameplay manager.
        public GameplayManager gameManager;

        [Header("Player/Input")]
        // Enables input from the player.
        public bool allowInput = true;

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();

            // Sets the group.
            group = assetGroup.player;

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
            // If the player isn't in the process of moving, they can select their next move.
            if(!Moving)
            {
                // The move direction.
                Vector2Int moveDirec = Vector2Int.zero;

                // Moves the player in a given direction, and has the player face said direction.
                // Player Movement - Ver. 1 - Constant Movement
                //if (Input.GetAxisRaw("Vertical") != 0) // Up/Down
                //{
                //    // Checks direction.
                //    if(Input.GetAxisRaw("Vertical") == 1) // Up
                //    {
                //        moveDirec = Vector2Int.up;
                //        transform.forward = Vector3.forward;
                //    }
                //    else // Down
                //    {
                //        moveDirec = Vector2Int.down;
                //        transform.forward = Vector3.back;
                //    }

                //}
                //else if (Input.GetAxisRaw("Horizontal") != 0) // Left/Right
                //{
                //    // Checks direction.
                //    if (Input.GetAxisRaw("Horizontal") == 1) // Right
                //    {
                //        moveDirec = Vector2Int.right;
                //        transform.forward = Vector3.right;
                //    }
                //    else // Left
                //    {
                //        moveDirec = Vector2Int.left;
                //        transform.forward = Vector3.left;
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
                    transform.forward = Vector3.forward;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) // Down
                {
                    moveDirec = Vector2Int.down;
                    transform.forward = Vector3.back;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) // Left
                {
                    moveDirec = Vector2Int.left;
                    transform.forward = Vector3.left;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) // Right
                {
                    moveDirec = Vector2Int.right;
                    transform.forward = Vector3.right;
                }


                // There is movement.
                if (moveDirec != Vector2.zero)
                {
                    // Attempt movement.
                    gameManager.floorManager.TryPlayerMovement(this, moveDirec);
                }
            }
            

            // Reset Floor
            if(Input.GetKeyDown(KeyCode.R))
            {
                gameManager.floorManager.ResetFloor();
            }
        }

        // Called when a movement has been started.
        public override void OnMoveStarted(Vector3 start, Vector3 end, float t)
        {
            base.OnMoveStarted(start, end, t);
        }

        // Called when a movement is ending.
        public override void OnMoveEnded(Vector3 start, Vector3 end, float t)
        {
            base.OnMoveEnded(start, end, t);
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // ...
        }

        // Resets the asset.
        public override void ResetEntity()
        {
            base.ResetEntity();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Updates the inputs from the player.
            if (allowInput)
                UpdateInput();
        }

    }
}
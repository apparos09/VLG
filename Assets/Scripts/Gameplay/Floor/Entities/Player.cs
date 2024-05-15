using JetBrains.Annotations;
using UnityEngine;


namespace VLG
{
    // The player for the game.
    public class Player : FloorEntity
    {
        [Header("Player")]
        // Enables input from the player.
        public bool allowInput = true;

        // Gets se to 'true' when attacking, false when not attacking.
        private bool attacking = false;

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();

            // Sets the group.
            group = entityGroup.player;

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

                // TODO: If using move interpolation, use the axis raw version?
                // Maybe don't if the movement speed is too fast. Maybe revisit this idea.

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
                    bool success = floorManager.TryEntityMovement(this, moveDirec);

                    // Call on player movement input.
                    floorManager.OnPlayerMovementInput(this, moveDirec, success);
                }


                // Attack
                if(!attacking)
                {
                    // If the player should attack.
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Attack();
                    }
                }
            }
            

            // Reset Floor
            if(Input.GetKeyDown(KeyCode.R))
            {
                gameManager.floorManager.ResetFloor();
            }
        }

        // Called when a movement has been started.
        public override void OnMoveStarted(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveStarted(localStart, localEnd, t);
        }

        // Called when a movement is ending.
        public override void OnMoveEnded(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveEnded(localStart, localEnd, t);
        }

        // Call function to attack enemy.
        public void Attack()
        {
            // TODO: trigger animation and make function calls.
            // The attack direction.
            Vector2Int attackDirec = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z));
            
            // The attack position on the floor.
            Vector2Int attackFloorPos = floorPos + attackDirec;

            // Validity check.
            if(floorManager.IsFloorPositionValid(attackFloorPos))
            {
                // Gets the enemy.
                Enemy enemy = floorManager.floorEnemies[attackFloorPos.x, attackFloorPos.y];

                // There's an enemy at this index.
                if (enemy != null)
                {
                    // The enemy has been hit by the player.
                    enemy.OnPlayerAttackHit(this);
                }
            }
        }

        // Called when the player's attack is started.
        public void OnAttackStarted()
        {
            attacking = true;
        }

        // Called when the player's attack is ended.
        public void OnAttackEnded()
        {
            attacking = false;
        }


        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // ...
        }

        // Kills the player.
        public override void KillEntity()
        {
            // TODO: should the whole floor be reset?
            ResetEntity();
            // floorManager.ResetFloor();
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

            // The game is paused, so don't update anything.
            if (gameManager.paused)
                return;

            // Updates the inputs from the player.
            if (allowInput)
                UpdateInput();
        }

    }
}
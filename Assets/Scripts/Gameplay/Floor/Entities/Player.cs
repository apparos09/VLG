using JetBrains.Annotations;
using UnityEngine;


namespace VLG
{
    // The player for the game.
    public class Player : FloorEntity
    {
        [Header("Player")]

        // The collider for the player.
        public new BoxCollider collider;

        // The rigidbody for the player.
        public new Rigidbody rigidbody;

        // Enables input from the player.
        public bool allowInput = true;

        // Gets se to 'true' when attacking, false when not attacking.
        private bool attacking = false;

        // If 'true', the player uses the attack animation.
        private bool useAttackAnim = true;

        [Header("Player/Animation")]
        // The animation for the model specifically.
        public Animator modelAnimator;

        // The player's animations.
        public enum plyrAnims { none, reset, idle, jumpRise, jumpFall, jumpLand, attack }

        // The empty animation.
        public string emptyAnim = "Empty State";

        // The attack animation for main animator.
        [Tooltip("The attack animation for the main animator (the one saved to the parent object).")]
        public string attackMainAnim = "";

        [Header("Player/Animation/Model")]

        // The reset animation for the player.
        public string resetAnim = "";

        // The idle animation for the player.
        public string idleAnim = "";

        // The animation for the rise of the jump.
        public string jumpRiseAnim = "";

        // The animation for the fall of the jump.
        public string jumpFallAnim = "";

        // The animation for the landing of the jump.
        public string jumpLandingAnim = "";
        
        // The stage of the jump animation.
        // 0 = none, 1 = rise, 2 = fall, 3 = landing
        private int jumpAnimStage = 0;

        // The attack animation for the player.
        [Tooltip("The attack animation for the model animator (the one imported into Unity).")]
        public string attackModelAnim = "";

        [Header("Player/Animation/Sword")]
        // The animation for turning the sword object on.
        public string swordEnableAnim = "";

        // The animation for turning the sword off.
        public string swordDisableAnim = "";

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();

            // Sets the group.
            group = entityGroup.player;

            // Sets to this player.
            if (gameManager.player == null)
                gameManager.player = this;


            // If the collider is not set.
            if(collider == null)
            {
                collider = GetComponent<BoxCollider>();
            }

            // If the rigidbody is not set.
            if(rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            // Plays the idle animation.
            PlayIdleAnimation();
        }

        // Updates the player movements.
        public void UpdateInput()
        {
            // If the player isn't in the process of moving, they can select their next move.
            if(!Moving)
            {
                // The move direction.
                Vector2Int moveDirec = Vector2Int.zero;

                // Changes the direction the player is facing.
                if (Input.GetKeyDown(KeyCode.UpArrow)) // Face Up
                {
                    transform.forward = Vector3.forward;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow)) // Face Down
                {
                    transform.forward = Vector3.back;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow)) // Face Left
                {
                    transform.forward = Vector3.left;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow)) // Face Right
                {
                    transform.forward = Vector3.right;
                }

                // Moves the player in a given direction, and has the player face said direction.
                if (Input.GetKeyDown(KeyCode.W)) // Move Up
                {
                    moveDirec = Vector2Int.up;
                    transform.forward = Vector3.forward;
                }
                else if (Input.GetKeyDown(KeyCode.S)) // Move Down
                {
                    moveDirec = Vector2Int.down;
                    transform.forward = Vector3.back;
                }

                if (Input.GetKeyDown(KeyCode.A)) // Move Left
                {
                    moveDirec = Vector2Int.left;
                    transform.forward = Vector3.left;
                }
                else if (Input.GetKeyDown(KeyCode.D)) // Move Right
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
                        // If the attack animation should be used, call OnAttackStarted.
                        // If the attack animation won't be used, just call the attack function.
                        if (useAttackAnim)
                        {
                            // Plays the main attack animation.
                            animator.Play(attackMainAnim);
                        }
                        else
                        {
                            Attack();
                        }
                            
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
            PlayAnimation(plyrAnims.jumpRise);
            jumpAnimStage = 1;
        }

        // Called when a movement is ongoing.
        public override void OnMoveOngoing(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveOngoing(localStart, localEnd, t);

            // If at the peak of the movement, play the fall animation.
            if(t >= 0.5F && jumpAnimStage == 1)
            {
                PlayAnimation(plyrAnims.jumpFall);
                jumpAnimStage = 2;
            }
            
        }

        // Called when a movement is ending.
        public override void OnMoveEnded(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveEnded(localStart, localEnd, t);
            PlayIdleAnimation();
            jumpAnimStage = 0; // Reset to no stage.
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

            // The player used an attack.
            floorManager.OnPlayerAttackInput(this, attackDirec, attackFloorPos);
        }

        // Called when the player's attack is started.
        public void OnAttackStarted()
        {
            PlayAnimation(plyrAnims.attack);
            attacking = true;
        }

        // Called when the player's attack is ended.
        public void OnAttackEnded()
        {
            PlayIdleAnimation();
            attacking = false;
        }

        // Gives the player the provided item.
        public void GiveItem(Item item)
        {
            item.OnItemGiven(this);
        }


        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // ...
        }
        
        // Kills the player.
        public override void KillEntity()
        {
            // If the player dies, the floor has been failed.
            floorManager.OnFloorFailed();

            // Resets the player.
            // ResetEntity();
        }

        // Resets the asset.
        public override void ResetEntity()
        {
            // Resets the rotation.
            transform.rotation = Quaternion.identity;

            base.ResetEntity();
        }

        // ANIMATION
        // The animation to be played.
        public void PlayAnimation(plyrAnims anim)
        {
            // Resets the attacking variable in case the attack animation isn't finished.
            attacking = false;

            // Resets the model animation to the reset position.
            modelAnimator.Play(resetAnim);

            switch (anim)
            {
                case 0:
                default: // Empty/None
                    modelAnimator.Play(emptyAnim);
                    break;

                case plyrAnims.reset: // Reset
                    modelAnimator.Play(resetAnim);
                    break;

                case plyrAnims.idle: // Idle
                    modelAnimator.Play(idleAnim);
                    animator.Play(swordDisableAnim);
                    break;

                case plyrAnims.jumpRise: // Jump Rise
                    modelAnimator.Play(jumpRiseAnim);
                    animator.Play(swordDisableAnim);
                    break;

                case plyrAnims.jumpFall: // Jump Fall
                    modelAnimator.Play(jumpFallAnim);
                    animator.Play(swordDisableAnim);
                    break;

                case plyrAnims.jumpLand: // Jump Landing
                    modelAnimator.Play(jumpLandingAnim);
                    animator.Play(swordDisableAnim);
                    break;

                case plyrAnims.attack: // Attack
                    modelAnimator.Play(attackModelAnim); // Model Animation
                    animator.Play(attackMainAnim); // Main Animation
                    break;

            }
        }

        // Plays the idle animation.
        public void PlayIdleAnimation()
        {
            PlayAnimation(plyrAnims.idle);
        }

        // Plays the attack animation.
        public void PlayAttackAnimation()
        {
            PlayAnimation(plyrAnims.attack);
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static VLG.Player;

namespace VLG
{
    // An enemy that copies the player's movements, or reverses them.
    public class CopyEnemy : Enemy
    {
        [Header("CopyEnemy")]

        // The copy enemy's weapon model.
        public GameObject weaponModel;

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

        [Header("CopyEnemy/Animation")]

        // The animation names.
        public List<string> modelAnimNames = new List<string>();

        // The animation clips.
        public List<AnimationClip> modelAnimClips = new List<AnimationClip>();

        // The stage of the jump animation (same as the player).
        // 0 = none, 1 = rise, 2 = fall, 3 = landing
        private int jumpAnimStage = 0;

        // If set to true, all animations are copied.
        // Since not all animations are copied, the sword is disabled since animations related to it are not used.
        private bool copyAllAnims = false;

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

            // FIXME: for some reason, this doesn't work. So this is turned off by default.
            // Disabled the weapon model by default since the idle animation is playing.
            // weaponModel.SetActive(false);
        }

        // Called when a movement has been started.
        public override void OnMoveStarted(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveStarted(localStart, localEnd, t);
            modelAnimator.Play(player.jumpRiseAnim);
            jumpAnimStage = 1;
        }

        // Called when a movement is ongoing.
        public override void OnMoveOngoing(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveOngoing(localStart, localEnd, t);

            // If at the peak of the movement, play the fall animation.
            if (t >= 0.5F && jumpAnimStage == 1)
            {
                modelAnimator.Play(player.jumpFallAnim);
                jumpAnimStage = 2;
            }

        }

        // Called when a movement is ending.
        public override void OnMoveEnded(Vector3 localStart, Vector3 localEnd, float t)
        {
            base.OnMoveEnded(localStart, localEnd, t);
            modelAnimator.Play(idleAnim);
            jumpAnimStage = 0; // Reset to no stage.
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

        // Called to copy the player's current animation.
        protected void CopyPlayerAnimation()
        {
            // The model animator is set.
            if(modelAnimator != null)
            {
                // If there are animator clips to use.
                if(player.modelAnimator.GetCurrentAnimatorClipInfo(0).Length != 0 &&
                    modelAnimator.GetCurrentAnimatorClipInfo(0).Length != 0)
                {
                    // Gets the current animation clip for the player and the copy enemy's model.
                    AnimationClip currPlayerClip = player.modelAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
                    AnimationClip currCopyClip = modelAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;

                    // If the copy entity is not on the current animation.
                    if (currCopyClip != currPlayerClip)
                    {
                        // Gets the index of the player clip in the model animation clip list.
                        int index = modelAnimClips.IndexOf(currPlayerClip);

                        // Proper index found.
                        if (index != -1 && index >= 0 && index < modelAnimNames.Count)
                        {
                            // Gets the animation name.
                            string animName = modelAnimNames[index];

                            // FIXME: this didn't work for some reason.
                            // Active the weapon model if the attack model has been selected.
                            // weaponModel.SetActive(animName == attackAnim ? true : false);

                            // Checks if all animations should be copied.
                            if (copyAllAnims)
                            {
                                // Play the animation.
                                modelAnimator.Play(animName);
                            }
                            else // Don't copy all animations.
                            {
                                // The attack animation shouldn't be copied.
                                // If the player is in the middle of jumping, but the copy enemy isn't, don't copy the aniamtion.
                                // If the copy is jumping and the player isn't, don't copy the animation.
                                if ((!Moving && player.PlayingJumpAnimation()) || (Moving && !player.Moving))
                                {
                                    // ...

                                    // FIXME: this didn't work for some reason, so right now nothing is done here.
                                    // Disables the weapon model.
                                    //if(weaponModel.activeSelf)
                                    //    weaponModel.SetActive(false);
                                }
                                else if (animName == attackAnim) // If the copy enemy is attacking.
                                {
                                    // NOTE: the copy enemy cannot attack, so this has not been included.
                                    // If this is included, the sword won't appear since the model is disabled.
                                    // Using the SetActive function doesn't appear to fix this for some reason.
                                    // If this is implemented, then this would need to be fixed.

                                    // weaponModel.SetActive(true);
                                    // modelAnimator.Play(animName);
                                }
                                else
                                {
                                    // Play the animation.
                                    modelAnimator.Play(animName);
                                }

                            }

                        }
                    }
                }
                
            }
        }

        // Resets the asset.
        public override void ResetEntity()
        {
            // Resets the rotation.
            transform.rotation = Quaternion.identity;

            // Reset the jump anim stage.
            jumpAnimStage = 0;

            base.ResetEntity();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Tries to copy the player's animation.
            if (!gameManager.IsPaused() && Time.timeScale > 0.0f)
            {
                CopyPlayerAnimation();
            }
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
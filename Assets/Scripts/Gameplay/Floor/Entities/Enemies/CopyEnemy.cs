using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            // Disalbe the weapon model by default since the idle animation is playing.
            // weaponModel.SetActive(false);
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
                                // If the player is in the middle of jumping, but the copy enemy isn't...
                                // Don't use the jump animation
                                if (animName == attackAnim || (!Moving && player.PlayingJumpAnimation()))
                                {
                                    // ...

                                    // FIXME: this didn't work for some reason, so right now nothing is done here.
                                    // Disables the weapon model.
                                    //if(weaponModel.activeSelf)
                                    //    weaponModel.SetActive(false);
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

            base.ResetEntity();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Tries to copy the player's animation.
            if (!gameManager.paused && Time.timeScale > 0.0f)
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
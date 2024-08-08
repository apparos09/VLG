using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The item class.
    public class Item : FloorEntity
    {
        // The enum for the itme.
        public enum itemType { none, key, weapon }

        [Header("Item")]
        // The item type for the object.
        public itemType item = itemType.none;

        // The idle animation for the item.
        public string idleAnim = "";

        // The sound effect for getting the item.
        public AudioClip itemGetSfx;

        // Gets set to 'true' when post start has been called.
        private bool calledPostStart = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the group.
            group = entityGroup.item;

            // Play idle animation.
            animator.Play(idleAnim);
        }

        // Post Start function.
        protected virtual void PostStart()
        {
            calledPostStart = true;
        }

        // Call this function when the item is given to a player.
        public virtual void OnItemGiven(Player player)
        {
            // Plays the item get sound.
            PlayItemGetSfx();

            // Destroy the item object.
            KillEntity();
        }

        // Called when the item is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // The entity is a player.
            if(entity is Player)
            {
                // Get the player and give them this item.
                Player player = (Player)entity;
                player.GiveItem(this);
            }
        }

        // Kills the player.
        public override void KillEntity()
        {
            Destroy(gameObject);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Set this to false so that the function gets called again.
            calledPostStart = false;
        }

        // Plays the item get SFX.
        public void PlayItemGetSfx()
        {
            // The audio source and the sound effect are set.
            if(itemGetSfx != null)
            {
                gameManager.gameAudio.PlaySoundEffect(itemGetSfx);
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            // If post start hasn't been called, call post start.
            if (!calledPostStart)
                PostStart();

            // Base Update
            base.Update();
        }

        // This function is called when the MonoBehaviour will be destroyed.
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
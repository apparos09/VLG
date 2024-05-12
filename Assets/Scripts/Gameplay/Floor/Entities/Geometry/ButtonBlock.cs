using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The button block.
    public class ButtonBlock : Block
    {
        [Header("Button Block")]

        // The tags for triggering a button.
        [Tooltip("Valid interactions for the button. If none are listed, all entities can trigger the button.")]
        public List<string> validTags = new List<string>();

        // Determines if the button is locked or unlocked.
        private bool locked = false;

        // CALLBACKS
        // A callback for when all the text has been gone through.
        public delegate void ButtonBlockCallback(FloorEntity entity);

        // Callback for the textbox being opened.
        private ButtonBlockCallback buttonClickCallback;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Is the button locked?
        public bool IsLocked()
        {
            return locked;
        }

        // Sets if the button is locked on unlocked.
        public void SetLocked(bool value)
        {
            locked = value;

            // TODO: apply animation
            if(locked)
            {
                // ...
            }
            else
            {
                // ...
            }
        }

        // Locks the button.
        public void LockButton()
        {
            SetLocked(true);
        }

        // Unlocks the button.
        public void UnlockButton()
        {
            SetLocked(false);
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // Don't do anything if the portal is locked.
            if (locked)
                return;

            // Checks for valid tags.
            if (validTags.Count == 0)
            {
                OnButtonClicked(entity);
            }
            else
            {
                // Checks for the valid tag.
                if (validTags.Contains(entity.tag))
                    OnButtonClicked(entity);
            }
        }

        // Adds the button click callback.
        public void OnButtonClickedAddCallback(ButtonBlockCallback callback)
        {
            buttonClickCallback += callback;
        }

        // Removes the button click callback.
        public void OnButtonClickedRemoveCallback(ButtonBlockCallback callback)
        {
            buttonClickCallback -= callback;
        }

        // Called when the button block has been interacted with.
        private void OnButtonClicked(FloorEntity entity)
        {
            // Checks if there are functions to call.
            if (buttonClickCallback != null)
                buttonClickCallback(entity);

            
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}
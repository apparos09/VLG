using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The button block.
    public class ButtonBlock : Block
    {
        [Header("ButtonBlock")]

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

        // Gets set to 'true' when the post start function has been called.
        private bool calledPostStart = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Called on the first update frame.
        protected virtual void PostStart()
        {
            // This function has been called.
            calledPostStart = true;
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

        // Adds all floor entities of a given type to the button.
        // TODO: using the FindObjectsOfType is a bit intensive, but for a small game like this...
        // It should be okay. Either way, you may want to find a more efficient way to do this.
        protected List<T> AddAllFloorEntitiesOfType<T>(bool includeInactive) where T : FloorEntity
        {
            // Gets the list of all floor entities of a given type.
            List<T> list = new List<T>(FindObjectsOfType<T>(includeInactive));

            // Goes through all elements in the list.
            for(int i = 0; i < list.Count; i++)
            {
                list[i].AddToButtonBlock(this);
            }

            // Returns the list.
            return list;
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
                ClickButtonBlock(entity);
            }
            else
            {
                // Checks for the valid tag.
                if (validTags.Contains(entity.tag))
                    ClickButtonBlock(entity);
            }
        }

        // Adds the button click callback.
        public void AddClickButtonBlockCallback(ButtonBlockCallback callback)
        {
            buttonClickCallback += callback;
        }

        // Removes the button click callback.
        public void RemoveClickButtonBlockCallback(ButtonBlockCallback callback)
        {
            buttonClickCallback -= callback;
        }

        // Called when the button block has been interacted with. The provided entity is the one that clicked it.
        private void ClickButtonBlock(FloorEntity entity)
        {
            // Checks if there are functions to call.
            if (buttonClickCallback != null)
                buttonClickCallback(entity);
            
        }

        // Update is called once per frame
        protected override void Update()
        {
            // Call the post start function.
            if (!calledPostStart)
                PostStart();

            base.Update();
        }
    }
}
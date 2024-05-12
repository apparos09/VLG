using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The portal block.
    public class PortalBlock : Block
    {
        [Header("Portal Block")]

        // The end portal that this portal is connected to.
        public PortalBlock endPortal;

        // The tags for triggering a portal.
        [Tooltip("Valid interactions for the portal. If none are listed, all entities can use the portal")]
        public List<string> validTags = new List<string>();

        // Determines if the button is locked or unlocked.
        private bool locked = false;

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
            if (locked)
            {
                // ...
            }
            else
            {
                // ...
            }
        }

        // Locks the portal.
        public void LockPortal()
        {
            SetLocked(true);
        }

        // Unlocks the portal.
        public void UnlockPortal()
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

            // The end portal is not connected.
            if(endPortal == null)
            {
                Debug.LogError("No end portal is set, so warp can't happen.");
                return;
            }

            // Checks for valid tags.
            bool warp = validTags.Count == 0 || validTags.Contains(entity.tag);

            // If 'true', warp the entity.
            if (warp)
            {
                // TODO: apply animation.
                entity.SetFloorPosition(endPortal.floorPos, false, false);
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A block with a hazard on it.
    public class HazardBlock : Block
    {
        [Header("HazardBlock")]
        // The state of the hazard upon the game starting (on/off).
        [Tooltip("Determines the starting state of the hazard (on/off).")]
        public bool hazardOnDefault = true;

        // Determines if the hazard is on or off.
        protected bool hazardOn = true;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Sets the default hazard value.
            SetHazardOn(hazardOnDefault, false);
        }

        // Returns a bool to indicate if the hazard is on.
        public bool IsHazardOn()
        {
            return hazardOn;
        }

        // Sets whether the hazard is enabled or disabled.
        public virtual void SetHazardOn(bool value, bool animate = true)
        {
            hazardOn = value;
        }

        // Enables the hazard.
        public void EnableHazard(bool animate = true)
        {
            SetHazardOn(true);
        }

        // Disables the hazard.
        public void DisableHazard(bool animate = true)
        {
            SetHazardOn(false);
        }

        // Toggle the hazard on/off.
        public void ToggleHazard(bool animate = true)
        {
            SetHazardOn(!hazardOn);
        }

        // Called by a ButtonBlock event.
        public override void OnButtonBlockClicked(FloorEntity entity)
        {
            base.OnButtonBlockClicked(entity);

            // Toggles the hazard on/off.
            ToggleHazard();
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // If the hazard is on.
            if (hazardOn)
            {
                entity.KillEntity();
            }
            
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetHazardOn(hazardOnDefault, false); // Set value to default.
        }
    }

}
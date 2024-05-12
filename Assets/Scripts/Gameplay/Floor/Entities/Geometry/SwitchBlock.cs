using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // NOTE: you may not use this due to its mechanics being iffy and likely unnecessary.
    // For now, this is unused, and has not been tested.

    // The switch block - turns on/off when the player jumps.
    public class SwitchBlock : Block
    {
        [Header("SwitchBlock")]

        // Determines default state of the block (on/off)
        [Tooltip("If 'true', the block is on by default. If false, the block is off by default.")]
        public bool blockOnDefault = false;

        // The block is on/off
        protected bool blockOn = false;

        // If 'true', the block is usable when it's off. 
        [Tooltip("If 'true', the block is usable no matter the state.")]
        public bool usableWhenOff = false;

        // If 'true', the block kills the entity on it if it's off.
        [Tooltip("Kills the entity on the block when it's off if this is true.")]
        public bool killWhenOff = true;
        
        // The list of active switch blocks.
        public static List<SwitchBlock> switchBlocks = new List<SwitchBlock>();

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Default setting.
            blockOn = blockOnDefault;

            // Add to the list.
            switchBlocks.Add(this);
        }

        // Returns a bool to indicate if the hazard is on.
        public bool IsBlockOn()
        {
            return blockOn;
        }

        // Sets whether the hazard is enabled or disabled.
        public virtual void SetBlockOn(bool value)
        {
            blockOn = value;

            // TODO: trigger animation

            // Checks for entities on the platform.
            if(!blockOn)
            {
                // Player
                if (gameManager.player.floorPos == floorPos)
                {
                    OnEntityInteract(gameManager.player);
                }

                // Enemy
                if (floorManager.floorEnemies[floorPos.x, floorPos.y] != null)
                {
                    OnEntityInteract(floorManager.floorEnemies[floorPos.x, floorPos.y]);
                }
            }
            
        }

        // Turns the block on.
        public void TurnBlockOn()
        {
            SetBlockOn(true);
        }

        // Turns the block off.
        public void TurnBlockOff()
        {
            SetBlockOn(false);
        }

        // Toggles the block.
        public void ToggleBlock()
        {
            SetBlockOn(!blockOn);
        }

        // Called to see if this block is valid to use.
        public override bool UsableBlock(FloorEntity entity)
        {
            // If the block is usable when off, always return true.
            // If the entity is the player, they can try to jump on it regardless of the state.
            if (usableWhenOff || entity is Player)
            {
                return true;

            }
            else
            {
                return blockOn;

            }
        }

        // Called when an element interact with this block, which is usually the player.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);

            // If the block is off.
            if (killWhenOff && !blockOn)
            {
                // Kills the entity on the block if the block if off.
                entity.KillEntity();
            }

        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();
            SetBlockOn(blockOnDefault); // Set value to default.
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        // Remove from the switch block list.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the block list.
            switchBlocks.Remove(this);
        }
    }
}
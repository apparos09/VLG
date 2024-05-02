using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A floor asset.
    public abstract class FloorAsset : MonoBehaviour
    {
        // The asset group.
        public enum assetGroup { none, player, geometry, item }

        // The floor manager.
        public FloorManager floorManager;

        // The group the asset is part of.
        protected assetGroup group = assetGroup.none;

        // The ID of the floor asset.
        public int id = -1;

        // The world Y position of the floor asset.
        [Tooltip("The asset's position on the y-axis (up/down) in world space.")]
        public float localYPos = 0;

        // The floor position of the player (negative means the player isn't on the floor).
        public Vector2Int floorPos = new Vector2Int(-1, -1);

        // The reset position.
        public Vector2Int resetPos = new Vector2Int(-1, -1); 

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Gets the instance if this is null.
            if (floorManager == null)
                floorManager = FloorManager.Instance;
        }
        
        // Gets the group the asset is part of.
        public assetGroup GetGroup()
        {
            return group;
        }

        // Gets the group ID
        public string GetGroupId()
        {
            string str = "";

            // Checks the group.
            switch(group)
            {
                default:
                case assetGroup.none: 
                    str = "N"; 
                    break;

                case assetGroup.player: 
                    str = "P"; 
                    break;

                case assetGroup.geometry: 
                    str = "G"; 
                    break;

                case assetGroup.item: 
                    str = "I"; 
                    break;
            }

            // Adds the ID
            str += id.ToString("D2");

            return str;
        }

        // Set the floor position of the asset.
        public void SetFloorPosition(Vector2Int newPos, bool setResetPos)
        {
            // If the position is valid.
            if(newPos.x >= 0 && newPos.x < floorManager.currFloor.geometry.GetLength(1) &&
                newPos.y >= 0 && newPos.y < floorManager.currFloor.geometry.GetLength(0))
            {
                // Set position.
                floorPos = newPos;

                // Set reset position.
                if (setResetPos)
                    resetPos = newPos;

                // Sets the position in world space (the y-position in the grid is used as the z-position).
                Vector2 localXZPos = floorPos * floorManager.floorSpacing;

                // The local position offset.
                Vector2 localPosOffset;

                // Checks if the origin should be the centre of the world.
                // If true, the object is offset by the world size. If false, no offset is applied.
                // If no offset is applied, the world origin is in the top left.
                if (floorManager.originIsCenter) // Centre
                {
                    localPosOffset = new Vector2(
                        FloorData.FLOOR_COLS * floorManager.floorSpacing.x / 2.0F,
                        FloorData.FLOOR_ROWS * floorManager.floorSpacing.y / 2.0F);
                }
                else // Top Left
                {
                    localPosOffset = Vector2.zero;
                }

                // Applies the offset.
                localXZPos -= localPosOffset;

                // Sets the local position.
                transform.localPosition = new Vector3(localXZPos.x, localYPos, localXZPos.y);
            }
            
        }

        // Resets the floor asset.
        public virtual void ResetAsset()
        {
            SetFloorPosition(resetPos, false);
        }
    }
}
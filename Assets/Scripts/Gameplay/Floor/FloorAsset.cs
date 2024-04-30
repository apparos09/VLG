using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A floor asset.
    public abstract class FloorAsset : MonoBehaviour
    {
        // The floor manager.
        public FloorManager floorManager;

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

        // Set the floor position of the asset.
        public void SetFloorPosition(Vector2Int newPos, bool setResetPos)
        {
            // If the position is valid.
            if(newPos.x >= 0 && newPos.x < floorManager.currFloor.geometry.GetLength(0) &&
                newPos.y >= 0 && newPos.y < floorManager.currFloor.geometry.Length)
            {
                // Set position.
                floorPos = newPos;

                // Set reset position.
                if (setResetPos)
                    resetPos = newPos;

                // Sets the position in world space.
                transform.localPosition = floorPos * floorManager.floorSpacing;
            }
            
        }

        // Resets the floor asset.
        public abstract void ResetAsset();
    }
}
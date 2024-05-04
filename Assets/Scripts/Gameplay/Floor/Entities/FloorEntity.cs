using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VLG
{
    // A floor entity.
    public abstract class FloorEntity : MonoBehaviour
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

        [Header("Move Interpolation")]

        // If 'true', the entity interpolates movement across the floor.
        public bool useMoveInter = false;

        // Gets set to 'true' when the entity is moving.
        private bool moving = false;

        // If true', movement is curved. If false, it's a straigh line (regular lerp).
        [Tooltip("If true, movement is curved. If false, it's a straight line.")]
        public bool curvedMovement = true;

        // The start position.
        private Vector3 startWorldPos;

        // The end position.
        private Vector3 endWorldPos;

        // The interpolation time
        private float interPercent = 0.0F;

        // The movement speed.
        public float moveSpeed = 1.0F;

        // The jump height for the interpolation (only applies on curves).
        [Tooltip("The jump height during curved movement.")]
        public float jumpHeight = 10.0F;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Gets the instance if this is null.
            if (floorManager == null)
                floorManager = FloorManager.Instance;
        }

        // Checks if the entity is moving (in move animation)
        public bool Moving
        {
            get { return moving; }
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
        public void SetFloorPosition(Vector2Int newFloorPos, bool setResetPos, bool callInteract)
        {
            // If the position is valid.
            if(newFloorPos.x >= 0 && newFloorPos.x < floorManager.currFloor.geometry.GetLength(1) &&
                newFloorPos.y >= 0 && newFloorPos.y < floorManager.currFloor.geometry.GetLength(0))
            {
                // Set position.
                floorPos = newFloorPos;

                // Set reset position.
                if (setResetPos)
                    resetPos = newFloorPos;

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


                // Reset movement interpolation values.
                moving = false;
                interPercent = 0.0F;

                // Call interaction function.
                if(callInteract)
                    floorManager.OnFloorEntityPositionChanged(this);
            }
            
        }

        // Moves the asset to the provided floor position.
        public virtual void MoveEntity(Vector2Int newFloorPos, bool instant, bool setResetPos)
        {
            // Checks to see if the position is invalid.
            if (!floorManager.IsFloorPositionValid(newFloorPos))
            {
                // The position is invalid, so do nothing.
                return;
            }

            // Checks if the movement should be instant.
            if (instant)
            {
                // Set the floor position.
                SetFloorPosition(newFloorPos, setResetPos, setResetPos);

                // Set move to false.
                moving = false;

                // Set percent to zero.
                interPercent = 0.0F;
            }   
            else
            {
                // TODO: make this more efficient.
                // Sets the reset position to be the end position.
                if(setResetPos)
                {
                    // Moves the entity to set the reset position, then moves them back.
                    Vector2Int oldFloorPos = floorPos;
                    SetFloorPosition(newFloorPos, true, false);
                    SetFloorPosition(oldFloorPos, false, false);
                }

                // Gets the start and end points.
                startWorldPos = floorManager.GetFloorPositionInWorldSpace(floorPos, localYPos);
                endWorldPos = floorManager.GetFloorPositionInWorldSpace(newFloorPos, localYPos);

                // Internally, the player is considerd at their end location.
                floorPos = newFloorPos;

                // The entity is now moving.
                moving = true;

                // Resets the percent.
                interPercent = 0.0F;

                // The move has started.
                OnMoveStarted(startWorldPos, endWorldPos, interPercent);
            }

            
        }

        // Interpolates movement from one space to another on the floor in world space.
        protected virtual Vector3 InterpolateMove(Vector3 a, Vector3 b, float t)
        {
            // The resulting vector.
            Vector3 result;

            // Checks if movement should be curved.
            if(curvedMovement)
            {
                result = util.Interpolation.CatmullRom(a, a, b, b, t);

                // TODO: fix the jump arc so that it makes a curve.
                // The result's y-value. 
                float resultY = 0.0F;

                // The peak of the jump.
                float jumpPeak = localYPos + jumpHeight;

                // The lowest and highest points of the jump.
                Vector3 jumpLow = new Vector3(0, localYPos, 0); // Ground
                Vector3 jumpHigh = new Vector3(0, localYPos, 0); // Peak

                // Calculates the jump height (50% through should be the peak of the jump)
                if (t <= 0.5F)
                {
                    resultY = Mathf.Lerp(localYPos, localYPos + jumpHeight, t/0.5F);
                    // resultY = util.Interpolation.CatmullRom(jumpLow, jumpLow, jumpHigh, jumpHigh, t / 0.5F).y;
                }
                else
                {
                    resultY = Mathf.Lerp(localYPos + jumpHeight, localYPos, (t - 0.5F) / 0.5F);
                    // resultY = util.Interpolation.CatmullRom(jumpLow, jumpLow, jumpHigh, jumpHigh, (t - 0.5F) / 0.5F).y;
                }

                // Adjust for floor origin.
                if (floorManager.floorOrigin != null)
                    resultY += floorManager.floorOrigin.transform.position.y;

                // Sets the y-value.
                result.y = resultY;
            }
            else
            {
                result = Vector3.Lerp(a, b, t);
            }               

            return result;
        }

        // Called when a movement has been started.
        public virtual void OnMoveStarted(Vector3 start, Vector3 end, float t)
        {
            // ...
        }

        // Called when a movement is ongoing.
        public virtual void OnMoveOngoing(Vector3 start, Vector3 end, float t)
        {
            // ...
        }

        // Called when a movement is ending.
        public virtual void OnMoveEnded(Vector3 start, Vector3 end, float t)
        {
            floorManager.OnFloorEntityPositionChanged(this);
        }

        // Called when an entity interacts with this entity.
        public abstract void OnEntityInteract(FloorEntity entity);

        // Resets the floor entity.
        public virtual void ResetEntity()
        {
            SetFloorPosition(resetPos, false, false);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // If interpolation should be used.
            if(useMoveInter)
            {
                // If the entity is moving.
                if (moving)
                {
                    // Change percent.
                    interPercent += Time.deltaTime * moveSpeed;
                    interPercent = Mathf.Clamp(interPercent, 0, 1);

                    // Interpolates the movement.
                    transform.position = InterpolateMove(startWorldPos, endWorldPos, interPercent);

                    // If the end has been reached, stop moving.
                    if(interPercent >= 1.0F)
                    {
                        interPercent = 0.0F;
                        moving = false;
                        OnMoveEnded(startWorldPos, endWorldPos, interPercent);
                    }
                    else
                    {
                        // The move is still ongoing.
                        OnMoveOngoing(startWorldPos, endWorldPos, interPercent);
                    }
                }
            }
            
        }
    }
}
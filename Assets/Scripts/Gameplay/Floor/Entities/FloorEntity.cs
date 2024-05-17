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
        public enum entityGroup { none, player, geometry, enemy, item }

        // The game manager.
        public GameplayManager gameManager;

        // The floor manager.
        public FloorManager floorManager;

        // The group the asset is part of.
        protected entityGroup group = entityGroup.none;

        // The ID number of the floor asset.
        public int idNumber = -1;

        // The version of the entity (A-Z).
        public char version = 'A';

        // The world Y position of the floor asset.
        [Tooltip("The asset's position on the y-axis (up/down) in world space.")]
        public float localYPos = 0;

        // The floor position of the player (negative means the player isn't on the floor).
        public Vector2Int floorPos = new Vector2Int(-1, -1);

        // The reset position.
        public Vector2Int resetPos = new Vector2Int(-1, -1);

        // The list of button blocks this floor entity is linked to.
        private List<ButtonBlock> linkedButtonBlocks = new List<ButtonBlock>();

        [Header("Move Interpolation")]

        // If 'true', the entity interpolates movement across the floor.
        // If 'false', the entity instantly moves to their next location.
        public bool useMoveInter = true;

        // Gets set to 'true' when the entity is moving.
        private bool moving = false;

        // If true', movement is curved. If false, it's a straigh line (regular lerp).
        [Tooltip("If true, movement is curved. If false, it's a straight line.")]
        public bool curvedMovement = true;

        // The start position in local space.
        private Vector3 startLocalPos;

        // The end position in local space.
        private Vector3 endLocalPos;

        // The interpolation time
        private float interPercent = 0.0F;

        // The movement speed.
        public float moveSpeed = 4.0F;

        // The jump factor used for movement interpolation (only applies on curved movement, applied to bezier handles).
        [Tooltip("The jump factor for curved movement (applied to handles of bezier curve).")]
        public float jumpFactor = 7.0F;

        // Awake is called when the script instance is being loaded
        protected virtual void Awake()
        {
            // Gets the instance.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Gets the instance if this is null.
            if (floorManager == null)
                floorManager = FloorManager.Instance;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // ...
        }

        // Checks if the entity is moving (in move animation)
        public bool Moving
        {
            get { return moving; }
        }
        
        // Gets the group the asset is part of.
        public entityGroup GetGroup()
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
                case entityGroup.none: 
                    str = "N"; 
                    break;

                case entityGroup.player: 
                    str = "P"; 
                    break;

                case entityGroup.geometry: 
                    str = "G"; 
                    break;

                case entityGroup.enemy:
                    str = "E";
                    break;

                case entityGroup.item: 
                    str = "I"; 
                    break;
            }

            // Adds the ID
            str += idNumber.ToString("D2");

            return str;
        }

        // Returns the ID with the version as the floor info code.
        public string GetFloorInfoCode()
        {
            string infoCode = idNumber.ToString("D2");
            infoCode += version;
            return infoCode;
        }

        // Called when collision is entered.
        private void OnCollisionEnter(Collision collision)
        {
            // An entity.
            FloorEntity entity;

            // Tries to get the entity's componenet.
            if(collision.gameObject.TryGetComponent(out entity))
            {
                // Call entity interaction.
                OnEntityInteract(entity);
            }
        }

        // Set the floor position of the asset.
        public virtual void SetFloorPosition(Vector2Int newFloorPos, bool setResetPos, bool callInteract)
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
                startLocalPos = floorManager.GetFloorPositionInLocalSpace(floorPos, localYPos);
                endLocalPos = floorManager.GetFloorPositionInLocalSpace(newFloorPos, localYPos);

                // Internally, the player is considerd at their end location.
                floorPos = newFloorPos;

                // The entity is now moving.
                moving = true;

                // Resets the percent.
                interPercent = 0.0F;

                // The move has started.
                OnMoveStarted(startLocalPos, endLocalPos, interPercent);
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
                // The bezier handles are the start and end points with their y's adjusted.
                // This makes a perfect curve, but it doesn't make direct control of the jump height.
                Vector3 curveHeight = new Vector3(0, jumpFactor, 0);
                result = util.Interpolation.Bezier(a + curveHeight, a, b, b + curveHeight, t);
            }
            else
            {
                result = Vector3.Lerp(a, b, t);
            }               

            return result;
        }

        // Called when a movement has been started.
        public virtual void OnMoveStarted(Vector3 localStart, Vector3 localEnd, float t)
        {
            // ...
        }

        // Called when a movement is ongoing.
        public virtual void OnMoveOngoing(Vector3 localStart, Vector3 localEnd, float t)
        {
            // ...
        }

        // Called when a movement is ending.
        public virtual void OnMoveEnded(Vector3 localStart, Vector3 localEnd, float t)
        {
            floorManager.OnFloorEntityPositionChanged(this);
        }

        // Swaps positions in the floor array.
        // if 'callSetPosition' is true, the entity is physically moved.
        public void SwapPositionsInFloorArray(Vector2Int newFloorPos, bool callSetPosition)
        {
            // The array.
            FloorEntity[,] array = null;

            // Checks the group the entity is part of.
            switch (group)
            {
                case entityGroup.geometry:
                    array = floorManager.floorGeometry;

                    break;

                case entityGroup.enemy:
                    array = floorManager.floorEnemies;

                    break;

                case entityGroup.item:
                    array = floorManager.floorItems;

                    break;

                default:
                    Debug.LogWarning("This entity is not part of a valid group for floor array swapping.");
                    break;
            }

            // Array set.
            if(array != null)
            {
                // The old floor position.
                Vector2Int currFloorPos = new Vector2Int(-1, -1);

                // Goes through each row.
                for(int r = 0; r < array.GetLength(0); r++)
                {
                    // Goes through each column.
                    for(int c = 0; c < array.GetLength(1); c++)
                    {
                        // The object has been found.
                        if (array[r, c] == this)
                        {
                            currFloorPos.x = r;
                            currFloorPos.y = c;
                            break;
                        }
                    }

                    // Old position set.
                    if (currFloorPos.x != -1 && currFloorPos.y != -1)
                        break;
                }

                // Found the position, so set the other position.
                if (currFloorPos.x != -1 && currFloorPos.y != -1)
                    UpdatePositionInFloorArray(currFloorPos, newFloorPos, callSetPosition);
            }

        }

        // Swaps the position of the entity in the applicable floor array.
        // If 'callSetPosition' is true, then SetFloorPosition is called on the object(s) swapped.
        // If 'callSetPosition' is false, the floorPos objects are changed, but the entities are not physically moved.
        public void UpdatePositionInFloorArray(Vector2Int oldFloorPos, Vector2Int newFloorPos, bool callSetPosition)
        {
            // If there is an invalid floor position, do nothing.
            if(!floorManager.IsFloorPositionValid(oldFloorPos) || !floorManager.IsFloorPositionValid(newFloorPos))
            {
                return;
            }

            // The entity to be traded with.
            FloorEntity entity2 = null;


            // Checks the type of the entity.
            if(this is Block)
            {
                // Grabs the entity at the intended position.
                entity2 = floorManager.floorGeometry[newFloorPos.x, newFloorPos.y];

                // Swaps the entities.
                floorManager.floorGeometry[oldFloorPos.x, oldFloorPos.y] = (Block)entity2;
                floorManager.floorGeometry[newFloorPos.x, newFloorPos.y] = (Block)this;
            }
            else if(this is Enemy)
            {
                // Grabs the entity at the intended position.
                entity2 = floorManager.floorEnemies[newFloorPos.x, newFloorPos.y];

                // Swaps the entities.
                floorManager.floorEnemies[oldFloorPos.x, oldFloorPos.y] = (Enemy)entity2;
                floorManager.floorEnemies[newFloorPos.x, newFloorPos.y] = (Enemy)this;
            }
            else if(this is Item)
            {
                // Grabs the entity at the intended position.
                entity2 = floorManager.floorItems[newFloorPos.x, newFloorPos.y];

                // Swaps the entities.
                floorManager.floorItems[oldFloorPos.x, oldFloorPos.y] = (Item)entity2;
                floorManager.floorItems[newFloorPos.x, newFloorPos.y] = (Item)this;
            }
            else
            {
                Debug.LogWarning("This entity is not part of a valid group for floor array swapping.");
            }


            // Save the positions.
            floorPos = newFloorPos;

            if (entity2 != null)
                entity2.floorPos = oldFloorPos;

            // If SetPositions should be called for the entities.
            if(callSetPosition)
            {
                SetFloorPosition(newFloorPos, false, false);

                if (entity2 != null)
                    entity2.SetFloorPosition(oldFloorPos, false, false);
            }
        }


        // Call to add to the button block callback.
        public void AddToButtonBlock(ButtonBlock buttonBlock)
        {
            // Add to the button block callback.
            buttonBlock.AddClickButtonBlockCallback(OnButtonBlockClicked);

            // If the button block isn't in the list, add it.
            if(!linkedButtonBlocks.Contains(buttonBlock))
            {
                linkedButtonBlocks.Add(buttonBlock);
            }
        }

        // Call to remove from the button block callback.
        public void RemoveFromButtonBlock(ButtonBlock buttonBlock)
        {
            // Remove from the button block callback.
            buttonBlock.RemoveClickButtonBlockCallback(OnButtonBlockClicked);

            // If the button block is in the list, remove it.
            if (linkedButtonBlocks.Contains(buttonBlock))
            {
                linkedButtonBlocks.Remove(buttonBlock);
            }
        }

        // Called when a ButtonBlock has its button clicked.
        public virtual void OnButtonBlockClicked(FloorEntity entity)
        {
            // ...
        }

        // Called when an entity interacts with this entity.
        public abstract void OnEntityInteract(FloorEntity entity);

        // Kills the entity.
        public abstract void KillEntity();

        // Resets the floor entity.
        public virtual void ResetEntity()
        {
            SetFloorPosition(resetPos, false, false);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // The game is paused, so don't update anything.
            if (gameManager.paused)
                return;

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
                    transform.localPosition = InterpolateMove(startLocalPos, endLocalPos, interPercent);

                    // If the end has been reached, stop moving.
                    if(interPercent >= 1.0F)
                    {
                        interPercent = 0.0F;
                        moving = false;
                        OnMoveEnded(startLocalPos, endLocalPos, interPercent);
                    }
                    else
                    {
                        // The move is still ongoing.
                        OnMoveOngoing(startLocalPos, endLocalPos, interPercent);
                    }
                }
            }
            
        }

        // This function is called when the MonoBehaviour will be destroyed.
        protected virtual void OnDestroy()
        {
            // Goes through each button block this floor entity is linked to.
            for(int i = linkedButtonBlocks.Count - 1;  i >= 0; i--)
            {
                // If the block button exists.
                if(linkedButtonBlocks[i] != null)
                {
                    // Remove from the button block.
                    // This also removes the button from the linked button blocks list.
                    RemoveFromButtonBlock(linkedButtonBlocks[i]);
                }
            }
        }
    }
}
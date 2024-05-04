using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The floor manager.
    public class FloorManager : MonoBehaviour
    {
        // The singleton instance.
        private static FloorManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The gameplay manager.
        public GameplayManager gameManager;

        [Header("Floor")]

        // The current floor.
        public Floor currFloor = null;
        
        // The origin of all floor/the floor's parent (top left corner of the floor).
        public GameObject floorOrigin;

        // If 'true', the origin is the centre of the area. If not, it's the top left of the area.
        [Tooltip("Makes the floor origin the center of the area if true. If false, it's the top left corner.")]
        public bool originIsCenter = true;

        // The floor data.
        public FloorData floorData;

        // The spacing for floor assets.
        public Vector2 floorSpacing = new Vector2(5.0F, 5.0F);

        // The array of floor geometry
        public FloorEntity[,] floorGeometry = new FloorEntity[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];

        // The array of floor items
        // TODO: implement item array initalization.
        public FloorEntity[,] floorItems = new FloorEntity[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];

        [Header("Other")]

        // The time spent on this floor.
        public float floorTime = 0.0F;

        // Constructor
        private FloorManager()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected virtual void Awake()
        {
            // If the instance hasn't been set, set it to this object.
            if (instance == null)
            {
                instance = this;
            }
            // If the instance isn't this, destroy the game object.
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;
            }
        }     

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance if this is null.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Sets to this floor manager.
            if (gameManager.floorManager == null)
                gameManager.floorManager = this;

            // Gets the instance if this is null.
            if (floorData == null)
                floorData = FloorData.Instance;

            // Generates the floor
            GenerateFloor(floorData.GetFloor00());
        }

        // Gets the instance.
        public static FloorManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<FloorManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("FloorManager (singleton)");
                        instance = go.AddComponent<FloorManager>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public static bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Generates the floor by the ID.
        public void GenerateFloor(int id)
        {
            // Gets the floor data.
            Floor floor = FloorData.Instance.GetFloor(id);

            // Generates the floor.
            GenerateFloor(floor);
        }

        // Generates the floor.
        public void GenerateFloor(Floor floor)
        {
            // Clear current floor, and set new floor.
            ClearFloor();
            currFloor = floor;

            // If the floor is null, do nothing.
            if (floor == null)
                return;

            // The entry point for the player.
            EntryBlock entryBlock = null;

            // TODO: maybe use the array sizes instead of the max values?

            // Generate Assets
            // The row (Y) value.
            for (int row = 0; row < FloorData.FLOOR_ROWS; row++)
            {                
                // The column (X) value.
                for (int col = 0; col < FloorData.FLOOR_COLS; col++)
                {
                    // Calculates the new position for the potential assets.
                    // New position in the grid.
                    Vector2Int gridPos = new Vector2Int(col, row);

                    // The floor asset.
                    FloorEntity geoAsset = null;

                    // Checks the geometry.
                    switch(floor.geometry[col, row])
                    {
                        case 1: // Block 01
                            geoAsset = Instantiate(floorData.g01);
                            break;

                        case 2: // Block 02
                            geoAsset = Instantiate(floorData.g02);
                            break;

                        case 3: // Block 03
                            geoAsset = Instantiate(floorData.g03);
                            break;
                    }

                    // Initialize the asset.
                    if(geoAsset != null)
                    {
                        // Sets the floor manager.
                        geoAsset.floorManager = this;

                        // Sets the transform parent.
                        geoAsset.transform.parent = floorOrigin.transform;

                        // Sets the floor position.
                        geoAsset.SetFloorPosition(gridPos, true, false);

                        // Adds the asset to the list.
                        floorGeometry[col, row] = geoAsset;

                        // If the geo asset is the entry block, and this hasn't been set.
                        if(geoAsset is EntryBlock && entryBlock == null)
                            entryBlock = geoAsset as EntryBlock;
                    }
                    
                }
            }

            // Resets the player's position to the entry block's position.
            if (entryBlock != null)
                gameManager.player.SetFloorPosition(entryBlock.floorPos, true, true);
        }

        // Clears the floor.
        public void ClearFloor()
        {
            // TODO: use object pools instead of deleting and remaking objects.

            // Deletes all the geometry.
            for(int r = 0; r < floorGeometry.GetLength(0); r++)
            {
                for(int c = 0; c < floorGeometry.GetLength(1); c++)
                {
                    // Delete the element.
                    if(floorGeometry[r, c] != null)
                    {
                        Destroy(floorGeometry[r, c].gameObject);
                        floorGeometry[r, c] = null;
                    }
                }
            }

        }

        // Checks if a floor position is valid.
        public bool IsFloorPositionValid(Vector2Int floorPos)
        {
            // Checks position validity.
            // Checks rows (y) and cols (x)
            if (floorPos.y >= 0 && floorPos.y < currFloor.geometry.GetLength(0) &&
                floorPos.x >= 0 && floorPos.x < currFloor.geometry.GetLength(1))
            {
                return true; // Valid
            }
            else
            {
                return false; // Invalid
            }

        }

        // Gets the floor position in local space.
        public Vector3 GetFloorPositionInLocalSpace(Vector2Int floorPos, float localYPos)
        {
            // The local position.
            Vector3 localPos = new Vector3();

            // The world size.
            Vector2 worldSize = new Vector2(FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS);

            // Calculates the base floor position based on the spacing of indexes.
            localPos.x = floorPos.x * floorSpacing.x;
            localPos.y = localYPos; // The y-position stays the same.
            localPos.z = floorPos.y * floorSpacing.y;

            // Gets the local position without offsetting by the origin's position.
            if (originIsCenter) // Centre 
            {
                localPos -= new Vector3(worldSize.x * floorSpacing.x / 2.0F, 0, worldSize.y * floorSpacing.y / 2.0F);
            }

            // Return the world position.
            return localPos;
        }

        // Gets the floor position in world space.
        public Vector3 GetFloorPositionInWorldSpace(Vector2Int floorPos, float localYPos)
        {
            // The world position.
            Vector3 worldPos = new Vector3();
            
            // The world size.
            Vector2 worldSize = new Vector2(FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS);

            // Calculates the base floor position based on the spacing of indexes.
            worldPos.x = floorPos.x * floorSpacing.x;
            worldPos.y = localYPos; // The y-position stays the same.
            worldPos.z = floorPos.y * floorSpacing.y;

            // Floor origin exists.
            if(floorOrigin != null)
            {
                // Alters the world position.
                worldPos = floorOrigin.transform.position + worldPos;

                // If the origin is the centre instead of the top left...
                // Adjust the world position.
                if (originIsCenter) // Centre 
                {
                    worldPos -= new Vector3(worldSize.x * floorSpacing.x / 2.0F, 0, worldSize.y * floorSpacing.y / 2.0F);
                }
            }
            

            // Return the world position.
            return worldPos;
        }

        // Called when the floor entity's position has been changed.
        public void OnFloorEntityPositionChanged(FloorEntity entity)
        {
            // The floor position isn't valid, so do nothing.
            if (!IsFloorPositionValid(entity.floorPos))
                return;

            // Gets the floor position.
            Vector2Int floorPos = entity.floorPos;

            // If the entity isn't the player, check them for player interaction.
            if (!(entity is Player))
            {
                // Get the player.
                Player player = gameManager.player;

                // Player Check
                if (entity.floorPos == player.floorPos)
                {
                    player.OnEntityInteract(entity);
                }
            }

            // Geometry Check
            if (floorGeometry[floorPos.x, floorPos.y] != null && floorGeometry[floorPos.x, floorPos.y] != entity)
            {
                floorGeometry[floorPos.x, floorPos.y].OnEntityInteract(entity);
            }

            // Item Check
            if(floorItems[floorPos.x, floorPos.y] != null && floorItems[floorPos.x, floorPos.y] != entity)
            {
                floorItems[floorPos.x, floorPos.y].OnEntityInteract(entity);
            }
        }

        // Resets the floor.
        public void ResetFloor()
        {
            // Resets the geometry floor assets.
            for (int r = 0; r < floorGeometry.GetLength(0); r++) // Row
            {
                for(int c = 0; c < floorGeometry.GetLength(1); c++) // Column
                {
                    // Object exists.
                    if (floorGeometry[r, c] != null)
                    {
                        floorGeometry[r, c].ResetEntity();
                    }
                }
            }

            // TODO: reset other assets.

            // Resets the player.
            gameManager.player.ResetEntity();

            // Reset timer.
            floorTime = 0.0F;
        }

        // Tries player movement.
        public bool TryPlayerMovement(Player player, Vector2Int direc)
        {
            // No movement.
            if (direc == Vector2.zero)
                return false;

            // Gets the player's position.
            Vector2Int currFloorPos = player.floorPos;

            // The new player new floor position.
            Vector2Int newFloorPos = currFloorPos + direc;

            // Checks movement validity.
            if(!IsFloorPositionValid(newFloorPos))
            {
                // Validity check failed.
                return false;
            }

            // Restricts player movement.
            if (floorGeometry[newFloorPos.x, newFloorPos.y] != null)
            {
                // The element is active and enabled.
                if(floorGeometry[newFloorPos.x, newFloorPos.y].isActiveAndEnabled)
                {
                    // Checks
                    if (floorGeometry[newFloorPos.x, newFloorPos.y] is Block)
                    {
                        // Gets the block.
                        Block block = (Block)floorGeometry[newFloorPos.x, newFloorPos.y];

                        // If the block is usable.
                        if (block.UsableBlock())
                        {
                            // TODO: move the interaction function to FloorEntity, and...
                            // Have it be called everytime the entity is moved to a new space.

                            // If the player is using move interpolation...
                            // Some operations will be done at the end of the movement.
                            // If the movement is isntant, do them here.
                            if(player.useMoveInter)
                            {
                                // Start the movement.
                                player.MoveEntity(newFloorPos, false, false);
                            }
                            else
                            {
                                // Sets the position.
                                player.SetFloorPosition(newFloorPos, false, true);                             
                            }

                            // Success.
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // It's not a block, so the player can't jump on it.
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else // No block at that space for the player to jump on.
            {
                return false;
            }
        }

        // Called when the floor is completed.
        public void OnFloorComplete()
        {
            // TODO: save floor completion time.
        }

        // Called when the floor is failed.
        public void OnFloorFailed()
        {

        }

        // Update is called once per frame
        void Update()
        {
            floorTime += Time.deltaTime;
        }

        // This function is called when the MonoBehaviour will be destroyed.
        private void OnDestroy()
        {
            // If the saved instance is being deleted, set 'instanced' to false.
            if (instance == this)
            {
                instanced = false;
            }
        }
    }
}
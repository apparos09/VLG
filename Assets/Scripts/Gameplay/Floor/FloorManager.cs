using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

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

        // The goal for the game.
        public Goal goal;

        // The coroutine for loading the floor assets.
        private Coroutine floorGenCoroutine = null;

        [Header("Floor")]
        
        // The origin of all floor/the floor's parent (top left corner of the floor).
        public GameObject floorOrigin;

        // If 'true', the origin is the centre of the area. If not, it's the top left of the area.
        [Tooltip("Makes the floor origin the center of the area if true. If false, it's the top left corner.")]
        public bool originIsCenter = true;

        // The current floor.
        public Floor currFloor = null;

        // The floor data.
        public FloorData floorData;

        // The spacing for floor assets.
        public Vector2 floorSpacing = new Vector2(5.0F, 5.0F);

        // If 'true', the floor is fliped vertically. A flipped floor matches how the array looks in the code.
        [Tooltip("Flips the floor map vertically if true.")]
        public bool flipFloorVert = false;

        // The death plane for elements that fall off the map.
        // I don't know if I'll use this, but I'll keep it here.
        //[Tooltip("The death plane Y. If an element falls below this value, kill it.")]
        //public float deathPlaneY = -10;

        // TODO: add object pools so that you aren't constantly deleting and remaking everything.

        // The array of floor geometry
        // TODO: I don't know if all floor geometry will be blocks, hence why this is just floor entity. Maybe change this.
        public Block[,] floorGeometry = new Block[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];

        // The array of floor enemies.
        public Enemy[,] floorEnemies = new Enemy[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];

        // The array of floor items
        public Item[,] floorItems = new Item[FloorData.FLOOR_ROWS, FloorData.FLOOR_COLS];

        [Header("Floor/Stats")]

        // The time spent on this floor.
        public float floorTime = 0.0F;

        // The floor turns.
        public int floorTurns = 0;

        // The floor turn limit.
        public int floorTurnsMax = -1;

        // If 'true', the player has a limited number of turns on the floor.
        [Tooltip("If 'true', the player has a limited number of turns to complete the floor.")]
        public bool limitTurns = false;

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
            // Gets the floor data and tries to generate it.
            GenerateFloor(FloorData.Instance.GetFloor(id));
        }

        // Generates the floor using the provided code.
        public void GenerateFloor(string code)
        {
            GenerateFloor(FloorData.Instance.GetFloor(code));
        }

        // Generates the floor.
        public void GenerateFloor(Floor floor)
        {
            // Clear current floor, and set new floor.
            ClearFloor();
            currFloor = floor;

            // If the floor is null, do nothing.
            if (floor == null)
            {
                Debug.LogError("Attempted to generate a floor that was null.");
                return;
            }
                

            // The entry point for the player.
            EntryBlock entryBlock = null;

            // TODO: maybe use the array sizes instead of the max values?

            // Generate Assets
            // The row (Y) value.
            for (int row = 0; row < currFloor.geometry.GetLength(0); row++)
            {                
                // The column (X) value.
                for (int col = 0; col < currFloor.geometry.GetLength(1); col++)
                {
                    // Calculates the new position for the potential assets.
                    // New position in the grid.
                    Vector2Int gridPos = new Vector2Int(col, ConvertFloorRowPosition(row));

                    // The floor assets for geometry, items, and enemies.
                    Block geoEntity;
                    Enemy emyEntity;
                    Item itmEntity;

                    // Generates the geometry, enemy, and item elements.
                    // These use the base column and row values, not the converted values.
                    // TODO: this should probably be [row, col], but it's too late to change that right now.
                    geoEntity = floorData.InstantiateGeometryElement(floor.geometry[col, row]);
                    emyEntity = floorData.InstantiateEnemyElement(floor.enemies[col, row]);
                    itmEntity = floorData.InstantiateItemElement(floor.items[col, row]);


                    // Initialize the entity.
                    if (geoEntity != null)
                    {
                        // Sets the default values.
                        SetDefaultEntityValues(geoEntity, gridPos);

                        // If the geo asset is the entry block, and this hasn't been set.
                        if(geoEntity is EntryBlock && entryBlock == null)
                            entryBlock = geoEntity as EntryBlock;
                    }

                    // ENEMY //
                    if (emyEntity != null)
                    {
                        // Sets the default values.
                        SetDefaultEntityValues(emyEntity, gridPos);
                    }

                    // ITEM //
                    if (itmEntity != null)
                    {
                        // Sets the default values.
                        SetDefaultEntityValues(itmEntity, gridPos);
                    }
                }
            }

            // Resets the player's position to the entry block's position.
            if (entryBlock != null)
                gameManager.player.SetFloorPosition(entryBlock.floorPos, true, true);

            // Floor Turn Limit
            if(floor.turnsMax <= 0) // If 0 or less, there is no limit.
            {
                limitTurns = false;
                floorTurnsMax = 0;
            }
            else // There are limited turns.
            {
                limitTurns = true;
                floorTurnsMax = floor.turnsMax;
            }
            

            // Sets the Skybox
            floorData.SetSkybox(floor);

            // Sets the BGM
            gameManager.gameAudio.PlayGameplayBgm(floor);

            // Update the floor text and game progress bar.
            gameManager.gameUI.UpdateAllHUDElements();

            // Resets the player.
            gameManager.player.ResetEntity();

            // Set to the default view.
            gameManager.gameCamera.SetView(0);
        }

        // Coroutine Variants
        // Generates the floor by the ID as a coroutine.
        public void GenerateFloorAsCoroutine(int id)
        {
            // Gets the floor data and tries to generate it as a coroutine.
            GenerateFloorAsCoroutine(FloorData.Instance.GetFloor(id));
        }

        // Generates the floor using the provided code as a coroutine.
        public void GenerateFloorAsCoroutine(string code)
        {
            // Gets the floor data and tries to generate it as a coroutine.
            GenerateFloorAsCoroutine(FloorData.Instance.GetFloor(code));
        }

        // Generates the floor as a coroutine.
        public void GenerateFloorAsCoroutine(Floor floor)
        {
            // A coroutine is already in progress.
            if(floorGenCoroutine != null)
            {
                Debug.LogAssertion("A floor is already being generated.");
                return;
            }

            // Starts the coroutine.
            floorGenCoroutine = StartCoroutine(GenerateFloorCoroutine(floor));
        }

        // Generates a floor as a coroutine
        private IEnumerator GenerateFloorCoroutine(Floor floor)
        {
            // Save the time scale.
            float timeScale = Time.timeScale;

            // Turn on the loading screen.
            gameManager.floorLoadingScreen.gameObject.SetActive(true);
            gameManager.floorLoadingScreen.UpdateFloorDisplay(floor);

            // Stop time, and disable all player inputs.
            Time.timeScale = 0.0F; // Stop delta time.
            gameManager.player.enabledInputs = false;

            // Wait for one second.
            yield return new WaitForSecondsRealtime(0.25F);            

            // Generate the floor, and stall.
            GenerateFloor(floor);

            yield return null;

            // Resume time and enable player inputs.
            Time.timeScale = timeScale;
            gameManager.player.enabledInputs = true;

            yield return null;

            // Turn off the loading screen.
            gameManager.floorLoadingScreen.gameObject.SetActive(false);

            // If a tutorial is running, restart the tutorial to make sure...
            // The player's inputs are disabled, and the time scale is set to 0.
            if(gameManager.IsTutorialRunning())
            {
                // Restarts the tutorial.
                gameManager.tutorials.RestartTutorial();
            }

            // Stops the coroutine once it's finished.
            if (floorGenCoroutine != null)
            {
                StopCoroutine(floorGenCoroutine);
                floorGenCoroutine = null;
            }
        }



        // Sets the default entity values.
        // gridPos = (col, row)
        private void SetDefaultEntityValues(FloorEntity entity, Vector2Int gridPos)
        {
            // Sets the floor manager.
            entity.floorManager = this;

            // Sets the transform parent.
            entity.transform.parent = (floorOrigin != null) ? floorOrigin.transform : null;

            // Sets the floor position.
            entity.SetFloorPosition(gridPos, true, false);

            // Adds the asset to the applicable array.
            if(entity is Block) // Block
            {
                floorGeometry[gridPos.x, gridPos.y] = (Block)entity;
            }
            else if(entity is Enemy) // Enemy
            {
                floorEnemies[gridPos.x, gridPos.y] = (Enemy)entity;
            }
            else if(entity is Item) // Item
            {
                floorItems[gridPos.x, gridPos.y] = (Item)entity;
            }
            
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

            // Deletes all the enemies.
            for (int r = 0; r < floorEnemies.GetLength(0); r++)
            {
                for (int c = 0; c < floorEnemies.GetLength(1); c++)
                {
                    // Delete the element.
                    if (floorEnemies[r, c] != null)
                    {
                        Destroy(floorEnemies[r, c].gameObject);
                        floorEnemies[r, c] = null;
                    }
                }
            }

            // Deletes all the items.
            for (int r = 0; r < floorItems.GetLength(0); r++)
            {
                for (int c = 0; c < floorItems.GetLength(1); c++)
                {
                    // Delete the element.
                    if (floorItems[r, c] != null)
                    {
                        Destroy(floorItems[r, c].gameObject);
                        floorItems[r, c] = null;
                    }
                }
            }

            // Reset the floor time and turns.
            floorTime = 0;
            floorTurns = 0;
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
        // NOTE: this treats (rows, cols) as (y, x), which is different from how the grid is read.
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
        // NOTE: this treats (rows, cols) as (y, x), which is different from how the grid is read.
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

        // Gets the provided floor row position in local space.
        // If the floor is flipped, the final row position is reflected as such.
        public int ConvertFloorRowPosition(int baseRow)
        {
            // Calculates the new row value if the floor should be flipped vertically.
            int finalRow = flipFloorVert ? currFloor.geometry.GetLength(0) - 1 - baseRow : baseRow;

            return finalRow;
        }

        // Gets the floor entities.
        // Gets the floor geometry entity.
        public Block GetFloorGeometryEntity(Vector2Int floorPos)
        {
            // If the floor position is valid.
            if (IsFloorPositionValid(floorPos))
            {
                return floorGeometry[floorPos.x, floorPos.y];
            }
            else // Not valid.
            {
                return null;
            }
        }

        // Gets the floor enemy entity.
        public Enemy GetFloorEnemyEntity(Vector2Int floorPos)
        {
            // If the floor position is valid.
            if (IsFloorPositionValid(floorPos))
            {
                return floorEnemies[floorPos.x, floorPos.y];
            }
            else // Not valid.
            {
                return null;
            }
        }

        // Gets the floor item entity.
        public Item GetFloorItemEntity(Vector2Int floorPos)
        {
            // If the floor position is valid.
            if (IsFloorPositionValid(floorPos))
            {
                return floorItems[floorPos.x, floorPos.y];
            }
            else // Not valid.
            {
                return null;
            }
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
                // Checks if the entity is an enemy.
                if (entity is Enemy)
                {
                    Enemy enemy = (Enemy)entity;

                    // If the enemy doesn't ignore geometry, interact with the geometry.
                    if(!enemy.ignoreGeometry)
                        floorGeometry[floorPos.x, floorPos.y].OnEntityInteract(entity);
                }
                else
                {
                    floorGeometry[floorPos.x, floorPos.y].OnEntityInteract(entity);
                }
                
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
            // Inital reset based on the current floor status.
            // This is done twice to avoid triggering the player's death animation.
            gameManager.player.ResetEntity();

            // GEOMETRY
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

            // NOTE: enemies are deleted when they're killed, so the game kills all existing enemies...
            // And restores them from the floor data.

            // ENEMIES 
            // Deletes all existing enemies
            for (int r = 0; r < floorEnemies.GetLength(0); r++) // Row
            {
                for (int c = 0; c < floorEnemies.GetLength(1); c++) // Column
                {
                    // Kill the enemy.
                    if (floorEnemies[r, c] != null)
                    {
                        floorEnemies[r, c].KillEntity();
                    }
                }
            }


            // ITEMS
            // Deletes all items.
            for (int r = 0; r < floorItems.GetLength(0); r++) // Row
            {
                for (int c = 0; c < floorItems.GetLength(1); c++) // Column
                {
                    // Kill the item.
                    if (floorItems[r, c] != null)
                    {
                        floorItems[r, c].KillEntity();
                    }
                }
            }

            // Re-generate enemies and items
            // The row (Y) value.
            for (int row = 0; row < currFloor.enemies.GetLength(0); row++)
            {
                // The column (X) value.
                for (int col = 0; col < currFloor.enemies.GetLength(1); col++)
                {
                    // Calculates the new position for the potential assets.
                    // New position in the grid.
                    Vector2Int gridPos = new Vector2Int(col, ConvertFloorRowPosition(row));

                    // Generates the enemy at the provided index (uses base array location)
                    Enemy emyEntity = floorData.InstantiateEnemyElement(currFloor.enemies[col, row]);
                    Item itmEntity = floorData.InstantiateItemElement(currFloor.items[col, row]);

                    // Sets the default values for the enemy.
                    if (emyEntity != null)
                    {
                        // Sets the default values.
                        SetDefaultEntityValues(emyEntity, gridPos);
                    }

                    // Sets the default values for the item.
                    if (itmEntity != null)
                    {
                        // Sets the default values.
                        SetDefaultEntityValues(itmEntity, gridPos);
                    }
                }
            }


            // Resets the player.
            gameManager.player.ResetEntity();

            // Reset timer and turn count.
            floorTime = 0.0F;
            floorTurns = 0;

            // Updates all the HUD elements.
            gameManager.gameUI.UpdateAllHUDElements();
        }

        // Tries entity movement.
        public bool TryEntityMovement(FloorEntity entity, Vector2Int direc)
        {
            // No movement.
            if (direc == Vector2.zero)
                return false;

            // Gets the player's position.
            Vector2Int currFloorPos = entity.floorPos;

            // The new player new floor position.
            Vector2Int newFloorPos = currFloorPos + direc;

            // Checks movement validity.
            if(!IsFloorPositionValid(newFloorPos))
            {
                // Validity check failed.
                return false;
            }


            // Checks entity type...
            if (entity.GetGroup() == FloorEntity.entityGroup.geometry) // Geometry
            {
                // Checks if the space is open for the block to move there.
                if (floorGeometry[newFloorPos.x, newFloorPos.y] == null) // Space open.
                {
                    // Checks how the block should move.
                    if (entity.useMoveInter)
                    {
                        // Start the movement.
                        entity.MoveEntity(newFloorPos, false, false);
                    }
                    else
                    {
                        // Sets the position.
                        entity.SetFloorPosition(newFloorPos, false, true);
                    }

                    // Swap positions in array.
                    entity.UpdatePositionInFloorArray(currFloorPos, newFloorPos, false);

                    // Movement successful.
                    return true;
                }
                else // Space taken.
                {
                    return false;
                }

            }
            else // Other group
            {
                // If the entity is an enemy.
                if(entity is Enemy)
                {
                    // Enemy
                    Enemy enemy = (Enemy)entity;

                    // Checks that the space is available
                    if (floorEnemies[newFloorPos.x, newFloorPos.y] == null)
                    {
                        // If the geometry should be ignored...
                        if (enemy.ignoreGeometry)
                        {
                            // Checks how the enemy should move.
                            if (enemy.useMoveInter)
                            {
                                // Start the movement.
                                enemy.MoveEntity(newFloorPos, false, false);
                            }
                            else
                            {
                                // Sets the position.
                                enemy.SetFloorPosition(newFloorPos, false, true);
                            }

                            // Swap positions in array.
                            entity.UpdatePositionInFloorArray(currFloorPos, newFloorPos, false);

                            // Movement successful.
                            return true;
                        }

                        // The enemy abides by the geometry, so go to block check.
                    }
                    else
                    {
                        // Space not available, so don't move.
                        return false;
                    } 
                    
                }

                // Restricts entity movement to floor geometry.
                if (floorGeometry[newFloorPos.x, newFloorPos.y] != null)
                {
                    // The element is active and enabled.
                    if (floorGeometry[newFloorPos.x, newFloorPos.y].isActiveAndEnabled)
                    {
                        // Checks that the floor geometry entity is a block.
                        if (floorGeometry[newFloorPos.x, newFloorPos.y] is Block)
                        {
                            // Gets the block.
                            Block block = floorGeometry[newFloorPos.x, newFloorPos.y];

                            // If the block is active and enabled, and if the block is usable.
                            if (block.isActiveAndEnabled && block.UsableBlock(entity))
                            {
                                // TODO: move the interaction function to FloorEntity, and...
                                // Have it be called everytime the entity is moved to a new space.

                                // If the entity is using move interpolation...
                                // Some operations will be done at the end of the movement.
                                // If the movement is isntant, do them here.
                                if (entity.useMoveInter)
                                {
                                    // Start the movement.
                                    entity.MoveEntity(newFloorPos, false, false);
                                }
                                else
                                {
                                    // Sets the position.
                                    entity.SetFloorPosition(newFloorPos, false, true);
                                }

                                // Swap positions in array (doesn't get used for the player)
                                if(!(entity is Player))
                                    entity.UpdatePositionInFloorArray(currFloorPos, newFloorPos, false);

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
        }

        // Called when the player moves via their own input.
        // Player is the player object, and move direction is where they moved.
        // Success shows if the movement worked.
        public void OnPlayerMovementInput(Player player, Vector2Int moveDirec, bool success)
        {       
            // Moving the Copy Enemies
            // Gives the information to all the copy enemies.
            foreach (CopyEnemy copy in CopyEnemy.copyEnemies)
            {
                // The copy is active.
                if (copy.isActiveAndEnabled)
                {
                    // Copies the movement.
                    copy.CopyMovement(moveDirec);
                }
            }

            // If the movement was successful.
            if(success)
            {
                // Gives information to all bar enemies.
                foreach (BarEnemy barEnemy in BarEnemy.barEnemies)
                {
                    // Apply rotation if active.
                    if (barEnemy.isActiveAndEnabled && barEnemy.rotateBars)
                    {
                        // Triggers the next rotation.
                        barEnemy.RotateBarsBy45Degrees();
                        // barEnemy.RotateBarsBy90Degrees();
                    }
                }
            }
            

            // Updates the turns information.
            gameManager.UpdateTurns();
        }

        // Called on the player's attack input.
        public void OnPlayerAttackInput(Player player, Vector2Int attackDirec, Vector2Int attackFloorPos)
        {
            // Updating the switch blocks - they switch when the player attacks 
            foreach (SwitchBlock switchBlock in SwitchBlock.switchBlocks)
            {
                // Toggles the block's state.
                switchBlock.ToggleBlock();
            }

            // Updates the turns information.
            gameManager.UpdateTurns();
        }

        // Updates the number of turns.
        public void UpdateTurns()
        {
            // If the game is paused, don't updatre the turns.
            if (gameManager.IsPaused())
                return;

            // Increment the turn count.
            floorTurns++;

            // If the turns should be limited...
            if(limitTurns)
            {
                // The max has been reached.
                if(floorTurns >= floorTurnsMax)
                {
                    ResetFloor();
                }
            }
        }

        // Called when the goal is entered. This manages the setup for the next floor, or the ending of the game. 
        public void OnGoalTriggered()
        {
            // No floor set, so just finish the game.
            if (currFloor == null)
            {
                gameManager.FinishGame();
            }
            else
            {
                OnFloorComplete();
            }
        }

        // Called when the floor is completed.
        // See OnGoalEntered in GameManager to see how the game handles a stage being completed.
        public void OnFloorComplete()
        {
            // Saves the floor turns and times to an array.
            gameManager.floorTurns[currFloor.id] = floorTurns;
            gameManager.floorTimes[currFloor.id] = floorTime;

            // Gets the next ID.
            int nextFloorId = currFloor.id + 1;

            // Checks if there are remaining floors
            // Keep in mind that the debug floor/floor 0 is part of the floor count.
            if (nextFloorId < gameManager.floorCount && nextFloorId < FloorData.FLOOR_COUNT_MAX)
            {
                // Generates the next floor. Checks if a coroutine should be used or not.
                if(gameManager.useFloorCoroutine)
                    GenerateFloorAsCoroutine(nextFloorId);
                else
                    GenerateFloor(nextFloorId);
            }
            else // No other floors, so finish the game.
            {
                gameManager.FinishGame();
            }

            // If the game should allow saves, save the game.
            if (gameManager.IsSavingLoadingEnabled())
            {
                // If a coroutine is being used, increment the ID by one...
                // So that the current floor is saved.
                if(gameManager.useFloorCoroutine)
                {
                    // The floor isn't finished generating yet, so this workaround is needed.
                    currFloor.id++;
                    gameManager.SaveGame();
                    currFloor.id--;
                }
                else // No coroutine, so just save like normal.
                {
                    // Since there's no coroutine, this won't be reached until the floor is generated.
                    gameManager.SaveGame();
                }
                
            }
                
        }

        // Called when the floor is failed.
        public void OnFloorFailed()
        {
            // Saves these values temporarily.
            float timeTemp = floorTime;
            // int turnsTemp = floorTurns; // Taken out to account for floors with limited turns.

            // Resets the floor.
            ResetFloor();

            // Updates the time and turns with the old values since the floor was failed.
            floorTime = timeTemp;
            // floorTurns = turnsTemp;
        }

        // Update is called once per frame
        void Update()
        {
            // If the game is not paused.
            if(!gameManager.IsPaused())
            {
                floorTime += Time.deltaTime;
            }
            
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
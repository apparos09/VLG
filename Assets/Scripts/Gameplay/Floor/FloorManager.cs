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

        // The floor data.
        public FloorData floorData;

        // The spacing for floor assets.
        public Vector2 floorSpacing = new Vector2(5.0F, 5.0F);

        // The list of floor assets.
        public List<FloorAsset> floorAssets;

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
            // GenerateFloor(floorData.GetFloor00());
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
            // TODO: create instances

            // TODO: maybe use the array sizes instead of the max values?

            // Generate Assets
            // The row (Y) value.
            for (int row = 0; row < FloorData.FLOOR_ROWS_MAX; row++)
            {                
                // The column (X) value.
                for (int col = 0; col < FloorData.FLOOR_COLS_MAX; col++)
                {
                    // Calculates the new position for the potential assets.
                    // New position in the grid.
                    Vector2Int gridPos = new Vector2Int(col, row);

                    // The floor asset.
                    FloorAsset geoAsset = null;

                    // Checks the geometry.
                    switch(floor.geometry[col, row])
                    {
                        case 1: // Block 00
                            geoAsset = Instantiate(floorData.block00);
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
                        geoAsset.SetFloorPosition(gridPos, true);

                        // Adds the asset to the list.
                        floorAssets.Add(geoAsset);
                    }
                    
                }
            }
            
            
            // TODO: reset player position
        }

        // Clears the floor.
        public void ClearFloor()
        {
            // The floor assets.
            for(int i = 0; i < floorAssets.Count; i++)
            {
                Destroy(floorAssets[0]);
            }

            // Clears the list.
            floorAssets.Clear();
        }

        // Resets the floor.
        public void ResetFloor()
        {
            // // The floor assets.
            // for (int i = 0; i < floorAssets.Count; i++)
            // {
            //     // TODO: reset floor asset.
            // }

            // Reset timer.
            floorTime = 0.0F;
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
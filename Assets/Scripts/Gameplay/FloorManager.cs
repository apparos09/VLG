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

        // The origin of all floor/the floor's parent.
        public GameObject floorOrigin;

        // The current floor.
        public Floor currFloor = null;

        [Header("Prefabs")]

        // Block 00.
        public GameObject block00;

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
            // The column (X) value.
            for (int col = 0; col < FloorData.FLOOR_COLS_MAX; col++)
            {
                // The row (Y) value.
                for (int row = 0; row < FloorData.FLOOR_ROWS_MAX; row++)
                {
                    // Generate Assets
                }
            }
            
            // TODO: reset player position
        }

        // Update is called once per frame
        void Update()
        {

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The floor.
    public class Floor
    {
        // The id number (double digit).
        public int id;

        // The floor geometry (includes door).
        public int[,] geometry;

        // The items.
        public int[,] items;
    }

    // The floor data.
    public class FloorData : MonoBehaviour
    {

        // The floor count (ignores the debug floor/floor 0).
        public const int FLOOR_COUNT = 0;

        // All floors are the same size, but the amount of space used will vary.

        // The amount of floor rows.
        public const int FLOOR_ROWS = 10;

        // The amount of floor columns.
        public const int FLOOR_COLS = 10;

        // The singleton instance.
        private static FloorData instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        [Header("Prefabs")]

        // Geometry (G-Series Elements)
        // 00 is a blank space
        public FloorAsset g01;
        public FloorAsset g02;
        public FloorAsset g03;

        // Constructor
        private FloorData()
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
        public static FloorData Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<FloorData>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("FloorData (singleton)");
                        instance = go.AddComponent<FloorData>();
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

        // General Function
        // Gets the floor ID.
        public Floor GetFloor(int id)
        {
            // Floor
            Floor floor;

            // Gets the floor.
            switch(id)
            {
                default:
                case 0:
                    floor = GetFloor00();
                    break;
            }

            return floor;
        }

        // Floor 00
        public Floor GetFloor00()
        {
            // Template
            // {{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            // { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            // };

            Floor floor = new Floor();

            // ID
            floor.id = 0;

            // Geometry
            int[,] geometry = new int[FLOOR_COLS, FLOOR_ROWS]{
                {1, 3, 0, 0, 0, 0, 0, 0, 0, 3},
                {3, 0, 3, 0, 0, 0, 0, 0, 0, 0},
                {3, 3, 3, 3, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {3, 0, 0, 0, 0, 0, 0, 0, 0, 3}};
                      
            floor.geometry = geometry;


            // Items
            int[,] items = new int[FLOOR_COLS, FLOOR_ROWS];
            floor.items = items;


            return floor;
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
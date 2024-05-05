using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The floor.
    public class Floor
    {
        // The code for the floor (5 chars).
        public string code;

        // The id number (double digit).
        public int id;

        // The floor geometry (includes door).
        public int[,] geometry;

        // The enemies.
        public int[,] enemies;

        // The items.
        public int[,] items;

    }

    // The floor data.
    public class FloorData : MonoBehaviour
    {

        // The floor count (ignores the debug floor/floor 0).
        public const int FLOOR_COUNT = 1;

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

        // The floor codes, which are used to skip floors on the title screen.
        // A floor code has 4 digits, but "0" and "O" aren't used because they look similar.
        // TODO: don't allow the player to use "0" or "O".
        private string[] floorCodes = new string[FLOOR_COUNT + 1]
        {
            "0000",
            "91AB"
        };

        [Header("Prefabs")]

        // Geometry (G-Series Elements)
        // 00 is a blank space
        public FloorEntity g01;
        public FloorEntity g02;
        public FloorEntity g03;
        public FloorEntity g04;
        public FloorEntity g05;
        public FloorEntity g06;

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

        // Gets the geometry element.
        public FloorEntity InstantiateGeometryElement(int id)
        {
            // The geometry entity
            FloorEntity geoEntity;

            // Instantiates the goemetry object.
            switch (id)
            {
                case 1:
                    geoEntity = Instantiate(g01);
                    break;

                case 2:
                    geoEntity = Instantiate(g02);
                    break;

                case 3:
                    geoEntity = Instantiate(g03);
                    break;

                case 4:
                    geoEntity = Instantiate(g04);
                    break;

                case 5:
                    geoEntity = Instantiate(g05);
                    break;

                case 6:
                    geoEntity = Instantiate(g06);
                    break;


                case 0:
                default:
                    geoEntity = null;
                    break;
            }

            return geoEntity;
        }

        // Gets the enemy element.
        public Enemy InstantiateEnemyElement(int id)
        {
            // The enemy entity
            Enemy emyEntity;

            // Instantiates the enemy object.
            switch (id)
            {
                case 0:
                default:
                    emyEntity = null;
                    break;
            }

            return emyEntity;
        }

        // Gets the item element.
        public Item InstantiateItemElement(int id)
        {
            // The item entity
            Item itmEntity;

            // Instantiates the item object.
            switch (id)
            {
                case 0:
                default:
                    itmEntity = null;
                    break;
            }

            return itmEntity;
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

                case 1:
                    floor = GetFloor01();
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

            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 0;
            floor.code = floorCodes[floor.id];

            // Geometry
            int[,] geometry = new int[FLOOR_COLS, FLOOR_ROWS]{
                {1, 3, 0, 0, 0, 0, 0, 0, 0, 3},
                {3, 0, 3, 0, 5, 0, 0, 0, 0, 0},
                {3, 3, 3, 3, 3, 4, 0, 0, 0, 0},
                {0, 0, 2, 0, 6, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {3, 0, 0, 0, 0, 0, 0, 0, 0, 3}};
                      
            floor.geometry = geometry;


            // Enemies
            int[,] enemies = new int[FLOOR_COLS, FLOOR_ROWS] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

            floor.enemies = enemies;


            // Items
            int[,] items = new int[FLOOR_COLS, FLOOR_ROWS] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            
            floor.items = items;


            return floor;
        }

        // Floor 00
        public Floor GetFloor01()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 1;
            floor.code = floorCodes[floor.id];

            // Geometry
            int[,] geometry = new int[FLOOR_COLS, FLOOR_ROWS]{
            { 0, 0, 0, 0, 0, 0, 0, 3, 3, 2},
            { 0, 0, 0, 0, 0, 0, 0, 3, 3, 3},
            { 0, 0, 0, 0, 0, 0, 3, 3, 3, 3},
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0},
            { 0, 0, 0, 0, 0, 3, 3, 0, 0, 0},
            { 0, 0, 0, 0, 3, 3, 0, 0, 0, 0},
            { 0, 0, 0, 3, 3, 0, 0, 0, 0, 0},
            { 3, 3, 3, 3, 0, 0, 0, 0, 0, 0},
            { 3, 3, 3, 0, 0, 0, 0, 0, 0, 0},
            { 1, 3, 3, 0, 0, 0, 0, 0, 0, 0}};

            floor.geometry = geometry;

            // Enemies
            int[,] enemies = new int[FLOOR_COLS, FLOOR_ROWS] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

            floor.enemies = enemies;

            // Items
            int[,] items = new int[FLOOR_COLS, FLOOR_ROWS] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

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
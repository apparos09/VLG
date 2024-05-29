using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
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
        public string[,] geometry;

        // The enemies.
        public string[,] enemies;

        // The items.
        public string[,] items;

    }

    // The floor data.
    public class FloorData : MonoBehaviour
    {
        // A floor element code.
        private struct FloorElementInfo
        {
            // The entity group.
            public FloorEntity.entityGroup group;

            // The ID number.
            public int id;

            // The object type.
            public char version;
        }


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

        // Automatically overrides the prefab's ID and version values using grid information instead.
        [Tooltip("Autosets ID information for prefabs generated instances.")]
        public bool autoSetIdInfo = true;

        // The floor codes, which are used to skip floors on the title screen.
        // A floor code has 4 digits, but "0" and "O" aren't used because they look similar.
        // TODO: don't allow the player to use "0" or "O".
        private string[] floorCodes = new string[FLOOR_COUNT + 1]
        {
            "0000",
            "91AB"
        };


        // Geometry (G-Group)
        [Header("Geometry")]

        // 00 is a blank space
        [Header("Geometry/Entry")]
        public Block g01A;

        // Goal
        [Header("Geometry/Goal")]
        public Block g02A;

        // Block
        [Header("Geometry/Blocks")]
        public Block g03A;

        // Hazard
        [Header("Geometry/Hazard")]
        public Block g04A;

        // Damaged
        [Header("Geometry/Damaged")]
        public Block g05A;

        // Phase
        [Header("Geometry/Phase")]
        public Block g06A;

        [Header("Geometry/Portal")]
        public Block g07A;

        [Header("Geometry/Switch")]
        public Block g08A;
        public Block g08B;

        [Header("Geometry/Button")]
        public Block g09A;

        // Enemies (E-Group)
        [Header("Enemies")]
        public Enemy e01A;
        public Enemy e02A;
        public Enemy e03A;

        // Items (I-Group)
        [Header("Items")]
        public Item i01A;

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

        // FLOOR CODE
        // Checks if the floor code is valid.
        public bool IsFloorCodeValid(string code)
        {
            // Checks if the code is valid.
            if(floorCodes.Contains(code))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Gets the floor code by the ID.
        public string GetFloorCodeById(int id)
        {
            // If the ID is valid.
            if(id >= 0 && id < floorCodes.Length)
            {
                return floorCodes[id];
            }
            else
            {
                return string.Empty;
            }
        }

        // Returns the debug floor code.
        public string GetDebugFloorCode()
        {
            return floorCodes[0];
        }


        // FLOOR ID
        // Gets the floor ID by the code. An ID of -1 means the code is invalid.
        public int GetFloorIdByCode(string code)
        {
            return Array.IndexOf(floorCodes, code);
        }

        // Generates a floor element.
        private FloorElementInfo GenerateFloorElementInfo(FloorEntity.entityGroup group, string code)
        {
            // The element info.
            FloorElementInfo elementInfo = new FloorElementInfo();

            // Sets the group.
            elementInfo.group = group;

            // The code length is wrong.
            if (code.Length != 3)
            {
                Debug.LogError("The code is the wrong length. Codes are 3 chars long (double digit number followed by a letter).");

                // Default values.
                elementInfo.id = -1;
                elementInfo.version = 'A';

                return elementInfo;
            }
                

            // Gets the code in all uppercase (makes letter identifier uppercase).
            string codeUpper = code.ToUpper();

            // Sets the ID and type.
            elementInfo.id = int.Parse(codeUpper.Substring(0, 2));
            elementInfo.version = codeUpper[2];

            // Return the element info.
            return elementInfo;
        }


        // INSTANTIATING OBJECTS //
        // Gets the geometry element.
        public Block InstantiateGeometryElement(string code)
        {
            // The element info.
            FloorElementInfo elementInfo = GenerateFloorElementInfo(FloorEntity.entityGroup.geometry, code);

            // The geometry entity
            Block geoEntity;

            // Instantiates the geometry object.
            switch (elementInfo.id)
            {
                case 0:
                default:
                    geoEntity = null;
                    break;

                case 1:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g01A);
                            break;
                    }
                    
                    break;

                case 2:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g02A);
                            break;
                    }
                    break;

                case 3:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g03A);
                            break;
                    }

                    break;

                case 4:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g04A);
                            break;
                    }
                    break;

                case 5:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g05A);
                            break;
                    }

                    break;

                case 6:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g06A);
                            break;
                    }

                    break;

                case 7:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g07A);
                            break;
                    }

                    break;

                case 8:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g08A);
                            break;

                        case 'B':
                        case 'b':
                            geoEntity = Instantiate(g08B);
                            break;
                    }

                    break;

                case 9:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            geoEntity = Instantiate(g09A);
                            break;
                    }

                    break;
            }

            // Sets the values.
            if (autoSetIdInfo && geoEntity != null)
            {
                geoEntity.idNumber = elementInfo.id;
                geoEntity.version = elementInfo.version;
            }

            return geoEntity;
        }

        // Gets the enemy element.
        public Enemy InstantiateEnemyElement(string code)
        {
            // The element info.
            FloorElementInfo elementInfo = GenerateFloorElementInfo(FloorEntity.entityGroup.enemy, code);

            // The enemy entity
            Enemy emyEntity;

            // Instantiates the enemy object.
            switch (elementInfo.id)
            {

                case 0:
                default:
                    emyEntity = null;
                    break;

                case 1:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                            emyEntity = Instantiate(e01A);
                            break;

                        default:
                            emyEntity = null;
                            break;
                    }

                    break;

                case 2:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                            emyEntity = Instantiate(e02A);
                            break;

                        default:
                            emyEntity = null;
                            break;
                    }

                    break;

                case 3:
                    // Version
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                            emyEntity = Instantiate(e03A);
                            break;

                        default:
                            emyEntity = null;
                            break;
                    }

                    break;
            }

            // Sets the values.
            if(autoSetIdInfo && emyEntity != null)
            {
                emyEntity.idNumber = elementInfo.id;
                emyEntity.version = elementInfo.version;
            }            

            return emyEntity;
        }

        // Gets the item element.
        public Item InstantiateItemElement(string code)
        {
            // The element info.
            FloorElementInfo elementInfo = GenerateFloorElementInfo(FloorEntity.entityGroup.item, code);

            // The item entity
            Item itmEntity;

            // Instantiates the item object.
            switch (elementInfo.id)
            {
                case 0:
                default:
                    // Type
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            itmEntity = null;
                            break;
                    }
                    
                    break;

                case 1:
                    // Type
                    switch (elementInfo.version)
                    {
                        case 'A':
                        case 'a':
                        default:
                            itmEntity = Instantiate(i01A);
                            break;
                    }

                    break;
            }

            // Sets the values
            if(autoSetIdInfo && itmEntity != null)
            {
                itmEntity.idNumber = elementInfo.id;
                itmEntity.version = elementInfo.version;
            }

            return itmEntity;
        }


        // GETTING FLOOR DATA //
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

        // Gets the floor using the provided code.
        public Floor GetFloor(string code)
        {
            // Gets the index.
            int index = Array.IndexOf(floorCodes, code);

            // Checks the value of the index.
            if(index == -1) // Value not found.
            {
                return null;
            }
            else
            {
                // The ID is the same as the index, with floor 0 having index 0.
                return GetFloor(index);
            }
        }

        // Floor 00
        public Floor GetFloor00()
        {
            // Template
            // {{ "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
            // };

            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 0;
            floor.code = floorCodes[floor.id];

            // Geometry
            string[,] geometry = new string[FLOOR_COLS, FLOOR_ROWS]{
                {"01A", "03A", "05A", "00A", "00A", "00A", "00A", "00A", "00A", "03A"},
                {"03A", "00A", "03A", "00A", "05A", "00A", "00A", "00A", "00A", "00A"},
                {"03A", "03A", "03A", "03A", "03A", "03A", "04A", "00A", "00A", "00A"},
                {"07A", "00A", "02A", "00A", "06A", "08A", "00A", "00A", "03A", "00A"},
                {"00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A"},
                {"00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                {"00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A", "00A"},
                {"00A", "03A", "03A", "00A", "00A", "00A", "03A", "00A", "03A", "00A"},
                {"00A", "07A", "03A", "09A", "00A", "00A", "03A", "03A", "03A", "00A"},
                {"03A", "00A", "08A", "08B", "08A", "00A", "00A", "00A", "00A", "03A"}};
                      
            floor.geometry = geometry;


            // Enemies
            string[,] enemies = new string[FLOOR_COLS, FLOOR_ROWS] {
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "02A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "02A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.enemies = enemies;


            // Items
            string[,] items = new string[FLOOR_COLS, FLOOR_ROWS] {
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
            string[,] geometry = new string[FLOOR_COLS, FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "02A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A", "03A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "03A", "03A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "03A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.geometry = geometry;

            // Enemies
            string[,] enemies = new string[FLOOR_COLS, FLOOR_ROWS] {
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.enemies = enemies;

            // Items
            string[,] items = new string[FLOOR_COLS, FLOOR_ROWS] {
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
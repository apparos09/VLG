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

        // The skybox ID.
        public int skyboxId;

        // The BGM ID.
        public int bgmId;

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


        // The number of floors (includes the debug floor/floor 0)
        public const int FLOOR_COUNT = 2;

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
        private string[] floorCodes = new string[FLOOR_COUNT]
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
        public Block g02A; // None
        public Block g02B; // Key
        public Block g02C; // Enemy
        public Block g02D; // Button
        public Block g02E; // Boss

        // Block
        [Header("Geometry/Blocks")]
        public Block g03A;

        // Hazard
        [Header("Geometry/Hazard")]
        public Block g04A; // Active
        public Block g04B; // Inactive

        // Limited
        [Header("Geometry/Limited")]
        public Block g05A; // 2 Uses
        public Block g05B; // 4 Uses
        public Block g05C; // 6 Uses
        public Block g05D; // 11 Uses
        public Block g05E; // 13 Uses
        public Block g05F; // 16 Uses

        // Phase
        [Header("Geometry/Phase")]
        public Block g06A; // Active
        public Block g06B; // Inactive

        [Header("Geometry/Portal")]
        public Block g07A; // Purple
        public Block g07B; // Red
        public Block g07C; // Blue
        public Block g07D; // Yellow
        public Block g07E; // Green

        [Header("Geometry/Switch")]
        public Block g08A; // On
        public Block g08B; // Off

        [Header("Geometry/Button")]
        public Block g09A; // Default
        public Block g09B; // Goal
        public Block g09C; // Hazard
        public Block g09D; // Phase
        public Block g09E; // Portal

        // Enemies (E-Group)
        [Header("Enemies")]
        public Enemy e01A;
        public Enemy e02A;
        public Enemy e02B;
        public Enemy e03A;

        // Items (I-Group)
        [Header("Items")]
        public Item i01A;

        // Skyboxes
        [Header("Skyboxes")]
        public Material skyboxMat00;

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

                        case 'B':
                        case 'b':
                            geoEntity = Instantiate(g02B);
                            break;

                        case 'C':
                        case 'c':
                            geoEntity = Instantiate(g02C);
                            break;

                        case 'D':
                        case 'd':
                            geoEntity = Instantiate(g02D);
                            break;

                        case 'E':
                        case 'e':
                            geoEntity = Instantiate(g02E);
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

                        case 'B':
                        case 'b':
                            geoEntity = Instantiate(g04B);
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

                        case 'B':
                        case 'b':
                            geoEntity = Instantiate(g07B);
                            break;

                        case 'C':
                        case 'c':
                            geoEntity = Instantiate(g07C);
                            break;

                        case 'D':
                        case 'd':
                            geoEntity = Instantiate(g07D);
                            break;

                        case 'E':
                        case 'e':
                            geoEntity = Instantiate(g07E);
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

                        case 'B':
                        case 'b':
                            geoEntity = Instantiate(g09B);
                            break;

                        case 'C':
                        case 'c':
                            geoEntity = Instantiate(g09C);
                            break;

                        case 'D':
                        case 'd':
                            geoEntity = Instantiate(g09D);
                            break;

                        case 'E':
                        case 'e':
                            geoEntity = Instantiate(g09E);
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

                        case 'B':
                        case 'b':
                            emyEntity = Instantiate(e02B);
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


        // Sets the skybox using the provided ID.
        public void SetSkybox(int skyboxId)
        {
            switch(skyboxId)
            {
                case 0: // Debug Skybox
                default:
                    RenderSettings.skybox = skyboxMat00;
                    break;
            }
        }

        // Sets the skybox ID.
        public void SetSkybox(Floor floor)
        {
            SetSkybox(floor.skyboxId);
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
                {"07A", "00A", "02B", "00A", "06A", "08A", "00A", "00A", "03A", "00A"},
                {"00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A"},
                {"00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                {"00A", "00A", "09E", "00A", "00A", "00A", "03A", "03A", "03A", "00A"},
                {"00A", "03A", "03A", "00A", "00A", "00A", "03A", "00A", "03A", "00A"},
                {"00A", "07A", "03A", "09D", "00A", "00A", "03A", "03A", "03A", "00A"},
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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "02B", "00A"},
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

            // Skybox
            floor.skyboxId = 0;

            // BGM
            floor.bgmId = 0;

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

            // Skybox
            floor.skyboxId = 0;

            // BGM
            floor.bgmId = 0;

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
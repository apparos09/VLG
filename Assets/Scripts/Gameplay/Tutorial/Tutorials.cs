using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using util;
using static Unity.Collections.AllocatorManager;

namespace VLG
{
    // The tutorial object.
    public class Tutorials : MonoBehaviour
    {
        // The tutorial types.
        public enum tutorialType 
        {
            none, intro, 
            entryBlock, goalBlock, block, hazardBlock, limitedBlock, phaseBlock, portalBlock, switchBlock, buttonBlock,
            stationaryEnemy, barEnemy, copyEnemy, finalBossEnemy,
            keyItem, weaponItem

        };

        // The singleton instance.
        private static Tutorials instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The game manager.
        public GameplayManager gameManager;

        // The tutorials UI.
        public TutorialsUI tutorialsUI; 

        // The tutorial type count.
        public const int TUTORIAL_TYPE_COUNT = 17;

        // The cleared tutorials.
        public List<tutorialType> clearedTutorials = new List<tutorialType>();

        // If 'true', the tutorials object constantly checks for starting tutorials.
        [Tooltip("Constant check for tutorial start.")]
        public bool constantTutorialStartCheck = true;

        // Constructor
        private Tutorials()
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
            // Gets the game manager object.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Gets the tutorials object.
            if (tutorialsUI == null)
                tutorialsUI = TutorialsUI.Instance;
        }

        // Gets the instance.
        public static Tutorials Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<Tutorials>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Tutorial (singleton)");
                        instance = go.AddComponent<Tutorials>();
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

        // Checks if a tutorial is running.
        public bool IsTutorialRunning()
        {
            return tutorialsUI.IsTutorialRunning();
        }

        // Starts the tutorial.
        public void StartTutorial()
        {
            tutorialsUI.StartTutorial();
        }

        // Called when a tutorial is started.
        public void OnTutorialStart()
        {
            // UI start function.
            tutorialsUI.OnTutorialStart();

            // Freeze the game.
            Time.timeScale = 0.0F;
        }

        // Called when a tutorail ends.
        public void OnTutorialEnd()
        {
            // UI end function.
            tutorialsUI.OnTutorialEnd();

            // Unfreeze the game if the game is not paused.
            if(!gameManager.IsPaused())
                Time.timeScale = 1.0F;
        }



        // CLEARED TUTORIAL
        // Checks if the provided tutorial has been cleared.
        public bool IsTutorialCleared(tutorialType tutorial)
        {
            return clearedTutorials.Contains(tutorial);
        }

        // Adds a cleared tutorial to the list.
        public void AddClearedTutorial(tutorialType tutorial)
        {
            // If it's not in the list, add it
            if(!clearedTutorials.Contains(tutorial))
                clearedTutorials.Add(tutorial); 
        }

        // Adds cleared tutorials to the list.
        public void AddClearedTutorials(List<tutorialType> trlList, bool clearList)
        {
            // Clears the tutorial list.
            if (clearList)
                clearedTutorials.Clear();

            // Adds all elements aside from duplicates.
            foreach(tutorialType trl in trlList)
            {
                AddClearedTutorial(trl);
            }
        }

        // Generates the cleared tutorials array.
        public bool[] GenerateClearedTutorialsArray()
        {
            // Creates the array.
            bool[] arr = new bool[TUTORIAL_TYPE_COUNT];

            // Fill the array.
            FillClearedTutorialsArray(ref arr);

            // Returns the array.
            return arr;
        }

        // Fills a bool array with cleared tutorials values. 
        public void FillClearedTutorialsArray(ref bool[] arr)
        {
            // Goes through the cleared tutorials list
            foreach(tutorialType trl in clearedTutorials)
            {
                // Convert the value.
                int index = (int)trl;

                // Index is valid.
                if(index >= 0 && index < arr.Length)
                {
                    // This value has been cleared.
                    arr[index] = true;
                }

            }
        }

        // Sets the cleared tutorials with the bool array.
        // If the array is a different length than the total amount of types...
        // The rest of the types are ignored.
        public void AddClearedTutorials(bool[] arr, bool clearList)
        {
            // Clears the tutorial list.
            if (clearList)
                clearedTutorials.Clear();

            // Goes through each index in the array.
            for(int i = 0; i < arr.Length && i < TUTORIAL_TYPE_COUNT; i++)
            {
                // Converts the index.
                tutorialType tutorial = (tutorialType)i;

                // If the tutorial has been cleared, add it to the list.
                if (arr[i] && !clearedTutorials.Contains(tutorial))
                {
                    // Adds the tutorial to the list.
                    clearedTutorials.Add(tutorial);
                }
            }
        }

        // Tutorial Loader

        // Loads the tutorial
        private void LoadTutorial(ref List<Page> pages)
        {
            // Loads pages for the tutorial.
            tutorialsUI.LoadPages(ref pages, false);
        }

        // Loads the tutorial of the provided type.
        public void LoadTutorial(tutorialType tutorial)
        {
            // Checks the tutorial type to see what to load.
            switch(tutorial)
            {
                default:
                case tutorialType.none: // No behaviour.
                    break;

                    // OTHER/GENERAL
                case tutorialType.intro:
                    LoadIntroTutorial();
                    break;

                    // GEOMETRY
                case tutorialType.entryBlock:
                    LoadEntryBlockTutorial();
                    break;

                case tutorialType.goalBlock:
                    LoadGoalBlockTutorial();
                    break;

                case tutorialType.block:
                    LoadBlockTutorial();
                    break;

                case tutorialType.hazardBlock:
                    LoadHazardBlockTutorial();
                    break;

                case tutorialType.limitedBlock:
                    LoadLimitedBlockTutorial();
                    break;

                case tutorialType.phaseBlock:
                    LoadPhaseBlockTutorial();
                    break;

                case tutorialType.portalBlock:
                    LoadPortalBlockTutorial();
                    break;

                case tutorialType.switchBlock:
                    LoadSwitchBlockTutorial();
                    break;

                case tutorialType.buttonBlock:
                    LoadButtonBlockTutorial();
                    break;


                    // ENEMY
                case tutorialType.stationaryEnemy:
                    LoadStationaryEnemyTutorial();
                    break;

                case tutorialType.barEnemy:
                    LoadBarEnemyTutorial();
                    break;

                case tutorialType.copyEnemy:
                    LoadCopyEnemyTutorial();
                    break;

                case tutorialType.finalBossEnemy:
                    LoadFinalBossTutorial();
                    break;

                    // ITEM
                case tutorialType.keyItem:
                    LoadKeyItemTutorial();
                    break;

                case tutorialType.weaponItem:
                    LoadWeaponItemTutorial();
                    break;
            }
        }


        // Load the tutorial (template)
        private void LoadTutorialTemplate()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("Insert text here.")
            };

            // Change the display image when certain pages are opened using callbacks.

            // Loads the tutorial.
            LoadTutorial(ref pages);
        }


        // TUTORIAL LOADS //
        // Loads the intro tutorial
        public void LoadIntroTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("Welcome to <b>Tribulation Tower</b>! Your task is to ascend the tower and challenge the monster that rests at the top."),
                new Page("Use the <b>WASD keys</b> to move across the floor, and the <b>space bar</b> to attack. You always attack in the direction that you’re facing, and the attack covers one block in front of you. To change directions, use the <b>arrow keys</b>."),
                new Page("You can use the <b>0 key</b> to change the camera view, the <b>R key</b> to reset the floor, and the <b>P key</b> to pause/unpause the game."),
                new Page("There are 50 floors in the tower, and the game autosaves each time a floor is completed. Good luck!")
            };

            // Change the display image when certain pages are opened using callbacks.
            // No image to display, so just hide the diagram.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.HideDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ClearDiagram);
            pages[3].OnPageOpenedAddCallback(tutorialsUI.ClearDiagram);
            pages[3].OnPageOpenedAddCallback(tutorialsUI.HideDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.intro);

        }

        // Geometry
        // Entry Block
        public void LoadEntryBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is an <b>Entry Block</b>! You always start on this block when a floor begins, or when it’s reset.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToEntryBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.entryBlock);
        }

        // Goal Block
        public void LoadGoalBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Goal Block</b>! To complete a floor, you must reach the goal. If the goal isn’t visible, then it must be unlocked."),
                new Page("The way to unlock the goal is shown as part of the floor objective at the top of the screen.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToGoalBlockDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToGoalBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.goalBlock);
        }

        // Block
        public void LoadBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Block</b>! You and some other entities use it to get around the floor.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.block);
        }

        // Hazard Block
        public void LoadHazardBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Hazard Block</b>! If you step on it while the hazard is active, you will be destroyed. Entities vulnerable to the hazard will also be destroyed if they step on it.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToHazardBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.hazardBlock);
        }

        // Limited Block
        public void LoadLimitedBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Limited Block</b>! Limited blocks can only be used a certain number of times, which is displayed on the top of each block."),
                new Page("If a limited block is used when there are no more uses left, the block breaks, and the entity using the block is destroyed.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToLimitedBlockDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToLimitedBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.limitedBlock);
        }

        // Phase Block
        public void LoadPhaseBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Phase Block</b>! When a phase block is intangible (transparent), it cannot be used. If a phase block becomes intangible when a vulnerable entity is using it, the entity will be destroyed.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToPhaseBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.phaseBlock);
        }

        // Portal Block
        public void LoadPortalBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("These are <b>Portal Blocks</b>! Portals are used to travel to different parts of the floor, with each portal colour being connected to a different portal of the same colour."),
                new Page("If a portal block isn’t active, its portal cannot be used to teleport. However, it can still be used as a regular block in such a state.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToPortalBlockDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToPortalBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.portalBlock);
        }

        // Switch Block
        public void LoadSwitchBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("These are <b>Switch Blocks</b>! When the red block is active, the blue block is inactive, and vice versa. If an entity is standing on a switch block when it switches off, the entity is destroyed."),
                new Page("To switch the state of all switch blocks on a floor, perform an attack.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToSwitchBlockDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToSwitchBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.switchBlock);
        }

        // Button Block
        public void LoadButtonBlockTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("These are <b>Button Blocks</b>! These blocks change elements on the floor when an applicable entity presses their buttons."),
                new Page("<b>Yellow button blocks</b> unlock the goal."),
                new Page("<b>Blue button blocks</b> turn on/off the hazards on hazard blocks."),
                new Page("<b>Green button blocks</b> change the state of phase blocks, making them go from tangible to intangible, or vice versa."),
                new Page("<b>Purple button blocks</b> turn on/off all the portals on the current floor.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToButtonBlockDiagram);
            pages[4].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[4].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToButtonBlockDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.buttonBlock);
        }

        // Enemies
        // Stationary Enemy
        public void LoadStationaryEnemyTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Blue Dramini</b>! It will stay in place and act as an obstacle. If you collide with a Blue Dramini, you’ll be destroyed. To destroy a Blue Dramini, just attack it.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToStationaryEnemyDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.stationaryEnemy);
        }

        // Bar Enemy
        public void LoadBarEnemyTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Red Dramini</b>! It has flaming bars that rotate every time you jump to another block. If you touch the bars, you’ll be destroyed. To destroy a Red Dramini, just attack it.")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToBarEnemyDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.barEnemy);
        }

        // Copy Enemy
        public void LoadCopyEnemyTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("These are <b>Mimics</b>! <b>Red Mimics</b> copy your movements, while <b>Blue Mimics</b> do the opposite of your movements. Mimics only copy your movements, and not your attacks."),
                new Page("If you collide with a Mimic, you will be destroyed. To destroy a Mimic, simply attack it.")
            };

            // Change the display image when certain pages are opened using callbacks.
            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToCopyEnemyDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[1].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToCopyEnemyDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.copyEnemy);
        }

        // Final Boss Enemy
        public void LoadFinalBossTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("It’s the <b>dastardly dragon</b>, the boss of the tower! This ancient evil will attack you with lightning strikes and homing lasers. Dodge its attacks and you’ll get a chance to strike back!")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToFinalBossEnemyDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.finalBossEnemy);
        }

        // Items
        // Key
        public void LoadKeyItemTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Key</b>! Collect all the keys on the floor to unlock the goal!")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToKeyItemDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.keyItem);
        }

        // Weapon
        public void LoadWeaponItemTutorial()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>
            {
                // Load the pages.
                new Page("This is a <b>Weapon</b>! When a weapon is present on a floor, it means that you have lost your weapon! Without a weapon, you can’t attack! Collect a weapon to regain the ability to attack!")
            };

            // Change the display image when certain pages are opened using callbacks.
            pages[0].OnPageOpenedAddCallback(tutorialsUI.ShowDiagram);
            pages[0].OnPageOpenedAddCallback(tutorialsUI.SetDiagramToWeaponItemDiagram);

            // Loads the tutorial, and adds it to the cleared list.
            LoadTutorial(ref pages);
            AddClearedTutorial(tutorialType.weaponItem);
        }

        // Update is called once per frame
        void Update()
        {
            // If the tutorial start check should be constant.
            if(constantTutorialStartCheck)
            {
                // If a tutorial is not currently running.
                if(!IsTutorialRunning())
                {
                    // If the game manager is using tutorials, and is not paused.
                    if (gameManager.UsingTutorials && !gameManager.IsPaused())
                    {
                        // If there are pages to display, start the tutorial.
                        if (tutorialsUI.textBox.pages.Count > 0)
                            StartTutorial();
                    }
                }
                
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
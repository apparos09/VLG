using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VLG
{
    // The gameplay manager.
    public class GameplayManager : MonoBehaviour
    {
        // The singleton instance.
        private static GameplayManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The gameplay  camera.
        public GameplayCamera gameCamera;

        // The gameplay UI.
        public GameplayUI gameUI;

        // The gameplay audio.
        public GameplayAudio gameAudio;

        // The tutorials.
        public Tutorials tutorials;

        // The player for the game.
        public Player player;

        // The floor manager.
        public FloorManager floorManager;

        // The floor loading screen.
        public FloorLoadingScreen floorLoadingScreen;

        // If 'true', the floors are loaded using coroutines.
        // If 'false', the direct function is called.
        [HideInInspector]
        public bool useFloorCoroutine = true;

        // The number of floors the player will do. This is used for testing purposes.
        [Tooltip("The number of floors the player will do. This is for testing purposes.")]
        public int floorCount = 0;

        // Gets set to 'true', when the game is completed.
        private bool gameComplete = false;

        // Gets set to 'true' when the post start function has been called.
        private bool calledPostStart = false;

        [Header("Stats")]

        // The game time (total)
        public float gameTime = 0;

        // The floor times (total)
        public float[] floorTimes;

        // The game turns (total)
        public int gameTurns = 0;

        // The floor turns (total)
        public int[] floorTurns;

        // Determines if the game is paused or not.
        private bool paused = false;

        // Constructor
        private GameplayManager()
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
                // Initializes arrays.
                // This is done here so that the inspector doesn't override these values.
                floorTimes = new float[FloorData.FLOOR_COUNT_MAX];
                floorTurns = new int[FloorData.FLOOR_COUNT_MAX];

                // If the floor count is 0, set it to the max.
                if (floorCount <= 0)
                    floorCount = FloorData.FLOOR_COUNT_MAX;

                instanced = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // TODO: comment out when you want to test saving.
            // If the game isn't running in WebGL, allow the game to save.
            // This is already done in the InitScene, but it's done again here.
            // SaveSystem.Instance.allowSaveLoad = false;
            SaveSystem.Instance.allowSaveLoad = Application.platform != RuntimePlatform.WebGLPlayer;

            // Finds the game info object.
            // Ignores disabled GameInfo objects so that debug stages can be tested.
            GameInfo gameInfo = FindObjectOfType<GameInfo>(false);

            // If the game info object couldn't be found...
            if(gameInfo != null)
            {
                // Set to the floor count.
                floorCount = gameInfo.floorCount;

                // If 0 or negative, set it to the max.
                if (floorCount <= 0)
                    floorCount = FloorData.FLOOR_COUNT_MAX;

                // Checks if saved data should be loaded.
                if (gameInfo.loadFromSave) // Load from save.
                {
                    // Loads the saved game. If the load failed, load from the game info floor ID.
                    if(!LoadGame())
                    {
                        // Checks if a coroutine should be used or not.
                        if(useFloorCoroutine)
                            floorManager.GenerateFloorAsCoroutine(gameInfo.floorId);
                        else
                            floorManager.GenerateFloor(gameInfo.floorId);
                    }
                        
                }
                else // Start from applicable floor.
                {
                    // Checks if a coroutine should be used or not.
                    if (useFloorCoroutine)
                        floorManager.GenerateFloorAsCoroutine(gameInfo.floorId);
                    else
                        floorManager.GenerateFloor(gameInfo.floorId);
                }

                // Destroy the game object now that it's done being used.
                Destroy(gameInfo.gameObject);
            }
            else // Default load.
            {
                // TODO: change default floor load to floor 0, not floor one.
                
                // Checks if a coroutine should be used or not.
                if (useFloorCoroutine)
                    floorManager.GenerateFloorAsCoroutine(0);
                else
                    floorManager.GenerateFloor(0);
            }
        }

        // Called after the Start function 
        protected void PostStart()
        {
            // For some reason, when going back to the title screen and continuing the game...
            // The game will be paused by default. This automatically unpauses the game.
            // I don't know why it happens, but this fix seems to work.
            SetPaused(false);

            // TODO: uncomment when ready to test the tutorial.
            // INTRO TUTORIAL
            // If tutorials are being used.
            if (UsingTutorials)
            {
                // If the intro has not been cleared, trigger the intro.
                if (!tutorials.IsTutorialCleared(Tutorials.tutorialType.intro))
                {
                    LoadIntroTutorial();
                }
            }

            // This function has been called.
            calledPostStart = true;
        }

        // Gets the instance.
        public static GameplayManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<GameplayManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Gameplay Manager (singleton)");
                        instance = go.AddComponent<GameplayManager>();
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


        // PAUSE

        // Returns 'true 'if the game is paused.
        public bool IsPaused()
        {
            return paused;
        }

        // Sets that the game is paused
        public void SetPaused(bool pausedGame)
        {
            // Sets the value.
            paused = pausedGame;

            // Checks if the time scale should be changed.
            bool changeTimeScale = true;

            // If tutorials are being used.
            if(UsingTutorials)
            {
                // If a tutorial is running, don't change the time scale.
                changeTimeScale = !tutorials.IsTutorialRunning();
            }

            // Checks if the game is paused or unpaused.
            if(paused)
            {
                // Stops time.
                if(changeTimeScale)
                    Time.timeScale = 0.0F;
            }
            else
            {
                // Resumes normal time.
                if(changeTimeScale)
                    Time.timeScale = 1.0F;
            }

            // Called to update on the game's paused event.
            gameUI.OnPausedChanged(paused);
        }

        // Pauses the game.
        public void PauseGame()
        {
            SetPaused(true);
        }

        // Unpauses the game.
        public void UnpauseGame()
        {
            SetPaused(false);
        }

        // Toggles the paused value.
        public void TogglePaused()
        {
            SetPaused(!paused);
        }

        // TUTORIAL
        // If the tutorial is being used or not.
        public bool UsingTutorials
        {
            get
            {
                return GameSettings.Instance.useTutorials;
            }

            set
            {
                GameSettings.Instance.useTutorials = value;
            }
        }

        // Checks if a tutorial is running.
        public bool IsTutorialRunning()
        {
            return tutorials.IsTutorialRunning();
        }

        // Loads the intro tutorial.
        public void LoadIntroTutorial()
        {
            // Gets the current list of pages.
            List<util.Page> currPages = new List<util.Page>();

            // If the pages count is greater than 0.
            if (tutorials.tutorialsUI.textBox.pages.Count > 0)
            {
                // Get the current pages.
                currPages = tutorials.tutorialsUI.textBox.pages;

                // Clear the current pages.
                tutorials.tutorialsUI.textBox.ClearPages();
            }

            // Load the intro tutorial.
            tutorials.LoadIntroTutorial();

            // Put back the old pages. This makes sure that the intro tutorial plays first.
            if (currPages.Count > 0)
                tutorials.tutorialsUI.textBox.pages.AddRange(currPages);

            // Make sure to start at page 0 since the intro tutorial has been loaded.
            tutorials.tutorialsUI.textBox.SetPage(0);
        }

        // UPDATE TURNS
        // Updates game turns.
        public void UpdateTurns()
        {
            // If the game is paused, don't updatre the turns.
            if (paused)
                return;

            // TODO: implement turn update.
            gameTurns++;

            floorManager.UpdateTurns();
            gameUI.UpdateTurnsText();
        }


        // SAVING/LOADING

        // Returns 'true' if data can be saved/loaded.
        public bool IsSavingLoadingEnabled()
        {
            // Check if the save system is instantiated.
            if(SaveSystem.Instantiated)
            {
                // If saving/loading is allowed, return true. If not, return false.
                if (SaveSystem.Instance.allowSaveLoad)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }


        // Generate Save Data
        public VLG_GameData GenerateSaveData()
        {
            // The game data.
            VLG_GameData data = new VLG_GameData();

            // Valid is changed last in this function.

            // Checks if the game is complete.
            data.gameComplete = IsGameComplete();

            // Saves the floor count.
            data.floorCount = floorCount;

            // Saves the floor id and code.
            data.floorId = floorManager.currFloor.id;
            data.floorCode = floorManager.currFloor.code;

            // Saves the time.
            data.gameTime = gameTime;
            data.floorTimes = floorTimes;

            // Saves the turns.
            data.gameTurns = gameTurns;
            data.floorTurns = floorTurns;

            // Fill the cleared tutorials array.
            tutorials.FillClearedTutorialsArray(ref data.clearedTutorials);

            // The data is safe to read from.
            data.valid = true;

            // Returns the data.
            return data;
        }

        // Saves the game.
        public bool SaveGame()
        {
            // Grab the save system (creates it if it doesn't exist).
            SaveSystem saveSystem = SaveSystem.Instance;

            // Sets the game manager.
            saveSystem.gameManager = this;

            // Called when a save has been started.
            gameUI.OnSaveStarted();

            // Saves the game asynchronously.
            bool result = saveSystem.SaveGame(true);

            // If the save was successful, set the last save as the loaded data.
            // This makes sure the player can go to the title screen and start...
            // The game back up again as normal. It doesn't check for a successful...
            // Result, but this should be fine.
            saveSystem.SetLastSaveAsLoadedData();

            // Return the result of the save.
            return result;
        }

        // Loads the game.
        public bool LoadGame()
        {
            // If the save system hasn't been instantiated, there's no data to load.
            if (!SaveSystem.Instantiated)
            {
                Debug.LogError("The save system has not been instantiated. Load failed.");
                return false;
            }
            
            // Grab the save system.
            SaveSystem saveSystem = SaveSystem.Instance;

            // If the save system doesn't have loaded data, try to load it.
            if(!saveSystem.HasLoadedData())
            {
                // Tries to load the game.
                saveSystem.LoadGame();

                // If the game load failed, don't do anything else.
                if (!saveSystem.HasLoadedData())
                {
                    Debug.LogError("No loaded data could be found.");
                    return false;
                }

            }

            // Grabs the loaded data.
            VLG_GameData data = saveSystem.loadedData;

            // Data validity check.
            if(!data.valid)
            {
                Debug.LogError("The loaded data is invalid.");
                return false;
            }

            // If the game is completed, ignore the save data.
            if(data.gameComplete)
            {
                // Message to show that the game is complete.
                if (Application.platform.IsEditor())
                    Debug.LogWarning("The continued game was completed, so the game is restarting...");

                return false;
            }

            // These values should both be false regardless, but for clarity, this value is overwritten anyway.
            // In practice, this line shouldn't change anything.
            gameComplete = data.gameComplete;

            // Loads the floor count.
            floorCount = data.floorCount;

            // Loads the time data.
            gameTime = data.gameTime;
            floorTimes = data.floorTimes;

            // Loads the floor data.
            gameTurns = data.gameTurns;
            floorTurns = data.floorTurns;

            // Add the cleared tutorials.
            tutorials.AddClearedTutorials(data.clearedTutorials, true);

            // Loads the floor using the ID number (could also use code).
            floorManager.GenerateFloor(data.floorId);

            // Load successful.
            return true;
        }

        // Returns 'true' if the game is complete, false otherwise.
        public bool IsGameComplete()
        {
            return gameComplete;
        }

        // Called to finish the game.
        public void FinishGame()
        {
            // The game has been finished, meaning that it is complete.
            gameComplete = true;

            // Creates the results info object.
            GameObject newObject = new GameObject("Results Info");
            ResultsInfo resultsInfo = newObject.AddComponent<ResultsInfo>();
            DontDestroyOnLoad(newObject);

            // Saving data.
            // Game time and turns.
            // NOTE: this is the number of turns (moves) it took to reach the goal.
            // As such, it will be one less what's displayed, since what's displayed is...
            // The current move. e.g., current move 3 = 2 moves have already been made. 
            resultsInfo.gameTime = gameTime;
            resultsInfo.gameTurns = gameTurns;

            // Stores the floor times and floor turns.
            resultsInfo.floorTimes = floorTimes;
            resultsInfo.floorTurns = floorTurns;

            // Results scene.
            ToResultsScene();
        }

        // Goes to the title scene.
        public void ToTitleScene()
        {
            // UnpauseGame(); // Make sure the game isn't paused so that the timer is running.
            Time.timeScale = 1.0F; // Reset the time scale.
            SceneManager.LoadScene("TitleScene");
        }


        // Goes to the results scene.
        public void ToResultsScene()
        {
            // UnpauseGame(); // Make sure the game isn't paused so that the timer is running.
            Time.timeScale = 1.0F; // Reset the time scale.
            SceneManager.LoadScene("ResultsScene");
        }

        // Update is called once per frame
        void Update()
        {
            // If PostStart hasn't been called yet, call it.
            if (!calledPostStart)
                PostStart();

            // The game is not paused.
            if(!paused)
            {
                gameTime += Time.deltaTime;
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
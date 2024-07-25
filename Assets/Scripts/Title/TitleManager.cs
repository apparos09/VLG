using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace VLG
{
    // The title manager.
    public class TitleManager : MonoBehaviour
    {
        // The singleton instance.
        private static TitleManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The title UI.
        public TitleUI titleUI;

        // The code input.
        public CodeInput codeInput;

        // The gameplay info object.
        public GameInfo gameInfo;

        // If 'true', button combos are checked.
        private bool checkButtonCombos = true;

        // Gets set to 'true' when a button combo has been successfully pulled off.
        private bool buttonComboSuccess = false;

        // The floor select combination.
        private Queue<KeyCode> floorSelectCombo = new Queue<KeyCode>();

        // Constructor
        private TitleManager()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        void Awake()
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
            // If the game info object is null.
            if(gameInfo == null)
            {
                // Try to find the game info object.
                gameInfo = FindObjectOfType<GameInfo>(true);

                // The game info oobject doesn't exist.
                if(gameInfo == null)
                {
                    // Makes the new object and adds the info component.
                    GameObject newObject = new GameObject("Game Info");
                    gameInfo = newObject.AddComponent<GameInfo>();
                }
            }

            // Keep the game info object around for going to the gameplay scene.
            DontDestroyOnLoad(gameInfo);

            // Adds the floor select combination to the queue.
            floorSelectCombo.Enqueue(KeyCode.F);
            floorSelectCombo.Enqueue(KeyCode.B);
            floorSelectCombo.Enqueue(KeyCode.L);
        }

        // Gets the instance.
        public static TitleManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<TitleManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("TitleManager (singleton)");
                        instance = go.AddComponent<TitleManager>();
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

        // Starts the game.
        public void StartGame()
        {
            // Clamp the floor ID in case its invalid.
            gameInfo.floorId = Mathf.Clamp(gameInfo.floorId, 0, FloorData.FLOOR_COUNT - 1);

            // Go to the game scene.
            ToGameScene();
        }

        // Starts a new game.
        public void StartNewGame()
        {
            // New game.
            gameInfo.loadFromSave = false;

            // Starting floor.
            gameInfo.floorId = 1;

            // Start the game.
            StartGame();
        }

        // Continues the game.
        public void ContinueGame()
        {
            // Grabs the save system.
            SaveSystem saveSystem = SaveSystem.Instance;

            // If there is no save data to load, start a new game.
            gameInfo.loadFromSave = saveSystem.HasLoadedData();
            
            // If the game should be loaded from a save...
            if(gameInfo.loadFromSave) 
            { 
                // Grab the floor ID.
                gameInfo.floorId = saveSystem.loadedData.floorId;
            }
            else
            {
                // Start from floor 1.
                gameInfo.floorId = 1;
            }

            // Start the game.
            StartGame();
        }

        // Goes to the gameplay scene.
        public void ToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        // Quits the application.
        public void QuitApplication()
        {
            Application.Quit();
        }

        // Update is called once per frame
        void Update()
        {
            // TODO: the button combonation needs to have all the buttons held instead of just pressed to work.
            // Try to figure out a less finicky way of doing this.

            // If button combos should be chcked (only happens on the title window).
            if(checkButtonCombos && titleUI.titleWindow.activeSelf)
            {
                // Checks if any keys are being pressed.
                if(Input.anyKey && !buttonComboSuccess)
                {
                    // Creates a temporary queue.
                    Queue<KeyCode> tempQueue = new Queue<KeyCode>(floorSelectCombo);

                    // While there are remaining values.
                    while(tempQueue.Count > 0)
                    {
                        // If the next key has been pressed down.
                        if(Input.GetKey(tempQueue.Peek()))
                        {
                            // Remove the value.
                            tempQueue.Dequeue();

                        }
                        else
                        {
                            // Not right combination.
                            break;
                        }
                    }

                    // The right combination was provided.
                    if(tempQueue.Count == 0)
                    {
                        // Toggle the buttons.
                        titleUI.ToggleCodeAndFloorButtons();

                        // A button combo was successful.
                        buttonComboSuccess = true;

                        // If in the editor, print a success message.
                        if(Application.isEditor)
                        {
                            // The button combination was successful.
                            Debug.Log("Button Combo Success!");
                        }
                    }
                }
                else
                {
                    // If no buttons are being pressed, reset the combo check.
                    if(!Input.anyKey)
                        buttonComboSuccess = false;
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
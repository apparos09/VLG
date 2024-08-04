using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VLG
{
    // The title UI.
    public class TitleUI : MonoBehaviour
    {
        // The singleton instance.
        private static TitleUI instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The title manager.
        public TitleManager titleManager;

        [Header("Title Window")]
        
        // The continue button.
        public Button continueButton;

        // The code button.
        public Button codeWindowButton;

        // The floor button.
        public Button floorWindowButton;

        // The quit button.
        public Button quitButton;

        [Header("Windows")]
        
        // The title window.
        public GameObject titleWindow;

        // The story window.
        public GameObject storyWindow;

        // The instructions window.
        public GameObject instructionsWindow;

        // The code window.
        public GameObject codeWindow;

        // The floor window.
        public GameObject floorWindow;

        // The settings window.
        public GameObject settingsWindow;

        // The liscences window.
        public GameObject licensesWindow;

        // Constructor
        private TitleUI()
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

                // Gets the manager.
                if (titleManager == null)
                    titleManager = TitleManager.Instance;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Checks the game platform being run.
            if(Application.platform == RuntimePlatform.WebGLPlayer) // WebGL
            {
                // Can't save in WebGL, so disable the continue button.
                continueButton.interactable = false;

                // Can't quit in WebGL, so disable the quit button.
                quitButton.interactable = false;
            }
            else
            {
                // If the save system has loaded data, enable the continue button.
                continueButton.interactable = SaveSystem.Instance.HasLoadedData();
            }
        }

        // Gets the instance.
        public static TitleUI Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<TitleUI>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("TitleUIManager (singleton)");
                        instance = go.AddComponent<TitleUI>();
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

        // Opens the given window.
        public void OpenWindow(GameObject window)
        {
            CloseAllWindows();
            window.SetActive(true);
        }

        // Closes the given window.
        public void CloseWindow(GameObject window)
        {
            window.SetActive(false);
        }

        // Closes all windows.
        public void CloseAllWindows()
        {
            titleWindow.SetActive(false);
            storyWindow.SetActive(false);
            instructionsWindow.SetActive(false);
            codeWindow.SetActive(false);
            floorWindow.SetActive(false);
            settingsWindow.SetActive(false);
            licensesWindow.SetActive(false);
        }

        // Swpas the code input button and floor button.
        public void ToggleCodeAndFloorButtons()
        {
            // Toggles the code and floor buttons. Only one should be active at a time.
            codeWindowButton.gameObject.SetActive(!codeWindowButton.gameObject.activeSelf);
            floorWindowButton.gameObject.SetActive(!floorWindowButton.gameObject.activeSelf);
        }

        // Starts the game.
        public void StartGame()
        {
            titleManager.StartGame();
        }

        // Starts a new game.
        public void StartNewGame()
        {
            titleManager.StartNewGame();
        }

        // Continues the game.
        public void ContinueGame()
        {
            titleManager.ContinueGame();
        }

        // Quits the application.
        public void QuitApplication()
        {
            titleManager.QuitApplication();
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
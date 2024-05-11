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
            // ...
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
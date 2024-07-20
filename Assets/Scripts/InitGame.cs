using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace VLG
{
    // Initializes the game.
    public class InitGame : MonoBehaviour
    {
        // If 'true', saving/loading is allowed.
        public bool allowSaveLoad = true;

        private void Awake()
        {
            // Frame Rate
            Application.targetFrameRate = 30;
        }

        // Start is called before the first frame update
        void Start()
        {
            // If the game isn't running in the WebGL player, try to load the save data.
            if(Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // Operating in WebGL, so don't allow saving and loading.
                SaveSystem.Instance.allowSaveLoad = false; 
            }
            else
            {
                // Set allowing for saving/loading.
                SaveSystem.Instance.allowSaveLoad = allowSaveLoad;

                // If saving/loading is allowed, load the game.
                if(SaveSystem.Instance.allowSaveLoad)
                {
                    // Load the game data.
                    SaveSystem.Instance.LoadGame();
                }
            }


            // GAME SETTINGS DATA 
            // Loads the default settings.
            GameSettings.Instance.LoadDefaultGameSettingsData();

            // If saving/loading is allowed, load the settings data.
            if (SaveSystem.Instance.allowSaveLoad)
            {
                GameSettings.Instance.LoadGameSettingsDataFromFile();
            }

        }

        // Update is called once per frame
        void Update()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
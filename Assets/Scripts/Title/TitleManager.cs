using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            gameInfo.floorId = Mathf.Clamp(gameInfo.floorId, 0, FloorData.FLOOR_COUNT);

            ToGameScene();
        }

        // Starts a new game.
        public void StartNewGame()
        {
            gameInfo.floorId = 1;
            StartGame();
        }

        // Continues the game.
        public void ContinueGame()
        {
            // TODO: load save data
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

        }
    }
}
using System.Collections;
using System.Collections.Generic;
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

        // The gameplay UI.
        public GameplayUIManager gameUI;

        // The player for the game.
        public Player player;

        // The floor manager.
        public FloorManager floorManager;

        // The game time.
        public float gameTime = 0;

        // TODO: add floor array for moving around entites.

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
                instanced = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

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

        // Call when the goal is entered. 
        public void OnGoalEntered()
        {
            // No floor set, so just finish the game.
            if(floorManager.currFloor == null)
            {
                FinishGame();
            }
            else
            {
                // Gets the next ID.
                int nextFloorId = floorManager.currFloor.id + 1;

                // There are remaining floors.
                if(nextFloorId <= FloorData.FLOOR_COUNT)
                {
                    // Generates the next floor.
                    floorManager.GenerateFloor(nextFloorId);
                }
                else // No other floors.
                {
                    FinishGame();
                }

            }
        }

        // Called to finish game.
        public void FinishGame()
        {
            SceneManager.LoadScene("ResultsScene");
        }

        // Update is called once per frame
        void Update()
        {
            gameTime += Time.deltaTime;
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
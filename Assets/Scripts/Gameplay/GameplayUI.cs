using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using util;

namespace VLG
{
    // The gameplay UI manager.
    public class GameplayUI : MonoBehaviour
    {
        // The singleton instance.
        private static GameplayUI instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The game manager.
        public GameplayManager gameManager;

        // The text that displays the floor.
        public TMP_Text floorText;

        [Header("Progress")]

        // The progress bar for the game.
        public ProgressBar gameProgressBar;

        [Header("Time")]

        // The time text for the whole game.
        public TMP_Text gameTimeText;

        // The time text for the current floor.
        public TMP_Text floorTimeText;

        // Constructor
        private GameplayUI()
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

                // Gets the game manager.
                if (gameManager == null)
                    gameManager = GameplayManager.Instance;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // ...
        }

        // Gets the instance.
        public static GameplayUI Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<GameplayUI>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("GameplayUIManager (singleton)");
                        instance = go.AddComponent<GameplayUI>();
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

        // Updates the floor text.
        public void UpdateFloorText()
        {
            // Gets the floor ID.
            int floorId = gameManager.floorManager.currFloor.id;

            // Sets the floor text.
            floorText.text = "Floor " + floorId.ToString();
        }

        // Updates the game progress.
        public void UpdateGameProgressBar()
        {
            // Gets the clear percent.
            float percent = Mathf.Clamp01(gameManager.floorManager.currFloor.id / FloorData.FLOOR_COUNT);

            // Sets the value.
            gameProgressBar.SetValue(percent, true);
        }

        // Update is called once per frame
        void Update()
        {
            // If the game is not paused.
            if(!gameManager.paused)
            {
                // TODO: maybe change how often time text is updated so that it doesn't happen every frame.

                // Gets the game time and floor time.
                float gt = gameManager.gameTime;
                float ft = gameManager.floorManager.floorTime;

                // Updates the text.
                gameTimeText.text = "GT: " + StringFormatter.FormatTime(gt, false, true, false);
                floorTimeText.text = "FT: " + StringFormatter.FormatTime(ft, false, true, false);
            }
        }
    }
}
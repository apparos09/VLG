using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using util;

namespace VLG
{
    // The results UI.
    public class ResultsUI : MonoBehaviour
    {
        // The singleton instance.
        private static ResultsUI instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The results manager.
        public ResultsManager resultsManager;

        // The game time text.
        public TMP_Text gameTimeText;

        // Constructor
        private ResultsUI()
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
                if (resultsManager == null)
                    resultsManager = ResultsManager.Instance;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // ...
        }

        // Gets the instance.
        public static ResultsUI Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<ResultsUI>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("ResultsUIManager (singleton)");
                        instance = go.AddComponent<ResultsUI>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Loads the results info into the UI.
        public void LoadResultsInfo()
        {
            LoadResultsInfo(resultsManager.resultsInfo);
        }

        // Load the results info for the UI.
        public void LoadResultsInfo(ResultsInfo info)
        {
            // Checks the object to know what to load in.
            if(info != null)
            {
                gameTimeText.text = StringFormatter.FormatTime(info.gameTime, true, true);
            }
            else
            {
                gameTimeText.text = "-";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VLG
{
    // The results manager.
    public class ResultsManager : MonoBehaviour
    {
        // The singleton instance.
        private static ResultsManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The Results UI.
        public ResultsUI resultsUI;

        // The Results Audio.
        public ResultsAudio resultsAudio;

        // The results info
        public ResultsInfo resultsInfo;

        // The floor results entries.
        public List<FloorResultsEntry.FloorResultsEntryInfo> resultsEntries = new List<FloorResultsEntry.FloorResultsEntryInfo>();

        // The index for the floor results entries.
        public int resultsEntryIndex = 0;

        // Constructor
        private ResultsManager()
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
            // Sets the instance.
            if (resultsUI == null)
                resultsUI = ResultsUI.Instance;

            // Sets the instance.
            if (resultsAudio == null)
                resultsAudio = ResultsAudio.Instance;


            // Checks if the results info object exists.
            if (resultsInfo == null)
            {
                resultsInfo = FindObjectOfType<ResultsInfo>(false);
            }

            // Loads the Results
            LoadResultsInfo();
        }

        // Gets the instance.
        public static ResultsManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<ResultsManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("ResultsManager (singleton)");
                        instance = go.AddComponent<ResultsManager>();
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

        // Loads the results information.
        public void LoadResultsInfo()
        {
            LoadResultsInfo(resultsInfo);
        }

        // Loads the results information.
        public void LoadResultsInfo(ResultsInfo info)
        {
            // If there is no, do nothing.
            if (info == null)
                return;

            // Clear the entries.
            resultsEntries.Clear();

            // Goes through all the floor times and turns.
            for(int i = 0; i < resultsInfo.floorTimes.Length && i < resultsInfo.floorTurns.Length; i++)
            {
                // Generates the object.
                FloorResultsEntry.FloorResultsEntryInfo entryInfo = new FloorResultsEntry.FloorResultsEntryInfo();

                // Stores the information.
                entryInfo.floorNumber = i; // Floor 0/Debug is included.
                entryInfo.floorTime = resultsInfo.floorTimes[i]; // Time
                entryInfo.floorTurns = resultsInfo.floorTurns[i]; // Turn

                // Adds the entry info to the list.
                resultsEntries.Add(entryInfo);
            }

            // Removes the first entry, since it's for floor 0.
            resultsEntries.RemoveAt(0);

            // Loads the rest of the information for the UI.
            resultsUI.LoadResultsInfo(info);

            // Load Entries
            resultsUI.LoadResultsEntries();
        }

        // Goes to the title scene.
        public void ToTitleScene()
        {
            SceneManager.LoadScene("TitleScene");
        }


        // This function is called when the MonoBehaviour will be destroyed
        private void OnDestroy()
        {
            // If the saved instance is being deleted, set 'instanced' to false.
            if (instance == this)
            {
                instanced = false;

                // Destroys the game object.
                // This is set to not destroy on load, hence why it must be destroyed this way.
                if (resultsInfo != null)
                {
                    Destroy(resultsInfo.gameObject);
                }
            }
        }
    }
}
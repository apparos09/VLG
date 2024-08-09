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

        // The game turns text.
        public TMP_Text gameTurnsText;

        [Header("Results Entries")]

        // The floor results entries.
        // 1-5
        public FloorResultsEntry resultsEntry01;
        public FloorResultsEntry resultsEntry02;
        public FloorResultsEntry resultsEntry03;
        public FloorResultsEntry resultsEntry04;
        public FloorResultsEntry resultsEntry05;

        // 6-10
        public FloorResultsEntry resultsEntry06;
        public FloorResultsEntry resultsEntry07;
        public FloorResultsEntry resultsEntry08;
        public FloorResultsEntry resultsEntry09;
        public FloorResultsEntry resultsEntry10;

        // The results display count.
        public const int RESULTS_ENTRY_DISPLAY_COUNT = 10;

        // The pages text.
        public TMP_Text pageNumberText;

        [Header("Saving")]

        // The object for saving.
        public GameObject savingObject;

        // The text for saving.
        public TMP_Text savingText;

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
            // If the save system exists, and has been instantiated.
            if (SaveSystem.Instantiated)
            {
                // Gets the save system.
                SaveSystem saveSystem = SaveSystem.Instance;

                // Sets the object and the text.
                saveSystem.feedbackObject = savingObject;
                saveSystem.feedbackText = savingText;

                // Refreshes the feedback elements.
                saveSystem.RefreshFeedbackElements();
            }
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
            // Game Text and Game Turns
            gameTimeText.text = "<b>Total Time:</b> ";
            gameTurnsText.text = "<b>Total Turns:</b> ";

            // Checks the object to know what to load in.
            if (info != null)
            {
                gameTimeText.text += StringFormatter.FormatTime(info.gameTime, true, true, false);
                gameTurnsText.text += info.gameTurns.ToString();
            }
            else
            {
                // Default Text.
                gameTimeText.text += "-";
                gameTurnsText.text += "-";
            }
        }

        // Loads the results entries.
        public void LoadResultsEntries()
        {
            // The entry array
            FloorResultsEntry[] entryArr = new FloorResultsEntry[]
            {
                resultsEntry01,
                resultsEntry02,
                resultsEntry03,
                resultsEntry04,
                resultsEntry05,
                resultsEntry06,
                resultsEntry07,
                resultsEntry08,
                resultsEntry09,
                resultsEntry10,
            };

            // The array index.
            int arrIndex = 0;

            // The info index.
            int infoIndex = resultsManager.resultsEntryIndex;

            // Only do this if there are entries available.
            if(resultsManager.resultsEntries.Count != 0)
            {
                // Loads the entries.
                while (infoIndex < resultsManager.resultsEntries.Count && arrIndex < entryArr.Length)
                {
                    // Loads the info.
                    entryArr[arrIndex].LoadFloorInfo(resultsManager.resultsEntries[infoIndex]);

                    // Increases the indexes.
                    infoIndex++;
                    arrIndex++;
                }
            }

            // Clear remaining entries
            while(arrIndex < entryArr.Length)
            {
                // Clears the info and increases the index.
                entryArr[arrIndex].ClearFloorInfo();
                arrIndex++;
            }


            // The index is now adjusted when the page is changed.
            // // If the end of the list has been reached, reset the index to 0.
            // if(infoIndex >= resultsManager.resultsEntries.Count)
            // {
            //     infoIndex = 0;
            // }
            // 
            // // Replace the index.
            // resultsManager.resultsEntryIndex = infoIndex;

            // Updates the page number text.
            UpdatePageNumberText();
        }

        // Loads the results entries from the provided index.
        public void LoadResultsEntries(int firstEntryIndex)
        {
            // Sets the index.
            resultsManager.resultsEntryIndex = firstEntryIndex;

            // Loads the entries.
            LoadResultsEntries();
        }

        // Goes to the previous page.
        public void PreviousPage()
        {
            // Goes to the previous page.
            resultsManager.resultsEntryIndex -= RESULTS_ENTRY_DISPLAY_COUNT;

            // If the index is below 0, go to the max.
            if (resultsManager.resultsEntryIndex < 0)
            {
                // Gets the remainder so that the remaining entries can be consistently displayed.
                int remainder = resultsManager.resultsEntries.Count % RESULTS_ENTRY_DISPLAY_COUNT;

                // Determine the new index.
                if(remainder <= 0) // No Remainder
                {
                    // Reduce by the display count.
                    resultsManager.resultsEntryIndex = resultsManager.resultsEntries.Count - RESULTS_ENTRY_DISPLAY_COUNT;
                } 
                else // Utilize Renaminder
                {
                    // Set index to be the end of the list, minus the renaminder.
                    resultsManager.resultsEntryIndex = resultsManager.resultsEntries.Count - remainder;
                }
                
            }

            // Clamp the values.
            resultsManager.resultsEntryIndex = Mathf.Clamp(resultsManager.resultsEntryIndex, 0, resultsManager.resultsEntries.Count);

            // Loads the results entries.
            LoadResultsEntries();
        }

        // Goes to the next page.
        public void NextPage()
        {
            // Goes to the next page.
            resultsManager.resultsEntryIndex += RESULTS_ENTRY_DISPLAY_COUNT;

            // If the index is at or above the max, loop back to the start.
            if (resultsManager.resultsEntryIndex >= resultsManager.resultsEntries.Count)
            {
                // Set to 0.
                resultsManager.resultsEntryIndex = 0;
            }

            // Clamp the values.
            resultsManager.resultsEntryIndex = Mathf.Clamp(resultsManager.resultsEntryIndex, 0, resultsManager.resultsEntries.Count);

            // Loads the results entries.
            LoadResultsEntries();
        }

        // Updates the page number text.
        public void UpdatePageNumberText()
        {
            // The current page and pages total.
            int currPage = 0;
            int pagesTotal = Mathf.FloorToInt((float)resultsManager.resultsEntries.Count / RESULTS_ENTRY_DISPLAY_COUNT);

            // If the division operation leaves a remainder, increase the pages 
            if ((float)resultsManager.resultsEntries.Count % RESULTS_ENTRY_DISPLAY_COUNT != 0)
                pagesTotal++;

            // Gets the remainder.
            int remainder = resultsManager.resultsEntries.Count % RESULTS_ENTRY_DISPLAY_COUNT;

            // Current page calculation.
            currPage = (resultsManager.resultsEntryIndex + remainder) / RESULTS_ENTRY_DISPLAY_COUNT + 1;

            // Updates the pages text.
            pageNumberText.text = currPage.ToString() + "/" + pagesTotal.ToString();

        }

        // Returns to the title scene.
        public void ToTitleScene()
        {
            resultsManager.ToTitleScene();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace VLG
{
    // The tutorial object.
    public class Tutorial : MonoBehaviour
    {
        // The singleton instance.
        private static Tutorial instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The tutorial UI.
        public TutorialUI tutorialUI;

        // Constructor
        private Tutorial()
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
        public static Tutorial Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<Tutorial>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Tutorial (singleton)");
                        instance = go.AddComponent<Tutorial>();
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

        // Loads the tutorial
        private void LoadTutorial(ref List<Page> pages)
        {
            // Loads pages for the tutorial.
            tutorialUI.LoadPages(ref pages, false);
        }

        // Load the tutorial (template)
        private void LoadTutorialTemplate()
        {
            // Create the pages list.
            List<Page> pages = new List<Page>();

            // Load the pages.
            pages.Add(new Page("Insert text here."));

            // Change the display image when certain pages are opened using callbacks.

            // Loads the tutorial.
            LoadTutorial(ref pages);
        }

        // Loads the intro tutorial
        public void LoadIntroTutorial()
        {

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
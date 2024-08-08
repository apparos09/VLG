using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Audio for the ResultsScene.
    public class ResultsAudio : GameAudio
    {
        // The singleton instance.
        private static ResultsAudio instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The BGM
        [Header("Results Audio")]

        // BGM
        public AudioClip resultsBgm;

        // Clip Start and End
        public float resultsClipStart = 0;
        public float resultsClipEnd = 0;


        // Constructor
        private ResultsAudio()
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
        protected override void Start()
        {
            base.Start();

            // Auto-set title clip end
            if (resultsClipEnd == 0)
                resultsClipEnd = resultsBgm.length;

            // Play the results BGM.
            PlayResultsBgm();
        }

        // Gets the instance.
        public static ResultsAudio Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<ResultsAudio>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("ResultsAudio (singleton)");
                        instance = go.AddComponent<ResultsAudio>();
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

        // Plays the Results BGM
        public void PlayResultsBgm()
        {
            PlayBackgroundMusic(resultsBgm, resultsClipStart, resultsClipEnd);
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
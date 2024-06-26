using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace VLG
{
    // The Title Audio
    public class TitleAudio : GameAudio
    {
        // The singleton instance.
        private static TitleAudio instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The BGM
        [Header("Title Audio")]
        
        // If 'true', the title bgm is played when the game starts.
        public bool playTitleBgmOnStart = true;

        // BGM
        public AudioClip titleBgm;

        // Clip Start and End
        public float titleClipStart = 0;
        public float titleClipEnd = 0;


        // Constructor
        private TitleAudio()
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
            if(titleClipEnd == 0)
                titleClipEnd = titleBgm.length;

            // Play the title BGM.
            if(playTitleBgmOnStart)
                PlayTitleBgm();
        }

        // Gets the instance.
        public static TitleAudio Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<TitleAudio>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("TitleAudio (singleton)");
                        instance = go.AddComponent<TitleAudio>();
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

        // Plays the title BGM
        public void PlayTitleBgm()
        {
            PlayBackgroundMusic(titleBgm, titleClipStart, titleClipEnd);
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
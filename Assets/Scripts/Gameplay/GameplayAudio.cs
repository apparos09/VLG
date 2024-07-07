using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Audio for the GameplayScene.
    public class GameplayAudio : GameAudio
    {
        // The singleton instance.
        private static GameplayAudio instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The BGM
        [Header("Gameplay Audio")]

        // BGM
        public AudioClip gameplayBgm;

        // Clip Start and End
        public float gameplayClipStart = 0;
        public float gameplayClipEnd = 0;


        // Constructor
        private GameplayAudio()
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
            if (gameplayClipEnd == 0)
                gameplayClipEnd = gameplayBgm.length;
        }

        // Gets the instance.
        public static GameplayAudio Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<GameplayAudio>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("GameplayAudio (singleton)");
                        instance = go.AddComponent<GameplayAudio>();
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

        // Plays the Gameplay BGM
        public void PlayGameplayBgm(int bgmId)
        {
            // TODO: implement BGM ID number.

            PlayBackgroundMusic(gameplayBgm, gameplayClipStart, gameplayClipEnd);
        }

        // Plays the Gameplay BGM
        public void PlayGameplayBgm(Floor floor)
        {
            // Provides the ID
            PlayGameplayBgm(floor.bgmId);
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
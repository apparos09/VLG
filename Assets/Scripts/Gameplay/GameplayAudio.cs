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
        [Header("GameplayAudio")]

        // If 'true', the BGM is restarted if the program tries to play a song that's already playing.
        [Tooltip("If 'true', the BGM is restarted if the program tries to play a song that's already playing.")]
        public bool restartBgmOnReplay = false;

        // BGM
        // Debug BGM
        [Header("GameplayAudio/BGM00")]
        public AudioClip gameplayBgm00;
        public float gameplayBgm00ClipStart = 0;
        public float gameplayBgm00ClipEnd = 0;

        // BGM 1
        [Header("GameplayAudio/BGM01")]
        public AudioClip gameplayBgm01;
        public float gameplayBgm01ClipStart = 0;
        public float gameplayBgm01ClipEnd = 0;

        // BGM 2
        [Header("GameplayAudio/BGM02")]
        public AudioClip gameplayBgm02;
        public float gameplayBgm02ClipStart = 0;
        public float gameplayBgm02ClipEnd = 0;

        // BGM 3
        [Header("GameplayAudio/BGM03")]
        public AudioClip gameplayBgm03;
        public float gameplayBgm03ClipStart = 0;
        public float gameplayBgm03ClipEnd = 0;

        // BGM 4
        [Header("GameplayAudio/BGM04")]
        public AudioClip gameplayBgm04;
        public float gameplayBgm04ClipStart = 0;
        public float gameplayBgm04ClipEnd = 0;

        // BGM 5
        [Header("GameplayAudio/BGM05")]
        public AudioClip gameplayBgm05;
        public float gameplayBgm05ClipStart = 0;
        public float gameplayBgm05ClipEnd = 0;

        // BGM 6/Final Boss BGM
        [Header("GameplayAudio/BGM06")]
        public AudioClip gameplayBgm06;
        public float gameplayBgm06ClipStart = 0;
        public float gameplayBgm06ClipEnd = 0;

        //[Header("GameplayAudio/Sound Effects")]

        //// Floor failed sound effect.
        //public AudioClip floorFailedSfx;

        //// Floor reset sound effect.
        //public AudioClip floorResetSfx;


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
            if (gameplayBgm00ClipEnd == 0)
                gameplayBgm00ClipEnd = gameplayBgm00.length;
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
            // The BGM clip, and the clip points.
            AudioClip bgmClip;
            float clipStart, clipEnd;

            switch(bgmId)
            {
                case 0: // Debug
                default:
                    bgmClip = gameplayBgm00;
                    clipStart = gameplayBgm00ClipStart;
                    clipEnd = gameplayBgm00ClipEnd;
                    break;

                case 1:
                    bgmClip = gameplayBgm01;
                    clipStart = gameplayBgm01ClipStart;
                    clipEnd = gameplayBgm01ClipEnd;
                    break;

                case 2:
                    bgmClip = gameplayBgm02;
                    clipStart = gameplayBgm02ClipStart;
                    clipEnd = gameplayBgm02ClipEnd;
                    break;

                case 3:
                    bgmClip = gameplayBgm03;
                    clipStart = gameplayBgm03ClipStart;
                    clipEnd = gameplayBgm03ClipEnd;
                    break;

                case 4:
                    bgmClip = gameplayBgm04;
                    clipStart = gameplayBgm04ClipStart;
                    clipEnd = gameplayBgm04ClipEnd;
                    break;

                case 5:
                    bgmClip = gameplayBgm05;
                    clipStart = gameplayBgm05ClipStart;
                    clipEnd = gameplayBgm05ClipEnd;
                    break;

                case 6:
                    bgmClip = gameplayBgm06;
                    clipStart = gameplayBgm06ClipStart;
                    clipEnd = gameplayBgm06ClipEnd;
                    break;
            }

            // If the BGM shouldn't be restarted when the program tries to play a song that's already playing.
            if(!restartBgmOnReplay && bgmSource.isPlaying)
            {
                // If this song is already playing, don't start over.
                if (bgmSource.clip == bgmClip)
                    return;
            }

            // Play the background music.
            PlayBackgroundMusic(bgmClip, clipStart, clipEnd);
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
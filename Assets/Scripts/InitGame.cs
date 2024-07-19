using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace VLG
{
    // Initializes the game.
    public class InitGame : MonoBehaviour
    {
        // TODO: put the volume settings in a file that is loaded up on Start.
        [HideInInspector()]
        // The starting BGM volume.
        public float bgmVolume = 0.3F;

        [HideInInspector()]
        // The starting SFX volume.
        public float sfxVolume = 0.6F;

        private void Awake()
        {
            // Frame Rate
            Application.targetFrameRate = 30;
        }

        // Start is called before the first frame update
        void Start()
        {
            // If the game isn't running in the WebGL player, try to load the save data.
            if(Application.platform != RuntimePlatform.WebGLPlayer)
            {
                // Load the game data.
                SaveSystem.Instance.LoadGame();
            }

            // Auto-set the BGM and SFX volume.
            AudioControls ac = AudioControls.Instance;
            ac.BackgroundMusicVolume = bgmVolume;
            ac.SoundEffectVolume = sfxVolume;
        }

        // Update is called once per frame
        void Update()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
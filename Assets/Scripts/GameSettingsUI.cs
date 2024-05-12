using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLG
{
    // The game settings UI.
    public class GameSettingsUI : MonoBehaviour
    {
        // The game settings.
        public GameSettings gameSettings;

        [Header("Sliders")]

        // The BGM slider
        public Slider bgmSlider;

        // The SFX slider
        public Slider sfxSlider;

        // Start is called before the first frame update
        void Start()
        {
            // If the game settings instance hasn't been set, set it.
            if (gameSettings == null)
                gameSettings = GameSettings.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using util;

namespace VLG
{
    // The game settings UI.
    public class GameSettingsUI : MonoBehaviour
    {
        // The game settings.
        public GameSettings gameSettings;

        // The audio controls for the game.
        public AudioControls audioControls;

        [Header("Sliders")]

        // The BGM slider
        public Slider bgmSlider;

        // The SFX slider
        public Slider sfxSlider;

        [Header("Toggles")]
        // The Mute toggle.
        public Toggle muteToggle;

        // Awake is called when the script instance being loaded
        private void Awake()
        {
            // If the game settings instance hasn't been set, set it.
            if (gameSettings == null)
                gameSettings = GameSettings.Instance;

            // If the audio controls instance hasn't been set, set it.
            if (audioControls == null)
                audioControls = AudioControls.Instance;
        }

        // Start is called before the first frame update
        void Start()
        {
            // ...
        }

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            // Auto-set the volume settings.
            bgmSlider.value = Mathf.Lerp(bgmSlider.minValue, bgmSlider.maxValue, audioControls.BackgroundMusicVolume);
            sfxSlider.value = Mathf.Lerp(sfxSlider.minValue, sfxSlider.maxValue, audioControls.SoundEffectVolume);
            muteToggle.isOn = audioControls.Mute;
        }

        // This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable()
        {
            
        }

        // VOLUME //

        // Called when the BGM slider has been changed.
        public void OnBgmSliderValueChanged()
        {
            OnBgmSliderValueChanged(bgmSlider);
        }

        // Called when the BGM slider has been changed.
        public void OnBgmSliderValueChanged(Slider slider)
        {
            // Gets the value and provides it as the volume.
            float value = Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value);
            audioControls.BackgroundMusicVolume = value;
        }

        // Called when the SFX slider has been changed.
        public void OnSfxSliderValueChanged()
        {
            OnSfxSliderValueChanged(sfxSlider);
        }

        // Called when the SFX slider has been changed.
        public void OnSfxSliderValueChanged(Slider slider)
        {
            // Gets the value and provides it as the volume.
            float value = Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value);
            audioControls.SoundEffectVolume = value;
        }

        // Called when the mute option has been toggled.
        public void OnMuteToggle()
        {
            OnMuteToggle(muteToggle);
        }

        // Called when the mute option has been toggled.
        public void OnMuteToggle(Toggle toggle)
        {
            audioControls.Mute = toggle.isOn;
        }
    }
}
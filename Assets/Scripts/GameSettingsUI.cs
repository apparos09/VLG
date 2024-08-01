using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        // The cutscenes toggle.
        public Toggle cutscenesToggle;

        [Header("Dropdown")]
        // The resolution dropdown.
        public TMP_Dropdown resolutionDropdown;

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
            // Refresh the dropdown.
            RefreshResolutionDropdown();

            // If the runtime platform is WebGL, disable the resolution dropdown.
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                resolutionDropdown.interactable = false;
        }

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            // Auto-set the volume settings.
            bgmSlider.value = Mathf.Lerp(bgmSlider.minValue, bgmSlider.maxValue, audioControls.BackgroundMusicVolume);
            sfxSlider.value = Mathf.Lerp(sfxSlider.minValue, sfxSlider.maxValue, audioControls.SoundEffectVolume);
            muteToggle.isOn = audioControls.Mute;

            // Auto-set cutscenes.
            cutscenesToggle.isOn = gameSettings.cutscenes;

            // Auto-set resolution.
            RefreshResolutionDropdown();
            
        }

        // This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable()
        {
            // If the settings object has been instantiated.
            if(SaveSystem.Instantiated)
            {
                // If saving/loading is enabled, save the game settings data.
                if (SaveSystem.Instance.allowSaveLoad)
                {
                    gameSettings.SaveGameSettingsDataToFile();
                }
            }
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

        // Called when the cutscenes option has been toggled.
        public void OnCutscenesToggle()
        {
            OnCutscenesToggle(cutscenesToggle);
        }

        // Called when the cutscenes option has been toggled.
        public void OnCutscenesToggle(Toggle toggle)
        {
            gameSettings.cutscenes = toggle.isOn;
        }

        // Refreshes the resolution dropdown to make sure it's displaying the right resolution.
        public void RefreshResolutionDropdown()
        {

            // Check if in full screen or not.
            if(Screen.fullScreen) // Full Screen
            {
                resolutionDropdown.value = 2;
            }
            else // Not Full Screen
            {
                // Gets the current resolution.
                int height = Screen.height;

                switch (height)
                {
                    default: // Set it to the first value by default.
                    case 576:
                        resolutionDropdown.value = 0;
                        break;

                    case 720:
                        resolutionDropdown.value = 1;
                        break;

                }
            }
            
        }

        // Called when the resolutiond dropdown has been changed.
        public void OnResolutionDropdownValueChanged()
        {
            OnResolutionDropdownValueChanged(resolutionDropdown);
        }

        // Called when the resolutiond dropdown has been changed.
        public void OnResolutionDropdownValueChanged(TMP_Dropdown dropdown)
        {
            // Checks what the resolution it should be.
            switch (dropdown.value)
            {
                case 0: // 1024 X 576
                default:
                    gameSettings.SetScreenSize1024x576();
                    break;

                case 1: // 1280 X 720 
                    gameSettings.SetScreenSize1280x720();
                    break;

                case 2: // Full Screen
                    gameSettings.FullScreen = true;
                    break;
            }
        }

        // Applies the default game settings.
        public void ApplyDefaultGameSettings()
        {
            // Loads the default game settings.
            gameSettings.LoadDefaultGameSettingsData();
            
            // Change values in UI.
            OnEnable();
        }
    }
}
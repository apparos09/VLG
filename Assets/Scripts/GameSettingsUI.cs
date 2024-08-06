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

        // Used to call post start.
        private bool calledPostStart = false;

        [Header("Sliders")]

        // The BGM slider
        public Slider bgmSlider;

        // The SFX slider
        public Slider sfxSlider;

        [Header("Toggles")]
        // The mute toggle.
        public Toggle muteToggle;

        // The tutorials toggle.
        public Toggle tutorialsToggle;

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

        // Called after the Start function.
        void PostStart()
        {
            // Refreshes the game settings UI another time to make sure it updated properly.
            // This is used to correct the resolution dropdown, which doesn't get updated...
            // The first time a reset occurs for some reason.
            // ...It didn't work.
            RefreshGameSettingsUI();

            calledPostStart = true;
        }

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            RefreshGameSettingsUI();
            
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

        // Refreshes the game settings UI.
        public void RefreshGameSettingsUI()
        {
            // Auto-set the volume settings.
            bgmSlider.value = Mathf.Lerp(bgmSlider.minValue, bgmSlider.maxValue, audioControls.BackgroundMusicVolume);
            sfxSlider.value = Mathf.Lerp(sfxSlider.minValue, sfxSlider.maxValue, audioControls.SoundEffectVolume);

            // Auto-set the toggles
            muteToggle.isOn = audioControls.Mute;
            tutorialsToggle.isOn = gameSettings.useTutorials;
            cutscenesToggle.isOn = gameSettings.playCutscenes;

            // Auto-set resolution.
            RefreshResolutionDropdown();
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

        // Called when the tutorial option has been toggled.
        public void OnTutorialsToggle(Toggle toggle)
        {
            gameSettings.useTutorials = toggle.isOn;
        }

        // Called when the tutorial option has been toggled.
        public void OnTutorialsToggle()
        {
            OnTutorialsToggle(tutorialsToggle);
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
            gameSettings.playCutscenes = toggle.isOn;
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
            RefreshGameSettingsUI();

            // Make the post start function get called again to correct the UI.
            calledPostStart = false;
        }

        // Update is called once per frame
        void Update()
        {
            // If PostStart has not been called, call it.
            if(!calledPostStart)
            {
                PostStart();
            }

        }
    }
}
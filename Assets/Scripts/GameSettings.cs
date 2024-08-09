using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using util;

namespace VLG
{
    [System.Serializable]
    // The save data for the game settings.
    public class GameSettingsData
    {
        // Volume
        public float bgmVolume = 1.0F;
        public float sfxVolume = 1.0F;
        public float vceVolume = 1.0F;

        // Mute
        public bool mute = false;

        // Tutorial
        public bool useTutorials = false;

        // Cutscenes
        public bool playCutscenes = true;

        // Resolution (Vectors cannot be serialized).
        public bool fullScreen = false;
        public int screenWidth = 1024;
        public int screenHeight = 576;
    }

    // The game settings.
    public class GameSettings : MonoBehaviour
    {
        // The game settings instance.
        private static GameSettings instance;

        // Gets set to 'true' when the singleton is initialized.
        private bool initialized = false;

        // The audio controls for the game.
        public AudioControls audioControls;

        // If 'true', tutorial elements are used.
        public bool useTutorials = false;

        // Cutscenes
        public bool playCutscenes = true;

        [Header("File")]

        // The file reader for saving/loading game settings info.
        public FileReaderBytes fileReader = null;

        // The settings file.
        public string file = "settings.dat";

        // The primary file path.
        public string filePath1 = "";

        // The secondary file path.
        public string filePath2 = "";

        // Constructor
        private GameSettings()
        {
            // ...

        }

        // Awake is called when the script is being loaded
        void Awake()
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
            }

            // Run code for initialization.
            if (!initialized)
            {
                initialized = true;
            }
        }

        // Start is called just before any of the Update methods is called the first time
        private void Start()
        {
            // Don't destroy this object on load.
            DontDestroyOnLoad(this);

            // Checks if audio controls exists. If not, then grab the instance.
            if (audioControls == null)
            {
                audioControls = AudioControls.Instance;
            }

            // If the file reader has not been set.
            if(fileReader == null)
            {
                // Try to get the file reader.
                if(!TryGetComponent<FileReaderBytes>(out fileReader))
                {
                    // The file reader couldn't be found, so add the file reader.
                    fileReader = gameObject.AddComponent<FileReaderBytes>();
                }
            }
        }

        // Gets the instance.
        public static GameSettings Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<GameSettings>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Game Settings (singleton)");
                        instance = go.AddComponent<GameSettings>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        // RESOLUTION //
        // Changes full-screen settings.
        public bool FullScreen
        {
            get
            {
                return Screen.fullScreen;
            }

            set
            {
                Screen.fullScreen = value;
            }
        }

        // Toggles the full screen.
        public static void ToggleFullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        // Called to change the screen size.
        public void SetScreenSize(int width, int height, FullScreenMode mode)
        {
            Screen.SetResolution(width, height, mode);
        }


        // Called to change the screen size.
        public void SetScreenSize(int width, int height, FullScreenMode mode, bool fullScreen)
        {
            SetScreenSize(width, height, mode);
            Screen.fullScreen = fullScreen;
        }


        // Set Screen Size (1080 Resolution - 16:9)
        public void SetScreenSize1920x1080(FullScreenMode mode = FullScreenMode.MaximizedWindow)
        {
            SetScreenSize(1920, 1080, mode, false);
        }

        // Set Screen Size (720 Resolution - 16:9)
        public void SetScreenSize1280x720(FullScreenMode mode = FullScreenMode.Windowed)
        {
            SetScreenSize(1280, 720, mode, false);
        }

        // Set Screen Size (16:9 - WSVGA (Wide Super Video Graphics Array))
        public void SetScreenSize1024x576(FullScreenMode mode = FullScreenMode.Windowed)
        {
            SetScreenSize(1024, 576, mode, false);
        }



        // VOLUME //

        // Muting the game audio.
        public bool Mute
        {
            get
            {
                return AudioListener.pause;
            }

            set
            {
                AudioListener.pause = value;
            }
        }

        // Master volume.
        public float Volume
        {
            get
            {
                return AudioListener.volume;
            }

            set
            {
                AudioListener.volume = Mathf.Clamp01(value);
            }
        }


        // SAVE/LOAD
        // Applies the file reader settings.
        public string ApplyFileReaderSettings()
        {
            // The save file for the settings.
            fileReader.fileName = file;

            // The path to be returned.
            string path;

            // Checks if filePath1 exists.
            if (FileReader.FilePathExists(filePath1))
            {
                fileReader.filePath = filePath1;
                path = filePath1;
            }
            // Checks if filePath2 exists.
            else if (FileReader.FilePathExists(filePath2))
            {
                fileReader.filePath = filePath2;
                path = filePath2;
            }
            // Defulats to filePath1.
            else
            {
                fileReader.filePath = filePath1; // Set path.
                fileReader.CreateFilePath(); // Create path.
                path = filePath1; // Save path.
            }

            return path;
        }


        // Generates game settings data.
        public GameSettingsData GenerateGameSettingsData()
        {
            GameSettingsData data = new GameSettingsData();

            // Audio Information
            data.bgmVolume = audioControls.BackgroundMusicVolume;
            data.sfxVolume = audioControls.SoundEffectVolume;
            data.vceVolume = audioControls.VoiceVolume;
            data.mute = audioControls.Mute;

            // Cutscenes Information
            data.useTutorials = useTutorials;
            data.playCutscenes = playCutscenes;

            // Resolution
            data.fullScreen = Screen.fullScreen;
            data.screenWidth = Screen.width;
            data.screenHeight = Screen.height;

            return data;
        }

        // Saves the game settings data.
        public void SaveGameSettingsDataToFile()
        {
            // Generates the file path.
            ApplyFileReaderSettings();

            // Generates the data.
            GameSettingsData data = GenerateGameSettingsData();

            // Gets the file.
            string file = fileReader.GetFileWithPath();

            // Seralize the data.
            byte[] dataArr = SaveSystem.SerializeObject(data);

            // If the data did not serialize properly, do nothing.
            if (dataArr.Length == 0)
                return;

            // Write to the file.
            File.WriteAllBytes(file, dataArr);
        }

        // Loads game settings data.
        public void LoadGameSettingsData(GameSettingsData data)
        {
            // Audio Information
            audioControls.BackgroundMusicVolume = data.bgmVolume;
            audioControls.SoundEffectVolume = data.sfxVolume;
            audioControls.VoiceVolume = data.vceVolume;
            audioControls.Mute = data.mute;

            // Tutorials and Cutscenes
            useTutorials = data.useTutorials;
            playCutscenes = data.playCutscenes;

            // Resolution
            // This doesn't happen in WebGL because it screws up the screen resolution.
            if(Application.platform != RuntimePlatform.WebGLPlayer)
            {
                FullScreen = data.fullScreen;
                SetScreenSize(data.screenWidth, data.screenHeight, FullScreenMode.Windowed, FullScreen);
            }
            
        }

        // Loads game settings data from a file. Returns 'true' if successful.
        public bool LoadGameSettingsDataFromFile()
        {
            // Generates the file path.
            ApplyFileReaderSettings();

            // Gets the file.
            string file = fileReader.GetFileWithPath();

            // Checks that the file exists.
            if (!fileReader.FileExists())
                return false;

            // Read from the file.
            byte[] dataArr = File.ReadAllBytes(file);

            // Data did not serialize properly.
            if (dataArr.Length == 0)
                return false;

            // Deseralize the data.
            object data = SaveSystem.DeserializeObject(dataArr);

            // Convert to loaded data.
            GameSettingsData loadData = (GameSettingsData)(data);

            // Use the loaded data.
            LoadGameSettingsData(loadData);
            return true;
        }


        // Generates the default game settings data.
        public static GameSettingsData GenerateDefaultGameSettingsData()
        {
            // Game settings data.
            GameSettingsData settingsData = new GameSettingsData();

            // Default Values
            // Volume
            settingsData.bgmVolume = 0.3F;
            settingsData.sfxVolume = 0.6F;
            settingsData.vceVolume = 1.0F;
            settingsData.mute = false;

            // Tutorial
            settingsData.useTutorials = false;

            // Cutscenes
            settingsData.playCutscenes = true;
            
            // Resolution
            settingsData.fullScreen = false;
            settingsData.screenWidth = 1024;
            settingsData.screenHeight = 576;

            // Return the data.
            return settingsData;
        }

        // Loads the default game settings data.
        public void LoadDefaultGameSettingsData()
        {
            LoadGameSettingsData(GenerateDefaultGameSettingsData());
        }

        // Quits the application.
        public static void QuitApplication()
        {
            Application.Quit();
        }
    }
}

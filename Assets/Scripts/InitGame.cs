using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VLG
{
    // Initializes the game.
    public class InitGame : MonoBehaviour
    {
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
        }

        // Update is called once per frame
        void Update()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The game settings UI.
    public class GameSettingsUI : MonoBehaviour
    {
        // The game settings.
        public GameSettings gameSettings;

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
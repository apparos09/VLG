using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using util;

namespace VLG
{
    // UI Audio Element
    public class VLG_UIElementAudio : MonoBehaviour
    {
        // The game audio object.
        public GameAudio gameAudio;

        // The UI element audio.
        public UIElementAudio uiElementAudio;

        // Awake is called when the script instance is being loaded.
        protected virtual void Awake()
        {
            // ...
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // If the game audio is not set.
            if(gameAudio == null)
            {
                // Find the object.
                gameAudio = FindObjectOfType<GameAudio>();
            }

            // If the UI audio element is set.
            if(uiElementAudio == null)
            {
                // Gets the component.
                uiElementAudio = GetComponent<UIElementAudio>();
            }

            // Sets the audio source.
            uiElementAudio.audioSource = gameAudio.sfxSource;
        }


    }
}
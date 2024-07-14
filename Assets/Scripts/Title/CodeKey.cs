using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // An on-screen key to be used for code input.
    public class CodeKey : MonoBehaviour
    {
        // The code input.
        public CodeInput codeInput;

        // The character tied to this key.
        public char key;

        // Start is called before the first frame update
        void Start()
        {
            // If the code input is not set, try to find it.
            if(codeInput == null)
                codeInput = FindObjectOfType<CodeInput>();
        }

        // Called when the key has been pressed.
        public void OnKeyPressed()
        {
            // Adds the character.
            if(codeInput != null)
                codeInput.AddCharacter(key);
        }

    }
}
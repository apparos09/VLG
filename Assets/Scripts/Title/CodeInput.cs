using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VLG
{
    // Manages input for floor code.
    public class CodeInput : MonoBehaviour
    {
        // The title manager.
        public TitleManager titleManager;

        // The title UI.
        public TitleUI titleUI;

        // The floor data to check the code of.
        public FloorData floorData;

        [Header("UI")]

        // The password field for the title screen (selects level).
        public TMP_InputField codeInputField;

        // The start button (gets disabled if a code is invalid).
        public Button startButton;

        // Start is called before the first frame update
        void Start()
        {
            // Grabs the instance if it's not set.
            if (titleManager == null)
                titleManager = TitleManager.Instance;

            // Gets the instnce if it's not set.
            if (titleUI == null)
                titleUI = TitleUI.Instance;

            // Gets the instance if it's not set.
            if (floorData == null)
                floorData = FloorData.Instance;

            // Disalbe the start button by default.
            startButton.interactable = false;
        }

        // This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            startButton.interactable = false;
        }


        // Adds the provided character to the input field.
        public void AddCharacter(char c)
        {
            // Grabs the text, adds the character, and clears the code input.
            string text = codeInputField.text;
            text += c;
            codeInputField.text = text;
        }

        // Removes a character from the input field.
        public void RemoveLastCharacter()
        {
            // Grabs the text, removes the last character, and puts it back into the input field.
            string text = codeInputField.text;

            // There is text to remove.
            if(text != string.Empty)
            {
                // Removes the last character, and puts in the resulting text.
                text = text.Remove(text.Length - 1, 1);
                codeInputField.text = text;
            }
        }

        // Clears the text for the code input.
        public void ClearText()
        {
            codeInputField.text = string.Empty;
        }

        // Called when the input field has been edited.
        public void OnInputFieldEdited()
        {
            // Disables the start button until the code is submitted.
            if(startButton.interactable)
                startButton.interactable = false;
        }

        // Submits the code using the input field.
        public void SubmitCode()
        {
            // Gets the code and enters the other submit function.
            string code = codeInputField.text;
            SubmitCode(code);
        }

        // Submits the code using the input field.
        public void SubmitCode(TMP_InputField inputField)
        {
            SubmitCode(inputField.text);
        }

        // Submits the code (takes code in directly)
        public void SubmitCode(string code)
        {
            // Checks if the code is valid.
            bool valid = floorData.IsFloorCodeValid(code);

            // Checks if the code is valid.
            if (valid)
            {
                // If the title manage is not set, set it.
                if(titleManager == null)
                    titleManager = TitleManager.Instance;

                // Gets the floor ID using the code and sets it.
                titleManager.gameInfo.floorId = floorData.GetFloorIdByCode(code);
            }

            // Called when the code has been submitted.
            OnCodeSubmitted(valid);
        }

        // Called when a code has been submitted.
        public void OnCodeSubmitted(bool success)
        {
            // Checks if the code submission was a success.
            if(success)
            {
                startButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
            }
        }

        // Starts the game.
        public void StartGame()
        {
            // Starts the game using the set ID.
            titleManager.StartGame();
        }
        
    }
}
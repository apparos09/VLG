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

        // If 'true', debug codes are allowed to be submitted.
        private bool allowDebugCodes = true;

        [Header("UI")]

        // The password field for the title screen (selects level).
        public TMP_InputField codeInputField;

        // If 'true', the input field has a limited length.
        private bool limitInputLength = true;

        // The start button (gets disabled if a code is invalid).
        // TODO: this button is no longer used, and should be removed.
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

            // If running inside the Unity editor, allow debug codes.
            // If not, don't allow debug codes.
            allowDebugCodes = Application.isEditor;
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
            // If the input length should be limited.
            if(limitInputLength)
            {
                // If the limit on the input field has been reached, remove the additional characters.
                if (codeInputField.text.Length > FloorData.FLOOR_CODE_CHAR_LIMIT)
                    codeInputField.text = codeInputField.text.Substring(0, FloorData.FLOOR_CODE_CHAR_LIMIT);
            }

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

            // If debug codes should NOT be allowed, check if a debug code was sent.
            if(!allowDebugCodes)
            {
                // If the provided code is the same as the debug code, set valid to false.
                if (floorData.GetDebugFloorCode() == code)
                    valid = false;
            }

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

                // Valid Code SFX
                titleManager.titleAudio.PlayCodeValidSfx();
            }
            else
            {
                startButton.interactable = false;

                // Invalid Code SFX
                titleManager.titleAudio.PlayCodeInvalidSfx();
            }
        }

        // Starts the game.
        public void StartGame()
        {
            // Starts the game using the set ID.
            titleManager.StartGame();
        }

        // Submits the code and starts the game automatically if the code is valid.

        public void SubmitAndStart()
        {
            // Submits the code.
            SubmitCode();

            // If the start button is now interactable, that means the code was valid.
            // So start the game.
            if (startButton.interactable)
                StartGame();
        }

    }
}
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
        // The title UI.
        public TitleUI titleUI;

        // The password field for the title screen (selects level).
        public TMP_InputField codeInputField;

        // The floor data to check the code of.
        public FloorData floorData;

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instnce if it's not set.
            if (titleUI == null)
                titleUI = TitleUI.Instance;

            // Gets the instance if it's not set.
            if (floorData == null)
                floorData = FloorData.Instance;
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
                // TODO: create object and dont destroy it on load.
            }
        }

        

        // Update is called once per frame
        void Update()
        {

        }
    }
}
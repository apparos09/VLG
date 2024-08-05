using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using util;

namespace VLG
{
    // The UI for the tutorial.
    public class TutorialUI : MonoBehaviour
    {
        // The singleton instance.
        private static TutorialUI instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The tutorial text box.
        public TextBox textBox;

        [Header("Diagram")]
        // The text box image object.
        public GameObject textBoxDiagram;

        // The text box image.
        public Image textBoxDiagramImage;

        // The alpha 0 sprite.
        public Sprite alpha0Sprite;

        // Constructor
        private TutorialUI()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected virtual void Awake()
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
                return;
            }

            // Run code for initialization.
            if (!instanced)
            {
                textBox.OnTextBoxOpenedAddCallback(OnTextBoxOpened);
                textBox.OnTextBoxClosedAddCallback(OnTextBoxClosed);
                textBox.OnTextBoxFinishedAddCallback(OnTextBoxFinished);

                instanced = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // If the text box is open, close it.
            if(textBox.gameObject.activeSelf)
            {
                textBox.gameObject.SetActive(false);
            }
        }

        // Gets the instance.
        public static TutorialUI Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<TutorialUI>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Tutorial UI (singleton)");
                        instance = go.AddComponent<TutorialUI>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public static bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Text Box
        // Loads pages for the textbox.
        public void LoadPages(ref List<Page> pages, bool clearPages)
        {
            // If the pages should be cleared.
            if (clearPages)
                textBox.ClearPages();

            // Adds pages to the end of the text box.
            textBox.pages.AddRange(pages);

        }

        // Opens Text Box
        public void OpenTextBox()
        {
            textBox.Open();
        }

        // Closes the Text Box
        public void CloseTextBox()
        {
            textBox.Close();
        }

        // Text box operations.
        // Called when the text box is opened.
        private void OnTextBoxOpened()
        {
            // Hides the diagram by default.
            HideDiagram();
        }

        // Called when the text box is closed.
        private void OnTextBoxClosed()
        {
            // ...
        }

        // Called when the text box is finished.
        private void OnTextBoxFinished()
        {
            // Remove all the pages.
            textBox.ClearPages();

            // Clear the diagram and hides it.
            ClearDiagram();
            HideDiagram();
        }

        // Diagram
        // Sets the diagram's visibility.
        public void SetDiagramVisibility(bool visible)
        {
            textBoxDiagram.SetActive(visible);
        }

        // Show the diagram.
        public void ShowDiagram()
        {
            SetDiagramVisibility(true);
        }

        // Hide the diagram.
        public void HideDiagram()
        {
            SetDiagramVisibility(false);
        }

        // Clears the diagram.
        public void ClearDiagram()
        {
            // Clear out the sprite.
            textBoxDiagramImage.sprite = alpha0Sprite;
        }

        // This function is called when the MonoBehaviour will be destroyed.
        private void OnDestroy()
        {
            // If the saved instance is being deleted, set 'instanced' to false.
            if (instance == this)
            {
                instanced = false;
            }
        }
    }
}
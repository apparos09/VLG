using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VLG
{
    // The floor select button.
    public class FloorSelectButton : MonoBehaviour
    {
        // The floor select.
        public FloorSelect floorSelect;

        // The button for the floor select button.
        public Button button;

        // The text for the button.
        public TMP_Text text;

        // The floor ID number.
        public int floorId = -1;

        // Autosets the floor ID.
        public bool autoSetFloorId = false;

        // Start is called before the first frame update
        void Start()
        {
            // If the floor select is not set, search for it.
            if(floorSelect == null)
                floorSelect = FindObjectOfType<FloorSelect>();

            // If the button isn't set, try to find it.
            if(button == null)
                button = GetComponent<Button>();

            // If the text isn't set, try to find it.
            if(text == null)
                text = GetComponentInChildren<TMP_Text>();

            // If the floor ID should be autoset.
            if(autoSetFloorId)
            {
                // The value being set.
                int value = 0;

                // Tries to convert the value.
                if(int.TryParse(text.text, out value))
                {
                    // Set the value.
                    floorId = value;
                }
                else
                {
                    Debug.LogError("Autoset floor ID has failed.");
                }
            }
        }

        // Called to select the floor.
        public void SelectFloor()
        {
            // If the floor select has been set.
            if(floorSelect != null)
                floorSelect.SelectFloor(floorId);
        }

        // Called to select the floor.
        public void SelectFloor(int newId)
        {
            floorId = newId;
            SelectFloor();
        }

    }
}
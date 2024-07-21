using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VLG
{
    // The floor loading screen.
    public class FloorLoadingScreen : MonoBehaviour
    {
        // The game manager.
        public GameplayManager gameManager;

        // The floor manager.
        public FloorManager floorManager;

        // The floor number text.
        public TMP_Text floorNumberText;

        // The floor objective text.
        public TMP_Text objectiveText;

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance if this is null.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Gets the instance if this is null.
            if (floorManager == null)
                floorManager = FloorManager.Instance;
        }

        // Updates the floor display.
        public void UpdateFloorDisplay(Floor floor)
        {
            // Grabs the instance if it's not already set.
            if(floorManager == null)
                floorManager = FloorManager.Instance;

            // The floor number
            floorNumberText.text = "Floor " + floor.id.ToString();
            
            // Note: this may not work since the goal doesn't exist yet.
            // objectiveText.text = floorManager.goal.GetObjectiveDescription();
        }

    }
}
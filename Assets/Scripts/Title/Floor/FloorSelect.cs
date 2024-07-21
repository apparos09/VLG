using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Used to select the floor.
    public class FloorSelect : MonoBehaviour
    {
        // The title manager.
        public TitleManager titleManager;

        // The title UI.
        public TitleUI titleUI;

        // Start is called before the first frame update
        void Start()
        {
            // Grab the title manager.
            if (titleManager == null)
                titleManager = TitleManager.Instance;

            // Grab the title UI.
            if(titleUI == null)
                titleUI = TitleUI.Instance;
        }

        // Selects the floor using the ID.
        public void SelectFloor(int floorId)
        {
            // Sets the floor ID.
            titleManager.gameInfo.floorId = floorId;

            // Starts the game.
            titleManager.StartGame();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
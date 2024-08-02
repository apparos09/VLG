using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Saves data for the GameplayScene so that the level can be loaded.
    public class GameInfo : MonoBehaviour
    {
        // If 'true', the game should load from the save data instead.
        public bool loadFromSave = false;

        // The id of the floor to be loaded.
        public int floorId = -1;

        // The number of floors the player will go through.
        public int floorCount = 0;
    }
}
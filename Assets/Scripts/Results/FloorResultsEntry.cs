using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;
using util;

namespace VLG
{
    // The floor results entry.
    public class FloorResultsEntry : MonoBehaviour
    {
        // The floor results entry info.
        public struct FloorResultsEntryInfo
        {
            public int floorNumber;
            public float floorTime;
            public int floorTurns;
        }

        // The text elements.
        public TMP_Text floorNumberText;
        public TMP_Text floorTimeText;
        public TMP_Text floorTurnsText;
        

        // Loads the floor info.
        public void LoadFloorInfo(FloorResultsEntryInfo entry)
        {
            floorNumberText.text = entry.floorNumber.ToString();
            floorTimeText.text = StringFormatter.FormatTime(entry.floorTime, true, true, false);
            floorTurnsText.text = entry.floorTurns.ToString();
        }

        // Loads the floor info.
        public void LoadFloorInfo(int floorNumber, float floorTime, int floorTurns)
        {
            // Object
            FloorResultsEntryInfo info = new FloorResultsEntryInfo();
            
            // Sets the values.
            info.floorNumber = floorNumber;
            info.floorTime = floorTime;
            info.floorTurns = floorTurns;

            // Loads the info.
            LoadFloorInfo(info);
        }

        // Clears the floor info.
        public void ClearFloorInfo()
        {
            floorNumberText.text = "-";
            floorTimeText.text = "-";
            floorTurnsText.text = "-";
        }
    }
}

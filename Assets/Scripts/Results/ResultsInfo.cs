using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Brings info over to the results scene.
    public class ResultsInfo : MonoBehaviour
    {
        // The game time.
        public float gameTime = 0.0F;

        // The game turns.
        public float gameTurns = 0;

        // The floor times.
        public float[] floorTimes = new float[FloorData.FLOOR_COUNT];

        // The floor turn counts.
        public int[] floorTurns = new int[FloorData.FLOOR_COUNT];
    }
}
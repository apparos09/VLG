using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VLG
{
    // An enemy that covers a certain number of spaces around itself.
    public class BarEnemy : Enemy
    {
        // Bar
        private struct Bar
        {
            // The amount of segments for the bar.
            public List<Enemy> segments;

            // The bar direction.
            public Vector2Int barDirec;
        }

        [Header("BarEnemy")]

        // THe prefab for the bar segment.
        public Enemy barSegmentPrefab;

        // Maximum number of bars for the enemy.
        private int BARS_COUNT_MAX = 4;

        // The number of bars for the enemy. Maximum 4 (4 directions)
        [Range(0, 4)]
        public int barCount = 1;

        // The number of segments per bar.
        public int segmentCount = 1;

        // The four bars for the enemy.
        private Bar[] bars = new Bar[4];

        // The number of times the bars have been alternated (0-3, loops back).
        private int barAltCount = 0;

        // If 'true', the bars are angled diagonally ("x" shape).
        // If false, the bars are perpedicular to one another ("+" shape).
        public bool diagonal = false;

        // If 'true', the bars are alternated to simulate a rotation.
        [Tooltip("If true, bars are toggled on/off to simulate a rotation.")]
        public bool alternateBars = true;

        // If 'true', the alternation order is reversed.
        [Tooltip("If true, the bars are alternated in reverse.")]
        public bool reversed = false;

        // The list of bar enemies in the game.
        public static List<BarEnemy> barEnemies = new List<BarEnemy>();



        // Awake is called when the script instance is being loaded
        protected override void Awake()
        {
            base.Awake();

            // Assigns relevant member variables.
            // This was moved here from Start() to address a null reference error.
            for (int i = 0; i < bars.Length; i++)
            {
                // Generates a new bar, list of segments, and direction.
                bars[i] = new Bar();
                bars[i].segments = new List<Enemy>();
                bars[i].barDirec = new Vector2Int();
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Generates the bars.
            GenerateBars();

            // Add to the list.
            if (!barEnemies.Contains(this))
                barEnemies.Add(this);
        }

        // Generates bars for the enemy.
        public void GenerateBars()
        {
            // Clamps the bar count within the max.
            barCount = Mathf.Clamp(barCount, 0, BARS_COUNT_MAX);

            // Generates the bars.
            for (int i = 0; i < bars.Length; i++)
            {
                // Sets the directions
                switch (i)
                {
                    default:
                    case 0: // Right/Right-Up (0 degrees)
                        bars[i].barDirec = (diagonal) ? new Vector2Int(1, 1) : Vector2Int.right;
                        break;

                    case 1: // Up/Up-Left (90 degrees)
                        bars[i].barDirec = (diagonal) ? new Vector2Int(-1, 1) : Vector2Int.up;
                        break;

                    case 2: // Left/Left-Down (180 degrees)
                        bars[i].barDirec = (diagonal) ? new Vector2Int(-1, -1) : Vector2Int.left;
                        break;

                    case 3: // Down/Down-Right (270 degrees)
                        bars[i].barDirec = (diagonal) ? new Vector2Int(1, -1) : Vector2Int.down;
                        break;

                }

                // Generating remaining segments.
                while (bars[i].segments.Count < segmentCount)
                {
                    bars[i].segments.Add(Instantiate(barSegmentPrefab));
                }

                // Removing additional segments.
                while (bars[i].segments.Count < segmentCount)
                {
                    // The index of the last segment.
                    int index = bars[i].segments.Count - 1;

                    // Destroy the segment and remove it from the list.
                    Enemy segment = bars[i].segments[index];
                    Destroy(segment.gameObject);
                    bars[i].segments.RemoveAt(index);
                }

                // Positioning segments.
                for (int j = 0; j < segmentCount; j++)
                {
                    // Make segment active.
                    bars[i].segments[j].gameObject.SetActive(true);

                    // Segment floor position.
                    Vector2Int segFloorPos = floorPos + bars[i].barDirec * (j + 1);
                    bars[i].segments[j].floorPos = segFloorPos;

                    // Calculate segment local position (this is manually calculated since the segment might go off the floor).
                    Vector3 segLocalPos = new Vector3();
                    segLocalPos.x = bars[i].barDirec.x * (j + 1) * floorManager.floorSpacing.x;
                    segLocalPos.z = bars[i].barDirec.y * (j + 1) * floorManager.floorSpacing.y;

                    // Setting the local position.
                    bars[i].segments[j].transform.parent = transform;
                    bars[i].segments[j].transform.localPosition = segLocalPos;


                    // If (i) is greater or equal to bar count, this bar's segments should all be made inactive.
                    if (i >= barCount)
                    {
                        bars[i].segments[j].gameObject.SetActive(false);
                    }
                }

            }

            // Resets the alternation count.
            barAltCount = 0;

            // If 'reversed' is true, alternate the bars so that the bar enemy starts off properly.
            if (reversed)
                AlternateBars(0);
        }

        // Alternates the bars by putting in the number of rotations the bar is simulating.
        public void AlternateBars(int alts)
        {
            // Increases the bar alternation count and clamps it to reset the loop if applicable.
            barAltCount = alts % bars.Length;

            // The active value for the bars. A copy is kept to know how values will be shifted.
            bool[] barsActive = new bool[bars.Length];
            bool[] barsActiveDefault = new bool[bars.Length];

            // Default active values for the bars (no alternations).
            // Unused bars have active set to false.
            for (int i = 0; i < barCount; i++)
            {
                barsActive[i] = true;
                barsActiveDefault[i] = true;
            }

            // Recalculating bars active.
            for(int i = 0; i < bars.Length; i++)
            {
                // Gets the original value and offsets its position...
                // To get the new index.
                // If 'reversed' is true, the value shifts in the opposite direction.
                bool value = barsActiveDefault[i]; // Original value

                // The new index for the value.
                int newIndex = (i + barAltCount) % bars.Length;

                // Change Value
                barsActive[newIndex] = value; 
                
                // Updates the segments at the bar indicated by the new index.
                for(int j = bars[newIndex].segments.Count - 1; j >= 0; j--)
                {
                    bars[newIndex].segments[j].gameObject.SetActive(barsActive[newIndex]);
                }
            }

            // TODO: this isn't an efficient way to do it, so maybe try optimizing it later.
            // If the rotation order should be reversed.
            if(reversed)
            {
                // Reverse the active list.
                System.Array.Reverse(barsActive);

                // Recalculating bars active.
                for (int i = 0; i < bars.Length; i++)
                {
                    // Updates the segments at the bar indicated by the new index.
                    for (int j = 0; j < bars[i].segments.Count ; j++)
                    {
                        bars[i].segments[j].gameObject.SetActive(barsActive[i]);
                    }
                }
            }
        }

        // Alternates the bars by simulating the next alteration.
        public void AlternateBarsIncrement()
        {
            // Alternates the bar a certain number of times.
            AlternateBars(barAltCount + 1);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Set the alteration to 0.
            AlternateBars(0);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        // This function is called when the MonoBehaviour will be destroyed
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Goes through each bar.
            for(int i = 0; i < bars.Length; i++)
            {

                // Makes sure that the segments list exists (was initially used to address a null reference error)
                if (bars[i].segments != null)
                {
                    // Destroy all the segments.
                    for (int j = 0; j < bars[i].segments.Count; j++)
                    {
                        // Destroy the segment if it still exists.
                        if (bars[i].segments[j] != null)
                            Destroy(bars[i].segments[j].gameObject);
                    }
                }

                // Clear the segment list.
                bars[i].segments.Clear();
            }

            // Remove from the list.
            if (barEnemies.Contains(this))
                barEnemies.Remove(this);
        }
    }
}
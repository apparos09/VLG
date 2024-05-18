using System.Collections;
using System.Collections.Generic;
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

        // THe prefab for the bar segment.
        public Enemy barSegmentPrefab;

        // The number of bars for the enemy. Maximum 4 (4 directions)
        public int barCount = 1;

        // The number of segments per bar.
        public int segmentCount = 1;

        // The four bars for the enemy.
        private Bar[] bars = new Bar[4];

        // If 'true', the bars are angled diagonally ("x" shape).
        // If false, the bars are perpedicular to one another ("+" shape).
        public bool diagonal = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Generates the bars.
            for (int i = 0; i < bars.Length; i++)
            {
                // Generates a new bar, list of segments, and direction.
                bars[i] = new Bar();
                bars[i].segments = new List<Enemy>();
                bars[i].barDirec = new Vector2Int();
            }

            // Generates the bars.
            GenerateBars();
        }

        // Generates bars for the enemy.
        public void GenerateBars()
        {
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
                // Destroy all the segments.
                for(int j = 0; j < bars[i].segments.Count; j++)
                {
                    // Destroy the segment if it still exists.
                    if(bars[i].segments[j] != null)
                        Destroy(bars[i].segments[j].gameObject);
                }

                // Clear the segment list.
                bars[i].segments.Clear();
            }
        }
    }
}
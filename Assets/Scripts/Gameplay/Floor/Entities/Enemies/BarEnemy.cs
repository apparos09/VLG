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

        // The centre of the bar that gets rotated.
        [Tooltip("The bar centre that gets rotated.")]
        public GameObject barCenter;

        // Maximum number of bars for the enemy.
        private int BARS_COUNT_MAX = 4;

        // The number of bars for the enemy. Maximum 4 (4 directions)
        [Range(0, 4)]
        public int barCount = 1;

        // The number of segments per bar.
        public int segmentCount = 1;

        // The four bars for the enemy.
        private Bar[] bars = new Bar[4];

        // If 'true', the bars are angled diagonally ("x" shape).
        // If false, the bars are perpedicular to one another ("+" shape).
        [Tooltip("If true, the bars form a \"x\" shape. If false, the bars form a \"+\" shape. ")]
        public bool diagonal = false;

        // If 'true', the bars are rotated when the player moves.
        [Tooltip("If true, bars are rotated when the player moves")]
        public bool rotateBars = true;

        // If 'true', the alternation order is reversed.
        [Tooltip("If true, the bars are rotated in reverse (counter-clockwise)")]
        public bool reversed = false;

        // The rotation speed.
        public float rotationSpeed = 10.0F;

        // The rotation for the bar enemy's center.
        private float rotation = 0;

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

            // Set to this game object if the bar centre has not been set.
            if (barCenter == null)
                barCenter = gameObject;

            // Generates the bars.
            GenerateBars();

            // Add to the list.
            if (!barEnemies.Contains(this))
                barEnemies.Add(this);
        }

        // This function is called when the object has become enabled and active
        protected override void OnEnable()
        {
            base.OnEnable();

            // The enemy is enabled, so add it to the list.
            if (!barEnemies.Contains(this))
                barEnemies.Add(this);
        }

        // This function is called when the behaviour has become disabled or inactive
        protected override void OnDisable()
        {
            base.OnDisable();

            // The enemy is disabled, so remove it from the list.
            if (barEnemies.Contains(this))
                barEnemies.Remove(this);
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
                    segLocalPos.y = bars[i].segments[j].localYPos;
                    segLocalPos.z = bars[i].barDirec.y * (j + 1) * floorManager.floorSpacing.y;

                    // Setting the local position.
                    bars[i].segments[j].transform.parent = barCenter.transform;
                    bars[i].segments[j].transform.localPosition = segLocalPos;


                    // If (i) is greater or equal to bar count, this bar's segments should all be made inactive.
                    if (i >= barCount)
                    {
                        bars[i].segments[j].gameObject.SetActive(false);
                    }
                }

            }

            // Resets the alternation count.
            rotation = 0;
        }

        // Rotates the bars by the provided amount. If the element is re
        public void RotateBars(float degrees)
        {
            rotation += degrees;
        }

        // Rotates the bar by 45 degrees in the set direction.
        public void RotateBarsBy45Degrees()
        {
            float degrees = (reversed) ? -45 : 45;
            RotateBars(degrees);
        }

        // Rotates the bar by 90 degrees in the set direction.
        public void RotateBarsBy90Degrees()
        {
            float degrees = (reversed) ? -90 : 90;
            RotateBars(degrees);
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Resets the rotation of the bar.
            rotation = 0;
            barCenter.transform.rotation = Quaternion.identity;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // If the bars should rotate.
            if(rotateBars)
            {
                // Modulus operation to cap 360 degrees.
                rotation = rotation % 360.0F;

                // If the rotation is not set to 0.
                if (rotation != 0)
                {
                    // The rotation in degrees. Clamps it so that the rotation won't go over.
                    float rotDegrees = rotationSpeed * Time.deltaTime;
                    rotDegrees = Mathf.Clamp(rotDegrees, 0.0F, Mathf.Abs(rotation));

                    // Checks the rotation direction.
                    if (rotation > 0) // Counter-clockwise (Positive)
                    {
                        barCenter.transform.Rotate(Vector3.up, rotDegrees);
                        rotation -= rotDegrees;
                    }
                    else // Clockwise (Negative)
                    {
                        barCenter.transform.Rotate(Vector3.up, -rotDegrees);
                        rotation += rotDegrees;
                    }
                }
            }
            
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
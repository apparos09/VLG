using UnityEngine;


namespace VLG
{
    // The player for the game.
    public class Player : MonoBehaviour
    {
        // The gameplay manager.
        public GameplayManager gameManager;

        // Enables input from the player.
        public bool allowInput = true;

        // The floor position of the player (negative means the player isn't on the floor).
        public Vector2Int floorPos = new Vector2Int(-1, -1);

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance if this is null.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Sets to this player.
            if (gameManager.player == null)
                gameManager.player = this;

        }

        // Updates the player movements.
        public void UpdateInput()
        {
            // The move direction.
            Vector2Int moveDirec;

            // Player Movement
            if (Input.GetKeyDown("Up"))
            {
                moveDirec = Vector2Int.up;
            }
            else if (Input.GetKeyDown("Down"))
            {
                moveDirec = Vector2Int.down;
            }
            else if (Input.GetKeyDown("Left"))
            {
                moveDirec = Vector2Int.left;
            }
            else if (Input.GetKeyDown("Right"))
            {
                moveDirec = Vector2Int.right;
            }
            else // No Movement
            {
                 moveDirec = Vector2Int.zero;
            }


            // There is movement.
            if(moveDirec != Vector2.zero)
            {
                gameManager.TryPlayerMovement(moveDirec);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Updates the inputs from the player.
            if (allowInput)
                UpdateInput();
        }
    }
}
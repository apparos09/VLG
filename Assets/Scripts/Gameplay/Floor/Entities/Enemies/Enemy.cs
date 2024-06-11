using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A floor enemy.
    public class Enemy : FloorEntity
    {
        // The collider for the player.
        public new BoxCollider collider;

        // The rigidbody for the player.
        public new Rigidbody rigidbody;


        // If 'true', the level geometry is ignored for enemy movement.
        [Tooltip("If true, the enemy ignores the floor geometry for movement.")]
        public bool ignoreGeometry = false;

        // If 'true', the enemy can take damage from the player.
        [Tooltip("If true, the player can damage the enemy.")]
        public bool vulnerable = true;

        // The list of all enemies.
        private static List<Enemy> enemies = new List<Enemy>();



        // Awake is called when the script instance is being loaded
        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            group = entityGroup.enemy;

            // If the collider is not set.
            if (collider == null)
            {
                collider = GetComponent<BoxCollider>();
            }

            // If the rigidbody is not set.
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            // Add to the enemy list.
            if (!enemies.Contains(this))
                enemies.Add(this);
        }

        // Gets the enemy count.
        public static int GetEnemyCount()
        {
            return enemies.Count;
        }

        // Called when the player has attacked the enemy.
        public virtual void OnPlayerAttackHit(Player player)
        {
            if(vulnerable)
                KillEntity();
        }

        // Called when the entity is interacted with.
        public override void OnEntityInteract(FloorEntity entity)
        {
            // If the entity is a player.
            if(entity is Player)
            {
                // Gets the player.
                Player player = (Player)entity;

                // TODO: attack player.

                // Kill the player.
                player.KillEntity();
            }
        }

        // Kills the enemy.
        public override void KillEntity()
        {
            Destroy(gameObject);
        }

        // Runs the AI for the enemy.
        public virtual void RunAI()
        {
            // ...
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // The game is paused, so don't update anything.
            if (!gameManager.paused)
            {
                RunAI();
            }
                
        }

        // This function is called when the MonoBehaviour will be destroyed
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Checks for removal.
            bool removed = false;

            // Goes through every row and column to remove the enemy from the array.
            for(int r = 0; r < floorManager.floorEnemies.GetLength(0) && !removed; r++)
            {
                for (int c = 0; c < floorManager.floorEnemies.GetLength(1) && !removed; c++)
                {
                    if (floorManager.floorEnemies[r, c] == this)
                    {
                        floorManager.floorEnemies[r, c] = null;
                        removed = true;
                        break;
                    }
                }
            }

            // Remove from the enemy list.
            if (enemies.Contains(this))
                enemies.Remove(this);
        }

    }
}
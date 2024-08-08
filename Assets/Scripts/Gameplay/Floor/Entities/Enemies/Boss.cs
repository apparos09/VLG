using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A boss in the game.
    public class Boss : Enemy
    {
        [Header("Boss")]

        // The maximum health for the boss.
        public float maxHealth = 1;

        // The boss's health. A boss dies in a fixed amount of hits.
        public float health = 1;
        
        // The number of phases a boss has.
        public int phaseCount = 1;

        // The phase of the boss.
        public int phase = 1;

        // Used to call post start.
        private bool calledPostStart = false;

        // The list of all bosses.
        private static List<Boss> bosses = new List<Boss>();

        [Header("Boss/Animations")]

        // Animation for resetting the boss effect.
        public string effectResetAnim = "";

        // Animation for the boss taking damage.
        public string damageAnim = "";

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // The boss gets added to the list.
            if (!bosses.Contains(this))
                bosses.Add(this);

            // Set to max health on Start.
            health = maxHealth;
        }

        // Called on the first frame.
        protected virtual void PostStart()
        {
            calledPostStart = true;
        }

        // This function is called when the object has become enabled and active
        protected override void OnEnable()
        {
            base.OnEnable();

            // The boss is enabled, so add it to the list.
            if (!bosses.Contains(this))
                bosses.Add(this);
        }

        // This function is called when the behaviour has become disabled or inactive
        protected override void OnDisable()
        {
            base.OnDisable();

            // The boss is disabled, so remove it from the list.
            if (bosses.Contains(this))
                bosses.Remove(this);
        }

        // Gets the boss count.
        public static int GetBossCount()
        {
            return bosses.Count;
        }

        // Called when the player's attack hits.
        public override void OnPlayerAttackHit(Player player)
        {
            // If the enemy is vulnerable, kill them.
            if (vulnerable)
            {
                // Reduce health by 1.
                health--;

                // Is the boss dead?
                if(health <= 0)
                {
                    KillEntity();
                }
                else
                {
                    OnDamageTaken();
                }
                    
            }
        }

        // Called when an entity interacts with this entity.
        public override void OnEntityInteract(FloorEntity entity)
        {
            base.OnEntityInteract(entity);
        }


        // Called when damage has been taken.
        public virtual void OnDamageTaken()
        {
            // Plays the damage animation for the boss.
            if(damageAnim != string.Empty)
                animator.Play(damageAnim);

            // Plays the hurt sound effect.
            PlayHurtSfx();
        }

        // Called when the damage animation has started.
        public virtual void OnDamageAnimationStart()
        {
            // ...
        }

        // Called when the damage animation has finished.
        public virtual void OnDamageAnimationEnd()
        {
            // Plays the reset effect animation.
            if (effectResetAnim != string.Empty)
                animator.Play(effectResetAnim);
        }

        // Kills the entity. Called when the entity has been damaged.
        public override void KillEntity()
        {
            base.KillEntity();
        }

        // Resets the floor entity.
        public override void ResetEntity()
        {
            base.ResetEntity();

            // Set back to max.
            health = maxHealth;

            // Call post start again.
            calledPostStart = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            // If PostStart hasn't been called, call it.
            if(!calledPostStart)
            {
                PostStart();
            }

            base.Update();
        }

        // This function is called when the MonoBehaviour will be destroyed
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the boss list.
            if (bosses.Contains(this))
                bosses.Remove(this);
        }
    }
}
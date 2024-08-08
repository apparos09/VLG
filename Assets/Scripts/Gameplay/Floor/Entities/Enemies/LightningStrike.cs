using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // A lightning strike that appears during a floor.
    // NOTE: lighting is not part of any array.
    public class LightningStrike : Enemy
    {
        [Header("Lighting Strike")]
        // The effect and the target.
        public SpriteRenderer targetSprite;
        public SpriteRenderer effectSprite;

        // The lighting strike animation.
        public string strikeAnim = "Lightning Strike";

        // The lighting strike callback.
        public delegate void LightningStrikeCallback(LightningStrike strike);

        // The lighting strike start callback.
        private LightningStrikeCallback strikeStartCallback;

        // The lighting strike end callback.
        private LightningStrikeCallback strikeEndCallback;

        // The list of all lightning strikes.
        private static List<LightningStrike> lightningStrikes = new List<LightningStrike>();

        [Header("Audio")]

        [Tooltip("If 'true', the same sounds can be overlayed with one another. If false, only one is allowed to play at a time.")]
        public bool overlaySameSounds = true;

        // Thunder sound effect.
        public AudioClip thunderSfx;

        // The number of calls to play the thunder sfx.
        private static int thunderSfxCalls = 0;

        // Lightning osund effect.
        public AudioClip lightningStrikeSfx;

        // The number of calls to play lightning.
        private static int lightningStrikeSfxCalls = 0;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set parent to the floor origin if it's not set already. 
            if (transform.parent == null)
                transform.parent = floorManager.floorOrigin.transform;

            // Add to the lightning strike list.
            if (!lightningStrikes.Contains(this))
                lightningStrikes.Add(this);
        }

        // This function is called when the object has become enabled and active
        protected override void OnEnable()
        {
            base.OnEnable();

            // The lightning strike is enabled, so add it to the list.
            if (!lightningStrikes.Contains(this))
                lightningStrikes.Add(this);
        }

        // This function is called when the behaviour has become disabled or inactive
        protected override void OnDisable()
        {
            base.OnDisable();

            // The lightning strike is disabled, so remove it from the list.
            if (lightningStrikes.Contains(this))
                lightningStrikes.Remove(this);
        }

        // Strikes the lighting by playing the animation.
        public void TriggerLightningStrike()
        {
            // Play the strike anim.
            animator.Play(strikeAnim);
        }

        // Gets the lightning strike count.
        public static int GetLightningStrikeCount()
        {
            return lightningStrikes.Count;
        }

        // Strike the lighting at the provided position.
        public void TriggerLightningStrike(Vector2Int newFloorPos)
        {
            SetFloorPosition(newFloorPos, true, false);
            TriggerLightningStrike();
        }

        // Called when the lighting strike starts.
        public void OnLightingStrikeStart()
        {
            // The lighting strike start callback.
            if (strikeStartCallback != null)
                strikeStartCallback(this);
        }

        // Called when the lightning strike ends.
        public void OnLightingStrikeEnd()
        {
            // Reduce the amount.
            thunderSfxCalls--;
            lightningStrikeSfxCalls--;

            // Bounds check.
            if (thunderSfxCalls < 0)
                thunderSfxCalls = 0;

            // Bounds check.
            if (lightningStrikeSfxCalls < 0)
                lightningStrikeSfxCalls = 0;


            // The lighting strike end callback.
            if (strikeEndCallback != null)
                strikeEndCallback(this);
        }

        // CALLBACKS //
        // Lighting Strike Start Add Callback
        public void OnLightingStrikeStartAddCallback(LightningStrikeCallback callback)
        {
            strikeStartCallback += callback;
        }

        // Lighting Strike Start Remove Callback
        public void OnLightingStrikeStartRemoveCallback(LightningStrikeCallback callback)
        {
            strikeStartCallback -= callback;
        }

        // Lighting Strike Start Add Callback
        public void OnLightingStrikeEndAddCallback(LightningStrikeCallback callback)
        {
            strikeEndCallback += callback;
        }

        // Lighting Strike Start Remove Callback
        public void OnLightingStrikeEndRemoveCallback(LightningStrikeCallback callback)
        {
            strikeEndCallback -= callback;
        }

        // AUDIO
        // Plays the thunder sound.
        public void PlayThunderSfx()
        {
            // Sound effect is set.
            if (thunderSfx != null)
            {
                // If the same sounds can be overlayed.
                if(overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(thunderSfx);
                }
                else
                {
                    // No calls have been made, so allow the SFX to play.
                    if(thunderSfxCalls <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(thunderSfx);
                        thunderSfxCalls++;
                    }
                }
            }
        }

        // Plays the lightning strike sound.
        public void PlayLightningStrikeSfx()
        {
            // Sound effect is set.
            if (lightningStrikeSfx != null)
            {
                // If the same sounds can be overlayed.
                if (overlaySameSounds)
                {
                    gameManager.gameAudio.PlaySoundEffect(lightningStrikeSfx);
                }
                else
                {
                    // No calls have been made, so allow the SFX to play.
                    if (lightningStrikeSfxCalls <= 0)
                    {
                        gameManager.gameAudio.PlaySoundEffect(lightningStrikeSfx);
                        lightningStrikeSfxCalls++;
                    }
                }
            }
        }

        // This function is called when the MonoBehaviour will be destroyed
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the lightning strikes list.
            if (lightningStrikes.Contains(this))
                lightningStrikes.Remove(this);

            // If there are no lightning strikes left, reset the call counts.
            if (GetLightningStrikeCount() <= 0)
            {
                thunderSfxCalls = 0;
                lightningStrikeSfxCalls = 0;
            }
        }

    }
}
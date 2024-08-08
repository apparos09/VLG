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

        // Thunder sound effect.
        public AudioClip thunderSfx;

        // Lightning osund effect.
        public AudioClip lightningStrikeSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set parent to the floor origin if it's not set already. 
            if (transform.parent == null)
                transform.parent = floorManager.floorOrigin.transform;
        }



        // Strikes the lighting by playing the animation.
        public void TriggerLightningStrike()
        {
            // Play the strike anim.
            animator.Play(strikeAnim);
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

        // Audio
        // Plays the thunder sound.
        public void PlayThunderSfx()
        {
            if (thunderSfx != null)
                gameManager.gameAudio.PlaySoundEffect(thunderSfx);
        }

        // Plays the lightning strike sound.
        public void PlayLightningStrikeSfx()
        {
            if (lightningStrikeSfx != null)
                gameManager.gameAudio.PlaySoundEffect(lightningStrikeSfx);
        }

    }
}
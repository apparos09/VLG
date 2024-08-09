using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace VLG
{
    // The audio manager.
    public class GameAudio : MonoBehaviour
    {
        // The audio sources.
        public AudioSource bgmSource;
        public AudioSourceLooper bgmLooper;

        public AudioSource sfxSource;

        [Header("Clips")]
        // Button 1
        public AudioClip button01Sfx;

        // Button 2
        public AudioClip button02Sfx;

        // Slider Sfx
        public AudioClip sliderSfx;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // If the looper exists.
            if(bgmLooper != null)
            {
                // If the looper's audio source has not been set.
                if(bgmLooper.audioSource == null)
                {
                    // Set the audio source.
                    bgmLooper.audioSource = bgmSource;
                }
            }
        }

        // Plays the provided background music.
        // The arguments 'clipStart' and 'clipEnd' are used for the BGM looper.
        public void PlayBackgroundMusic(AudioClip bgmClip, float clipStart, float clipEnd)
        {
            // If the looper has been set, change it thorugh that.
            if(bgmLooper != null) 
            {
                // Stop the audio and set the clip. This puts the audio at its start.
                bgmLooper.StopAudio(true);
                bgmLooper.audioSource.clip = bgmClip;

                // Sets the start and end for the BGM.
                bgmLooper.clipStart = clipStart;
                bgmLooper.clipEnd = clipEnd;

                // Play the BGM through the looper
                bgmLooper.PlayAudio(true);
            }
            else // No looper, so change settings normally.
            {
                // Stops the BGM source and sets the current clip.
                bgmSource.Stop();
                bgmSource.clip = bgmClip;

                // Play the BGM with the normal settings.
                bgmSource.Play();
            }

        }

        // Plays the background music (clipStart and clipEnd are autoset to the start and end of the audio).
        public void PlayBackgroundMusic(AudioClip bgmClip)
        {
            PlayBackgroundMusic(bgmClip, 0, bgmClip.length);
        }

        // Plays the provided background music.
        // If 'stopAudio' is 'true', then the BGM is stopped before playing the one shot.
        public void PlayBackgroundMusicOneShot(AudioClip bgmClip, bool stopCurrAudio = true)
        {
            // If the current audio should be stopped.
            if (stopCurrAudio)
                bgmSource.Stop();

            bgmSource.PlayOneShot(bgmClip);
        }

        // Stops the provided background music.
        public void StopBackgroundMusic()
        {
            bgmSource.Stop();
        }

        // Plays the provided sound effect.
        public void PlaySoundEffect(AudioClip sfxClip)
        {
            // If the SFX is active and enabled, play the one shot.
            if(sfxSource.isActiveAndEnabled)
            {
                sfxSource.PlayOneShot(sfxClip);
            }
            else
            {
                // If it's in the editor, throw this message.
                // Unity provides a message anyway if you attempt to play a disabled audio source...
                // So this is to hide that.
                if(Application.isEditor)
                {
                    Debug.LogWarning("The SFX source is disabled, so it can't be played.");
                }
            }
                
        }

        // Stops the sound effect.
        public void StopSoundEffect() 
        { 
            sfxSource.Stop();
        }

        // Audio Clips
        // Button 1 Use
        public void PlayButton01Sfx()
        {
            PlaySoundEffect(button01Sfx);
        }

        // Button 2 Use
        public void PlayButton02Sfx()
        {
            PlaySoundEffect(button02Sfx);
        }

        // Slider Use Sfx
        public void PlaySliderSfx()
        {
            PlaySoundEffect(sliderSfx);
        }
    }
}
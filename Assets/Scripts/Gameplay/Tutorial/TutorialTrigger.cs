using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace VLG
{
    // Triggers a tutorial on start.
    public class TutorialTrigger : MonoBehaviour
    {
        // The game manager.
        public GameplayManager gameManager;

        // If the trigger is active, set to true.
        public bool activeTrigger = true;

        // The tutorial type.
        public Tutorials.tutorialType tutorial;

        // If 'true', the trigger checks every update to start the tutorial.
        // If false, it only checks in Start().
        public bool constantTrigger = false;

        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Try to load th tutorial on start.
            if (activeTrigger && gameManager.UsingTutorials)
                TryLoadTutorial();
        }

        // Tries to start the tutorial.
        public bool TryLoadTutorial()
        {
            // If this tutorial has not been cleared.
            if (!gameManager.tutorials.IsTutorialCleared(tutorial))
            {
                // Loads the provided tutorial.
                gameManager.tutorials.LoadTutorial(tutorial);
                return true;
            }

            // Tutorial load failed.
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            // If the trigger is constant.
            if(constantTrigger)
            {
                // If the trigger is active, and the game is using tutorials.
                if (activeTrigger && gameManager.UsingTutorials)
                {
                    // If this tutorial has not been cleared.
                    if (!gameManager.tutorials.IsTutorialCleared(tutorial))
                    {
                        // Loads the provided tutorial.
                        gameManager.tutorials.LoadTutorial(tutorial);
                    }
                }
            }
            
        }
    }
}
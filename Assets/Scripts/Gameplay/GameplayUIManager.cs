using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The gameplay UI manager.
    public class GameplayUIManager : MonoBehaviour
    {
        // The singleton instance.
        private static GameplayUIManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // Constructor
        private GameplayUIManager()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected virtual void Awake()
        {
            // If the instance hasn't been set, set it to this object.
            if (instance == null)
            {
                instance = this;
            }
            // If the instance isn't this, destroy the game object.
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Gets the instance.
        public static GameplayUIManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<GameplayUIManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("GameplayUIManager (singleton)");
                        instance = go.AddComponent<GameplayUIManager>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public static bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // The gameplay camera.
    public class GameplayCamera : MonoBehaviour
    {
        // The gameplay manager.
        public GameplayManager gameManager;

        // The camera component.
        public new Camera camera;

        // The current view.
        private int view = 0;

        // If 'true', view switching is allowed.
        public bool allowViewSwitching = true;

        // If 'true', view 1 is automatically set.
        // It's recommended that you leave this turned on in case the camera gets funky.
        public bool autoSetView1 = true;

        // Gets set to 'true' when post start is called.
        private bool calledPostStart = false;

        [Header("Keys")]
        // Primary switch key.
        public KeyCode switchKey = KeyCode.Alpha0;

        // Secondary switch key.
        public KeyCode switchKeyAlt = KeyCode.Keypad0;

        [Header("View 0/View 1")]

        // View 1 Position
        public Vector3 view1Position;

        // View 1 Rotation
        public Vector3 view1Rotation;


        [Header("View 2")]

        // View 2 Position
        public Vector3 view2Position;

        // View 2 Rotation
        public Vector3 view2Rotation;

        // Start is called before the first frame update
        void Start()
        {
            // Grabs the instance if it's not set.
            if (gameManager == null)
                gameManager = GameplayManager.Instance;

            // Tries to grab the camera component.
            if(camera == null)
            {
                camera = GetComponent<Camera>();
            }

            // If view 1 should be automatically set.
            if(autoSetView1)
            {
                view1Position = transform.position;
                view1Rotation = transform.eulerAngles;
            }

            // Sets the current view.
            SetView(0);
        }

        // Called on the first frame of the game.
        void PostStart()
        {
            // Makes sure the default view is set properly.
            SetView(0);

            // Post Sstart called.
            calledPostStart = true;
        }

        // Returns the view number
        public int GetView()
        {
            return view;
        }

        // Set the current view.
        public void SetView(int newView)
        {
            view = newView;

            switch(view)
            {
                default:
                case 1: // View 1
                    view = 1;
                    transform.position = view1Position;
                    transform.eulerAngles = view1Rotation;
                    break;

                case 2: // View 2
                    view = 2;
                    transform.position = view2Position;
                    transform.eulerAngles = view2Rotation;
                    break;
            }
        }

        // Swaps between the two views.
        public void SwapViews()
        {
            // The new view.
            int newView;

            // Switches the view.
            switch(view)
            {
                default:
                case 1:
                    newView = 2;
                    break;

                case 2:
                    newView = 1;
                    break;
            }

            // Sets the new view.
            SetView(newView);
        }

        // Update is called once per frame
        void Update()
        {
            // If post start has not been called.
            if (!calledPostStart)
                PostStart();
        }

    }
}
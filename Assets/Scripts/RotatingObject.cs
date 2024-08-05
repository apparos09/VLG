using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Constantly rotates an object.
    public class RotatingObject : MonoBehaviour
    {
        // If rotating should be enabled.
        public bool enabledRotation = true;

        // The rotation speed of the object.
        public float rotSpeed = 1.0f;

        // If 'true', a positive rotation is used.
        // If false, a negative rotation is used.
        [Tooltip("Determines if it's a positive or negative rotation.")]
        public bool positiveRot = true;

        [Header("Axes")]

        // Checks which values to rotate.
        public bool rotateX = false;
        public bool rotateY = false;
        public bool rotateZ = true;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // If rotating is enabled...
            if (enabledRotation)
            {
                // The rotation.
                Vector3 rotation = new Vector3();

                // Checks which ax
                rotation.x = (rotateX) ? rotSpeed * Time.deltaTime : 0;
                rotation.y = (rotateY) ? rotSpeed * Time.deltaTime : 0;
                rotation.z = (rotateZ) ? rotSpeed * Time.deltaTime : 0;

                // Rotates the object.
                transform.Rotate(rotation);
            }
            
        }
    }
}
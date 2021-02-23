using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Heli_Camera : MonoBehaviour, IHeliCamera
    {
        public Rigidbody rb;
        public Transform lookAtTarget;
        public float height = 2f;
        public float distance = -2f;

        public float smoothSpeed = 1f;

        private Vector3 wantedPos;
        private Vector3 refVelocity;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (rb)
            {
                UpdateCamera();
            }
        }


        public void UpdateCamera()
        {
            //Vector3 heliPos = rb.position;
            //Vector3 desiredPos = new Vector3(0, height, distance);
            //transform.position = heliPos + desiredPos;

            Vector3 flatForward = rb.transform.forward;
            flatForward.y = 0f;
            flatForward = flatForward.normalized;

            // Desired position
            wantedPos = rb.position + (flatForward * distance) + (Vector3.up * height);

            // Position the camera
            transform.position = Vector3.SmoothDamp(transform.position, wantedPos, ref refVelocity, smoothSpeed);
            // transform.position = wantedPos;
            transform.LookAt(lookAtTarget);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Heli_Characteristics : MonoBehaviour
    {
        #region Variables
        [Header("Lift Properties")]
        public float maxLiftForce = 10f;
        public Main_Rotor mainRotor;
        [Space]

        [Header("Tail Rotor Properties")]
        public float tailForce = 2f;

        public float cyclicTorque = 2f;
        public float cyclicForceMult = 100f;

        public float autoLevelForce = 2f;

        Vector3 flatForward;
        float forwardDot;
        Vector3 flatRight;
        float rightDot;
        #endregion


        public void UpdateCharacteristics(Rigidbody rb, Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);

            CalculateAngles();
            AutoLevel(rb);
        }

        protected virtual void HandleLift(Rigidbody rb, Input_Controller input)
        {
            //Debug.Log("Handling lift");
            if (mainRotor)
            {
                Vector3 liftForce = transform.up * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;
                float normalizedRPMs = mainRotor.CurrentRPMs / 50f;
                Debug.Log(normalizedRPMs);
                rb.AddForce(liftForce * Mathf.Pow(normalizedRPMs, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f), ForceMode.Force);
                
            }
        }

        protected virtual void HandleCyclic(Rigidbody rb, Input_Controller input)
        {
            float cyclicZForce = -input.CyclicInput.x * cyclicTorque;
            float cyclicXForce = input.CyclicInput.y * cyclicTorque;

            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);

            // Apply force based off of the Dot Product values
            Vector3 forwardVec = flatForward * forwardDot;
            Vector3 rightVec = flatRight * rightDot;
            Vector3 finalCyclicDir = Vector3.ClampMagnitude(forwardVec + rightVec, 1f) * (cyclicTorque * cyclicForceMult);

            rb.AddForce(finalCyclicDir, ForceMode.Force);
        }

        protected virtual void HandlePedals(Rigidbody rb, Input_Controller input)
        {
            //Debug.Log("Handling pedals");
            rb.AddTorque(Vector3.up * input.PedalInput * tailForce, ForceMode.Acceleration);
        }

        void CalculateAngles()
        {
            // Calculate the flat forward
            flatForward = transform.forward;
            flatForward.y = 0f;
            flatForward = flatForward.normalized;
            Debug.DrawRay(transform.position, flatForward, Color.blue);

            // Calculate the flat right
            flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            Debug.DrawRay(transform.position, flatRight, Color.red);

            // Calculate the offset angles
            forwardDot = Vector3.Dot(transform.up, flatForward);
            rightDot = Vector3.Dot(transform.up, flatRight);
        }

        void AutoLevel(Rigidbody rb)
        {
            float rightForce = -forwardDot * autoLevelForce;
            float forwardForce = rightDot * autoLevelForce;

            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }
    }

}

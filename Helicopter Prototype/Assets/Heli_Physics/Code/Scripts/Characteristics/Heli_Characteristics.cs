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
        #endregion


        public void UpdateCharacteristics(Rigidbody rb, Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
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
            //Debug.Log("Handling cyclic");
        }

        protected virtual void HandlePedals(Rigidbody rb, Input_Controller input)
        {
            //Debug.Log("Handling pedals");
        }
    }

}

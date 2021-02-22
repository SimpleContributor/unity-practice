using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace MyCode
{
    public class Rotor_Controller : MonoBehaviour
    {
        public Cyclic_Rotation cyclicRotation;
        public float maxDegPerSec = 320f;
        private List<IHeli_Rotor> rotors;


        private void Start()
        {
            rotors = GetComponentsInChildren<IHeli_Rotor>().ToList();
        }


        public void UpdateRotors(Input_Controller input, float currentRPMs)
        {
            // Debug.Log("Updating Rotor Controller");
            // degPerSec calculation
            float degPerSec = ((currentRPMs * 360f) / 60f) * Time.deltaTime;
            degPerSec = Mathf.Clamp(degPerSec, 0f, maxDegPerSec);

            // Update rotors
            if (rotors.Count > 0)
            {
                foreach(var rotor in rotors)
                {
                    rotor.UpdateRotor(degPerSec, input);
                }
            }

            cyclicRotation.UpdateCyclic(input);
        }
    }

}

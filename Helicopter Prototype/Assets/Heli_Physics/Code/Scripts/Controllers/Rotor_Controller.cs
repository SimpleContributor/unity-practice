using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace MyCode
{
    public class Rotor_Controller : MonoBehaviour
    {
        public Cyclic_Rotation cyclicRotation;

        private List<IHeli_Rotor> rotors;


        private void Start()
        {
            rotors = GetComponentsInChildren<IHeli_Rotor>().ToList<IHeli_Rotor>();
        }


        public void UpdateRotors(Input_Controller input, float currentRPMs)
        {
            // Debug.Log("Updating Rotor Controller");
            // degPerSec calculation
            float degPerSec = ((currentRPMs * 360) / 60) * Time.deltaTime;

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

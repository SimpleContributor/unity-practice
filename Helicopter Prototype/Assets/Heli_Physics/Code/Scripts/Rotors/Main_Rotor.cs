using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyCode
{
    public class Main_Rotor : MonoBehaviour, IHeli_Rotor
    {
        public Transform lRotor;
        public Transform rRotor;
        public float maxPitch = 35f;



        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void UpdateRotor(float degPerSec, Input_Controller input)
        {
            // Debug.Log("Updating Main Rotor.");
            // transform.rotation = Quaternion.Euler(0f, degPerSec, 0f);
            transform.Rotate(Vector3.up, degPerSec);

            // Pitch the blades up and down
            if (lRotor && rRotor)
            {
                lRotor.localRotation = Quaternion.Euler(input.CollectiveInput * maxPitch, 0f, 0f);
                rRotor.localRotation = Quaternion.Euler(-input.CollectiveInput * maxPitch, 0f, 0f);
            }
        }

        
    }

}

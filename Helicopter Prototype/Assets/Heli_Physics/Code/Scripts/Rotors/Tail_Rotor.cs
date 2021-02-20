using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyCode
{
    public class Tail_Rotor : MonoBehaviour, IHeli_Rotor
    {
        public float rotationSpeedModifier = 1.5f;

        public Transform lRotor;
        public Transform rRotor;
        public float maxPitch = 25f;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void UpdateRotor(float degPerSec, Input_Controller input)
        {
            // Debug.Log("Updating Tail Rotor.");
            transform.Rotate(Vector3.right, degPerSec * rotationSpeedModifier);

            if (lRotor && rRotor)
            {
                lRotor.localRotation = Quaternion.Euler(0f, input.PedalInput * maxPitch, 0f);
                rRotor.localRotation = Quaternion.Euler(0f, -input.PedalInput * maxPitch, 0f);
            }
        }
    }

}

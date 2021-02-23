using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Cyclic_Rotation : MonoBehaviour
    {
        public float cyclicPitch = 20f;
        public float cyclicRoll = -10f;

        public void UpdateCyclic(Input_Controller input)
        {
            transform.localRotation = Quaternion.Euler(input.CyclicInput.y * cyclicPitch, 0f, input.CyclicInput.x * cyclicRoll);

        }
    }

}

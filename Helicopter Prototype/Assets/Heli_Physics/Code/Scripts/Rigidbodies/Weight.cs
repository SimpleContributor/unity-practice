using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Weight : MonoBehaviour
    {
        #region Variables
        [Header("Weight Properties")]
        public float weightInLbs = 10f;

        public float weight;

        public float lbsToKg = 0.454f;
        public float kgToLbs = 2.205f;

        Rigidbody rb;
        #endregion


        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            float finalKG = weightInLbs * lbsToKg;
            weight = finalKG;
            rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.mass = weight;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        
        }
        #endregion
    }

}

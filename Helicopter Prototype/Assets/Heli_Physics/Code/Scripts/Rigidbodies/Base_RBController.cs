using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    [RequireComponent(typeof(Rigidbody))]
    // Base_RBController is meant to be the main
    public class Base_RBController : MonoBehaviour
    {
        #region Variables
        [Header("Base Properties")]
        public float weightInLbs = 10f;
        public Transform cog;

        public float lbsToKg = 0.454f;
        public float kgToLbs = 2.205f;

        protected Rigidbody rb;
        protected float weight;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        public virtual void Start()
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
            if (rb)
            {
                HandlePhysics();
            }
        }
        #endregion

        #region Custom Methods
        // Protected: only accessible within this class AND by derived class instances i.e. Heli_Controller
        // Virtual: allows method to be overridden by any class that inherits it
        // Override: extend or modify abstract/virtual inherited method, property, indexer, or event

        protected virtual void HandlePhysics()
        {

        }
        #endregion
    }

}

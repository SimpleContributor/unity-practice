using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCode
{
    public class Drag : MonoBehaviour
    {

        #region Variables 
        [Header("Drag Properties")]
        public float dragFactor = 0.05f;

        private Rigidbody rb;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float currentSpeed = rb.velocity.magnitude;
            float finalDrag = currentSpeed * dragFactor;
            rb.drag = finalDrag;
        }
        #endregion
    }

}

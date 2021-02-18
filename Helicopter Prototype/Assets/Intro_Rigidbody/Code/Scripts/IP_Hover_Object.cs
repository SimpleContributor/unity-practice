using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel
{
    [RequireComponent(typeof(Rigidbody))]
    public class IP_Hover_Object : MonoBehaviour
    {
        #region Variables
        [Header("Hover Object Properties")]
        public float hoverHeight;
        public Transform hoverPosition;
        public float hoverDamping = 0.5f;

        private Rigidbody rb;
        private float weight;
        private float currentGroundDist;
        private float randDragScale;
        private float randForceScale;
        #endregion


        #region Builtin Methods
        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if(rb)
            {
                weight = rb.mass * Physics.gravity.magnitude;
                randDragScale = rb.drag;
                randForceScale = 1f;
            }

            InvokeRepeating("SetRandomForces", 0f, 4f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb && hoverPosition)
            {
                CalculateGroundDistance();
                HandleHover();
                HandleTorque();
            }
        }
        #endregion



        #region Custom Methods
        void CalculateGroundDistance()
        {
            Ray ray = new Ray(hoverPosition.position, Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100f))
            {
                if(hit.transform.tag == "ground")
                {
                    currentGroundDist = hit.distance;
                    //Debug.Log(currentGroundDist);
                }
            }
        }

        void HandleHover()
        {
            float forceDifference = hoverHeight - currentGroundDist;
            Vector3 finalForce = new Vector3(0f, 1 + forceDifference, 0f) * randForceScale;

            rb.AddForce(finalForce * weight);
            rb.drag = rb.velocity.magnitude * (Mathf.Pow(hoverDamping, 2f) * randDragScale);
        }

        void HandleTorque()
        {
            //float wobbleX = Mathf.Sin(Time.time * 100f);
            float wobbleZ = Mathf.Sin(Time.time * Time.deltaTime);
            float wobbleX = Mathf.Cos(Time.time * Time.deltaTime);

            //Vector3 wobbleVec = new Vector3(0f, 4f, 0f);
            Vector3 wobbleVec = new Vector3(0f, 0f, 0f);
            Vector3 wobbleScale = Vector3.right * 2f;
            Debug.DrawRay(transform.position, wobbleVec.normalized * 2f, Color.green);

            Vector3 torqueForce = wobbleScale;
            rb.AddTorque(torqueForce);
        }

        void SetRandomForces()
        {
            float wantedDragScale = Random.Range(0.9f, 1.1f);
            randDragScale = Mathf.Lerp(randDragScale, wantedDragScale, Time.deltaTime * 4f);

            float wantedForceScale = Random.Range(0.9f, 1.1f);
            randForceScale = Mathf.Lerp(randForceScale, wantedForceScale, Time.deltaTime * 4f);
            //Debug.Log(randDragScale);
        }
        #endregion
    }
}

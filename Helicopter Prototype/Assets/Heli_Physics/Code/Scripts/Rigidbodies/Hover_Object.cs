using UnityEngine;


namespace MyCode
{
    public class Hover_Object : MonoBehaviour
    {
        #region Variables
        [Header("Hover Object Properties")]
        public float hoverHeight;
        public Transform hoverPosition;
        public float hoverDamping = 0.5f;

        Rigidbody rb;
        float weight;
        float currentGroundDist;
        float randDragScale;
        float randForceScale;
        #endregion


        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb)
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
            CalculateGroundDistance();
            HandleHover();
            HandleTorque();
        }
        #endregion


        #region Custom Methods
        void CalculateGroundDistance()
        {
            Ray ray = new Ray(hoverPosition.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.tag == "ground")
                {
                    currentGroundDist = hit.distance;
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
            float wobbleZ = Mathf.Sin(Time.time * Time.deltaTime);
            float wobbleX = Mathf.Cos(Time.time * Time.deltaTime);

            Vector3 wobbleVec = new Vector3(0f, 0f, 0f);
            Vector3 wobbleScale = Vector3.right * 2f;

            Vector3 torqueForce = wobbleScale;
            rb.AddTorque(torqueForce);
        }

        void SetRandomForces()
        {
            float wantedDragScale = Random.Range(0.9f, 1.1f);
            randDragScale = Mathf.Lerp(randDragScale, wantedDragScale, Time.deltaTime * 4f);

            float wantedForceScale = Random.Range(0.9f, 1.1f);
            randForceScale = Mathf.Lerp(randForceScale, wantedForceScale, Time.deltaTime * 4f);
        }
        #endregion
    }

}

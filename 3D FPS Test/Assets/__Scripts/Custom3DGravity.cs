using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom3DGravity : MonoBehaviour
{
    [Header("Gravity Properties")]
    public float gravityScale = 1.0f;
    // static var can only be altered directly through Unity Inspector
    public static float globalGravity = -9.81f;

    Rigidbody m_rb;


    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }


    // Called when the object becomes enabled and active
    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
}

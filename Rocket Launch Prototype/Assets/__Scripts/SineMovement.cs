using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    public float waveFrequency = 6.0f;
    public float waveWidth = 10.0f;

    private float z0;

    // Start is called before the first frame update
    void Start()
    {
        z0 = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 tempPos = transform.position;
        float age = Time.time;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.z = z0 + waveWidth * sin;
        transform.position = tempPos;
    }
}

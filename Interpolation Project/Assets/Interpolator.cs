using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolator : MonoBehaviour
{
    public Vector3 p0 = new Vector3(0, 0, 0);
    public Vector3 p1 = new Vector3(3, 4, 5);
    public float timeDuration = 1;

    public bool checkToCalculate = false;

    public bool ___________________________;

    public Vector3 p01;
    public bool moving = false;
    public float timeStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkToCalculate)
        {
            checkToCalculate = false;

            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                moving = false;
            }

            // Standard linear interpolation function
            p01 = (1 - u) * p0 + u * p1;

            transform.position = p01;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    #region Variables
    [Header("Sine Wave Properties")]
    public float waveFrequency = 6.0f;
    public float waveWidth = 10.0f;

    [Header("Sine Wave Axis")]
    public string axis;

    private float x0, y0, z0;
    #endregion



    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        x0 = transform.position.x;
        y0 = transform.position.y;
        z0 = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    #endregion



    #region Custom Methods
    public void Movement()
    {
        Vector3 tempPos = transform.position;
        float age = Time.time;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);

        if (axis == "x")
        {
            tempPos.x = x0 + waveWidth * sin;
            transform.position = tempPos;
        } else if (axis == "z")
        {
            tempPos.z = z0 + waveWidth * sin;
            transform.position = tempPos;
        } else
        {
            tempPos.y = y0 + waveWidth * sin;
            transform.position = tempPos;
        }
    }
    #endregion
}

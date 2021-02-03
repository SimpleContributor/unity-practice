using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenosFollower : MonoBehaviour
{
    public GameObject poi;
    public float u = 0.1f;
    public Vector3 p0, p1, p01; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the position of this and the poi
        p0 = this.transform.position;
        p1 = poi.transform.position;

        p01 = (1 - u) * p0 + u * p1;

        this.transform.position = p01;
    }
}

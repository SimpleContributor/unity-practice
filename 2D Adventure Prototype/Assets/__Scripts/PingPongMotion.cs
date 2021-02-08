using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMotion : MonoBehaviour
{
    public Vector3 MoveAxes = Vector2.zero;
    public float Distance = 3f;

    private Vector3 OrigPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        OrigPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = OrigPos + MoveAxes * Mathf.PingPong(Time.time, Distance);
    }
}

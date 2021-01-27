using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    // This will allow the Boid script to access all the values i.e. S.numBoids
    static public BoidSpawner S;

    public int numBoids = 100;
    public GameObject boidPrefab;
    public float spawnRadius = 100f;
    public float spawnVelocity = 10f;
    public float minVelocity = 0f;
    public float maxVelocity = 35f;
    public float nearDist = 30f;
    public float collisionDist = 5f;
    public float velocityMatchingAmt = 0.01f;
    public float flockCenteringAmt = 0.15f;
    public float collisionAvoidanceAmt = -0.5f;
    public float mouseAttractionAmt = 0.01f;
    public float mouseAvoidanceAmt = 0.75f;
    public float mouseAvoidanceDist = 15f;
    public float velocityLerpAmt = 0.25f;

    public bool __________________;

    public Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        for (int i = 0; i < numBoids; i++)
        {
            Instantiate(boidPrefab);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Track the mouse position. This keeps it the same for all Boids
        Vector3 mousePos2d = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, this.transform.position.y);

        mousePos = this.GetComponent<Camera>().ScreenToWorldPoint(mousePos2d);
    }
}

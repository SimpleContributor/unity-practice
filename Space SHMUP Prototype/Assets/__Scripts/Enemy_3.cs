using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    // Enemy_3 will move following a Bezier curve
    public Vector3[] points;
    public float birthTime;
    public float lifeTime = 10;

    private void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[3]; // Init points

        // The start position has already been set by Main.SpawnEnemy()
        points[0] = pos;

        // Set xMin and xMax the same way that Main.SpawnEnemy() does
        float xMin = Utils.CamBounds.min.x + Main.S.enemySpawnPadding;
        float xMax = Utils.CamBounds.max.x - Main.S.enemySpawnPadding;

        Vector3 v;
        // Pick a random middle position in the bottom half of the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = Random.Range(Utils.CamBounds.min.y, 0);
        points[1] = v;

        // Pick random final position above the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = pos.y;
        points[2] = v;

        birthTime = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        // Interpolate the three Bezier curve points
        Vector3 p01, p12;

        // Adds easing to increase Enemy_3 velocity in the middle of the curve
        //  rather than slowing down (usual behavior of Beziers)
        u = u - 0.25f * Mathf.Sin(u * Mathf.PI * 2);

        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;

 
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}

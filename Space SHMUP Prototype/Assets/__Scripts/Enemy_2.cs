using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    // Enemy_2 uses a Sin wave to modify a 2-point linear interpolation
    public Vector3[] points;
    public float birthTime;
    public float lifeTime = 10;
    public float sinEccentricity = 0.6f;



    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[2];

        // Find Utils.camBounds
        Vector3 cbMin = Utils.CamBounds.min;
        Vector3 cbMax = Utils.CamBounds.max;

        Vector3 v = Vector3.zero;
        // Pick any point on the left side of the screen
        v.x = cbMin.x - Main.S.enemySpawnPadding;
        v.y = Random.Range(cbMin.y, cbMax.y);
        points[0] = v;

        // Pick any point on the right side of the screen
        v = Vector3.zero;
        v.x = cbMax.x + Main.S.enemySpawnPadding;
        v.y = Random.Range(cbMin.y, cbMax.y);
        points[1] = v;

        // Possibly swap sides
        if (Random.value < 0.5f)
        {
            // Setting the .x of each point to its negative will mave it to the other side of screen
            points[0].x *= -1;
            points[1].x *= -1;
        }

        birthTime = Time.time;
    }

    // Update is called once per frame


    public override void Move()
    {
        // Bezier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        // Interpolate the two linear interpolation points
        pos = (1 - u) * points[0] + u * points[1];

        base.Move();
    }
}

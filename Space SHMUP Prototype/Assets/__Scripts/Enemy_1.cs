using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy_1 extends the enemy class
public class Enemy_1 : Enemy
{
    // # of seconds to complete a full sine wave
    public float waveFrequency = 2;
    // sine wave width in meters
    public float waveWidth = 4;
    public float waveRotY = 45;

    private float x0 = -12345;
    private float birthTime;



    // Start is called before the first frame update
    void Start()
    {
        // Set x0 to the initial x position of Enemy_1
        // This works fine because the position will have already been
        // set by Main.SpawnEnemy() before Start() runs
        // This is also good because there is no Start() method on Enemy
        x0 = pos.x;

        birthTime = Time.time;
    }

    public override void Move()
    {
        // Because pos is a property, you can't directly set pos.x
        Vector3 tempPos = pos;
        // theta adjusts based on time
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        // Rotate a bit about y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        // base.Move() still handles the movement down the Y
        base.Move();
    }


}

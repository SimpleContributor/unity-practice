using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    public bool __________________;

    public Bounds bounds;
    public Vector3 boundsCenterOffset;

    private void Awake()
    {
        InvokeRepeating("CheckOffscreen", 0f, 2f);
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void CheckOffscreen()
    {
        // If bounds are still their default value...
        if (bounds.size == Vector3.zero)
        {
            // then set them
            bounds = Utils.CombineBoundsOfChildren(this.gameObject);
            // Also find the diff between bounds.center & transform.position
            boundsCenterOffset = bounds.center - transform.position;
        }

        // Every time, update the bounds to the current position
        bounds.center = transform.position + boundsCenterOffset;
        // Check to see whether the bounds are completely offscreen
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen);
        if (off != Vector3.zero)
        {
            if (off.y < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

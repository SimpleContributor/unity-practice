﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Part
{
    public string name;
    public float health;
    public string[] protectedBy;

    public GameObject go;
    public Material mat;
}

public class Enemy_4 : Enemy
{
    // Enemy_4 will start offscreen and then pick a random point on screen to move to.
    //  Once it has arrived, it will pick another random point and continue until destroyed
    public Vector3[] points; // Stores the p0 and p1 for interpolation
    public float timeStart;
    public float duration = 4;

    public Part[] parts;


    private void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[2];

        points[0] = pos;
        points[1] = pos;

        InitMovement();

        // Cache GomeObject and Material of each Part in parts
        Transform t;
        foreach (Part prt in parts)
        {
            t = transform.Find(prt.name);
            if (t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
            }
        }
    }


    void InitMovement()
    {
        // Pick a new point to move to on the screen
        Vector3 p1 = Vector3.zero;
        float esp = Main.S.enemySpawnPadding;
        Bounds cBounds = Utils.CamBounds;
        p1.x = Random.Range(cBounds.min.x + esp, cBounds.max.x - esp);
        p1.y = Random.Range(cBounds.min.y + esp, cBounds.max.y - esp);

        points[0] = points[1]; // Shift points[1] to points[0]
        points[1] = p1;

        // Reset the time
        timeStart = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2); // Apply Ease Out easing to u

        pos = (1 - u) * points[0] + u * points[1]; // Simple linear interpolation
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch(other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();

                // Hero Projectiles get destroyed when they leave the screen and hit an enemy
                bounds.center = transform.position + boundsCenterOffset;
                if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen) != Vector3.zero)
                {
                    Destroy(other);
                    break;
                }


                // Hurt this enemy
                // Find the GameObject that was hit
                // The Collision collision has contacts[], an array of ContactPoints
                // Because the enemy was hit, we are guaranteed to have at least one contacts[0], and
                //  contact points have a ref to thisCollider, which will be the coll for the part that was hit
                GameObject goHit = collision.contacts[0].thisCollider.gameObject;
                Part prtHit = FindPart(goHit);
                if (prtHit == null)
                {
                    goHit = collision.contacts[0].otherCollider.gameObject;
                    prtHit = FindPart(goHit);
                }

                // Check whether this part is still protected
                if (prtHit.protectedBy != null)
                {
                    foreach (string s in prtHit.protectedBy)
                    {
                        if (!Destroyed(s))
                        {
                            Destroy(other);
                            return;
                        }
                    }
                }

                // It's not protected so damage the part
                prtHit.health -= Main.W_DEFS[p.type].damageOnHit;
                // Show damage on the part
                ShowLocalizedDamage(prtHit.mat);

                if (prtHit.health <= 0)
                {
                    prtHit.go.SetActive(false);
                }

                // Check to see if the whole ship is destroyed
                bool allDestroyed = true;
                foreach (Part prt in parts)
                {
                    if (!Destroyed(prt))
                    {
                        allDestroyed = false;
                        break;
                    }
                }

                if (allDestroyed)
                {
                    Main.S.ShipDestroyed(this);
                    Destroy(this.gameObject);
                }

                Destroy(other);
                break;
        }
    }


    Part FindPart(string n)
    {
        foreach(Part prt in parts)
        {
            if (prt.name == n)
            {
                return (prt);
            }
        }
        return (null);
    }

    Part FindPart(GameObject go)
    {
        foreach(Part prt in parts)
        {
            if(prt.go == go)
            {
                return (prt);
            }
        }

        return (null);
    }


    // These functions return true if the Part has been destroyed
    bool Destroyed(GameObject go)
    {
        return (Destroyed(FindPart(go)));
    }
    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }
    bool Destroyed(Part prt)
    {
        if (prt == null)
        {
            return (true);
        }

        return (prt.health <= 0);
    }

    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        remainingDamageFrames = showDamageForFrames;
    }


    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }
}

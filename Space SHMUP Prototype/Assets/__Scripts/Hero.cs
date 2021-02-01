﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;
    public float gameRestartDelay = 2f;

    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -40;
    public float pitchMult = 25;

    // Ship status info
    [SerializeField] private float _shieldLevel = 1;

    public bool _______________________;

    public Bounds bounds;
    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate
    public WeaponFireDelegate fireDelegate;

    private void Awake()
    {
        S = this;
        bounds = Utils.CombineBoundsOfChildren(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        // Pull in info from the input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        bounds.center = transform.position;

        // Keep the ship constrained to the screen bounds
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.onScreen);
        if (off != Vector3.zero)
        {
            pos -= off;
            transform.position = pos;
        }

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Use the fireDelegatte to fire Weapons
        // First, make sure the Axis("Jump") button is pressed
        // Then ensure that fireDelegate ins't null to avoid an err
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }



    public GameObject lastTriggerGo = null;

    private void OnTriggerEnter(Collider other)
    {
        // Find the tag of other.gameObject or its parent GameObjects
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        // If there is a parent with a tag
        if (go != null)
        {
            if (go == lastTriggerGo) return;
            lastTriggerGo = go;

            if (go.tag == "Enemy")
            {
                shieldLevel--;
                Destroy(go);
            } else
            {
                print("Triggered: " + go.name);

            }

        } else
        {
            print("Triggered: " + other.gameObject.name);
        }
    }



    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}

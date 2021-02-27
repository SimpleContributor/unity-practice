﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    PlayerController player;

    public Vector2 moveDir = new Vector2(1f, 1f);
    public float rotSpeed = 1f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveDir;
        rb.rotation += rotSpeed;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        player.anims.SetBool("Alive", false);
    //        Destroy(this.gameObject, 0.5f);
    //    }
    //}
}

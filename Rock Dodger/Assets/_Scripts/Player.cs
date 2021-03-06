﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 10f;

    float horz;
    float screenEdge;

    // Start is called before the first frame update
    void Start()
    {
        screenEdge = (Camera.main.aspect * Camera.main.orthographicSize) + (transform.localScale.x / 2);
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");

        //ScreenWrap();
        ScreenWrap();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // rb.AddForce(new Vector2(horz * playerSpeed * Time.deltaTime, 0f));
        //Vector2 tempPos = rb.position;
        //tempPos.x += horz * playerSpeed * Time.deltaTime;
        //rb.position = tempPos;
        transform.Translate(Vector2.right * playerSpeed * horz * Time.deltaTime);
    }

    void ScreenWrap()
    {
        
        if (transform.position.x > screenEdge)
        {
            transform.position = new Vector2(-screenEdge, transform.position.y);
        }
        else if (transform.position.x < -screenEdge)
        {
            transform.position = new Vector2(screenEdge, transform.position.y);
        }
        
    }

    //void ScreenStay()
    //{
    //    float screenEdge = Camera.main.orthographicSize / 2;
    //    if (rb.transform.position.x > screenEdge)
    //    {
    //        Vector2 tempPos = new Vector2(screenEdge, rb.transform.position.y);
    //        rb.transform.position = tempPos;
    //    }
    //    else if (rb.transform.position.x < -screenEdge)
    //    {
    //        Vector2 tempPos = new Vector2(-screenEdge, rb.transform.position.y);
    //        rb.transform.position = tempPos;
    //    }

    //}
}

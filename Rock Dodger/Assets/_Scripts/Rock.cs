﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb;
    public float rockSpeed;
    float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenHeight = (Camera.main.aspect * Camera.main.orthographicSize) / 9 * 16;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.transform.Translate(Vector2.down * rockSpeed * Time.deltaTime);
        if (transform.position.y < -(screenHeight + transform.localScale.y))
        {
            Destroy(this.gameObject);
        }
    }
}

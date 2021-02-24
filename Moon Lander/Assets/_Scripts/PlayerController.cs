using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float crashForce = 2.2f;

    public float thrustForce = 20f;
    public float rotForce = 2f;
    float vInput, hInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleThrust();
        HandleRotation();
    }

    void HandleThrust()
    {
        vInput = Input.GetAxisRaw("Vertical");
        vInput = Mathf.Clamp01(vInput);
        //rb.velocity = transform.up * vInput * thrustForce * Time.fixedDeltaTime;

        rb.AddRelativeForce(new Vector2(0, vInput * thrustForce));
    }

    void HandleRotation()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        rb.transform.Rotate(Vector3.forward, hInput * rotForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Touching ground.");
            Debug.Log(collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude > crashForce)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

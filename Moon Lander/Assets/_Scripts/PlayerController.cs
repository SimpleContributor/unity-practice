using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxColl;
    GameController gameController;

    public Animator anims;

    [Header("Rocket Properties")]
    public float crashForce = 2.2f;
    public float thrustForce = 20f;
    public float rotForce = 2f;

    float vInput, hInput;
    bool thrusting = false;

    float screenWidth;
    float screenHeight;
    float playerHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = 0;

        boxColl = GetComponent<BoxCollider2D>();
        anims = GetComponent<Animator>();

        gameController = FindObjectOfType<GameController>();

        float camDimensions = Camera.main.aspect * Camera.main.orthographicSize;
        playerHeight = transform.localScale.y / 2;
        screenWidth = camDimensions + playerHeight;
        screenHeight = (camDimensions - playerHeight) / 9 * 16;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleThrust();
        HandleRotation();
        ScreenWrap();
    }

    void HandleThrust()
    {
        vInput = Input.GetAxisRaw("Vertical");
        if (vInput > 0)
        {
            // If we are thrusting
            rb.AddRelativeForce(new Vector2(0, vInput * thrustForce));
            anims.SetBool("Thrusting", true);
            thrusting = true;
        }
        else if (vInput == 0 && thrusting)
        {
            anims.SetBool("Thrusting", false);
            thrusting = false;
        }
    }

    void HandleRotation()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        // Torque will apply a rotational force that will continue to spin without
        // an opposing force. Angular drag will bring the rotation to a stop giving the 
        // ship a more outerspace feel
        rb.AddTorque(rotForce * hInput);

        // Same behavior as rb.transform.Rotate except it should be used for 2D and .Rotate() for 3D
        //rb.rotation += hInput * rotForce * Time.fixedDeltaTime;
    }

    void ScreenWrap()
    {
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector2(-screenWidth, transform.position.y);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector2(screenWidth, transform.position.y);
        }

        if (transform.position.y > screenHeight)
        {
            transform.position = new Vector2(transform.position.x, screenHeight);
        }
        else if (transform.position.y < -screenHeight)
        {
            transform.position = new Vector2(transform.position.x, -screenHeight);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Force in Newtons (mass / 2 * velocity * velocity)
        float collForce = (rb.mass / 2) * Mathf.Pow(collision.relativeVelocity.magnitude, 2);

        if (collision.gameObject.tag == "Ground")
        {
            if (collision.relativeVelocity.magnitude > crashForce)
            {
                Die();
            }

            Success();
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Die();
        }
    }

    void Success()
    {
        Debug.Log("Successful landing!!!");
    }

    void Die()
    {
        boxColl.isTrigger = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        anims.SetBool("Alive", false);
        Destroy(this.gameObject, 1.3f);
        gameController.ResetLevel();
    }   
}

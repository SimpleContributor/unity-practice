using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovement : MonoBehaviour
{
    public Camera playerCam;
    public GameObject playerBean;

    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    Rigidbody rb;
    public float moveSpeed = 50f;
    public float moveMult = 1f;
    public float jumpForce = 10f;
    ForceMode forceMode;

    public float threshold = 0.01f;
    public float counterMovement = 0.175f;
    public float slideCounterMovement = 0.2f;

    float horz, vert;

    public float mouseSensitivity = 50f;
    float xRot = 0f;

    bool onGround;
    bool jumping = false;
    bool crouching = false;

    public Vector3 crouchScale = new Vector3(1f, 0.6f, 1f);
    Vector3 playerScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScale = transform.localScale;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        UserInputs();
        Look();

        if (!onGround)
        {
            moveMult = 0.01f;
        }
        else
        {
            moveMult = 1f;
        }
    }

    void UserInputs()
    {
        // Handles Keyboard inputs for walking, crouching and jumping
        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        } 
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }

        if (jumping && onGround)
        {
            Jump();
        }
    }


    void StartCrouch()
    {

        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        moveMult = 0.5f;
    }


    void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        moveMult = 1f;
    }

    void Look()
    {
        float camRotY = playerCam.transform.rotation.y;
        // Handles Mouse Movements
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Look up and down
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // Look left and right
        Vector3 currentRot = playerCam.transform.localRotation.eulerAngles;
        float desiredYRot = currentRot.y + mouseX;

        playerCam.transform.localRotation = Quaternion.Euler(xRot, desiredYRot, 0f);
        playerBean.transform.localRotation = Quaternion.Euler(0f, desiredYRot, 0f);
        Debug.Log(new Vector3(8, 4, 40).normalized);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Movement();
        Vector2 mag = FindVelRelativeToLook();
        CounterMovement(horz, vert, mag);
    }

    void Movement()
    {
        if ((onGround && jumping) || !onGround)
        {
            forceMode = ForceMode.Force;
        }
        else
        {
            forceMode = ForceMode.Impulse;
        }
        
        if (!crouching)
        {
            rb.AddForce(playerBean.transform.right * horz * moveSpeed * moveMult, forceMode);
            rb.AddForce(playerBean.transform.forward * vert * moveSpeed * moveMult, forceMode);

        }
    }





    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = playerBean.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        // moveAngle is same as 90 - Mathf.Atan2(z, x)

        float moveLookDif = Mathf.DeltaAngle(lookAngle, moveAngle);
        //Debug.Log(moveLookDif);
        float v = 90f - moveLookDif;

        float magnitue = rb.velocity.magnitude;
        Debug.Log(magnitue);
        float yMag = magnitue * Mathf.Cos(moveLookDif * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        // same as xMag = magnitude * Mathf.Sin(moveLookDif * Mathf.Deg2Rad);
        // Cos(90 - theta) same as Sin(theta)
        // Sin(90 - theta) same as Cos(theta)

        return new Vector2(xMag, yMag);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!onGround || jumping) return;

        //Slow down sliding
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * playerBean.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > threshold && Mathf.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * playerBean.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > moveSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }
}

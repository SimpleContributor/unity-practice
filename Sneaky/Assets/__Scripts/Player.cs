using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float smoothMoveTime = 0.1f;
    public float rotSpeed = 5f;

    float smoothInputMag;
    float smoothMoveVel;

    float horz, vert;
    float angle, inputMag;
    float sqrPlayerSpeed;

    Rigidbody rb;
    Vector3 inputDir;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrPlayerSpeed = playerSpeed * playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");
        vert = -Input.GetAxisRaw("Vertical");
        inputDir = new Vector3(horz, 0, vert);
        Move();
        Rotate();
        CapDiagMove();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rb.MovePosition(rb.position + velocity * Time.deltaTime);

        if (rb.velocity.sqrMagnitude > sqrPlayerSpeed)
        {
            rb.velocity = rb.velocity.normalized * playerSpeed;
        }
    }

    void Move()
    {
        // Moving diagonal will make the input.mag about 1.4 so the player will move 40% faster
        // diagonally without this if statement. It just resets the vector to 1
        inputMag = inputDir.magnitude;
        if (inputMag > 1.0f)
        {
            inputMag = inputDir.normalized.magnitude;
        }
        smoothInputMag = Mathf.SmoothDamp(smoothInputMag, inputMag, ref smoothMoveVel, smoothMoveTime);
        velocity = transform.forward * playerSpeed * smoothInputMag;

        //transform.Translate(transform.forward * playerSpeed * smoothInputMag * Time.deltaTime, Space.World);
    }

    void Rotate()
    {
        // The direction the player is moving
        Vector3 dirToLook = new Vector3(horz, 0f, vert).normalized;
        // The direction the player is moving as an angle in degrees
        float targetAngle = Mathf.Atan2(dirToLook.z, dirToLook.x) * Mathf.Rad2Deg;
        // Set the new angle of the player rotation to Lerp from the current angle to the target 
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * rotSpeed * inputMag);

        //transform.eulerAngles = Vector3.up * angle;
    }

    void CapDiagMove()
    {

    }
}

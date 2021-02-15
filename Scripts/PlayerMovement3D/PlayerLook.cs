using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Return floats that get the mouse movement along each axis and multiply by public mouseSensitivity
        // and by Time.deltaTime
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Take the current xRotation (starts at 0f to look stright ahead)
        // Subtract the current mouseY value to get the new rotation. If the user looks up we subtract
        // the positive number to get the proper negative rotation value needed to look up
        xRotation -= mouseY;
        // Clamp so the user can only rotate between 180 degrees and not 360 and break our neck.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // This will rotate up or down based on the local rotation of the camera
        // Could apply this same method to the z axis for the player to tilt head
        // for wall running or peeking corners. Might need to rotate on a pivot point
        // to make it feel like the head is moving on a neck.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

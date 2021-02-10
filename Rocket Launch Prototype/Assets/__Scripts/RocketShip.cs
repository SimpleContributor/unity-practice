using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    Rigidbody myRigBody;
    AudioSource myAudioSource;

    public float shipSpeed = 100f;
    public float rotSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        myRigBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    
    private void LateUpdate()
    {
        // Stops the ship from continuing to rotate when it hits or is hit my another object
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }


    private void FixedUpdate()
    {
        Fly();
        Rotate();
    }

    public void Fly()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Flying");
            myRigBody.AddRelativeForce(Vector3.up * shipSpeed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAudioSource.Play();
        } else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            myAudioSource.Stop();
        }
    }

    public void Rotate()
    {
        myRigBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Rotate Right");
            //Quaternion rot = transform.rotation;
            //rot = Quaternion.Euler(0, 0, 0 + rotSpeed);
            //transform.rotation = rot;
            transform.Rotate(-Vector3.forward * rotSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotSpeed);
        }

        myRigBody.freezeRotation = false;
    }
}

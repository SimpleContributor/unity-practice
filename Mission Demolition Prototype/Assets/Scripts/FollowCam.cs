using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S; // FollowCam Singleton

    // fields set in the Unity Inspector pane
    public float easing = 0.05f;
    public Vector2 minXY;
    public bool _________________________;

    // fields set dynamically
    public GameObject poi; // The point of interest
    public float camZ; // The desired Z pos of the camera

    private void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination;
 
        if (poi == null)
        {
            destination = Vector3.zero;
        } else
        {
            // Get the position of the poi
            destination = poi.transform.position;
            // If poi is a Projectile, check to see if it is at rest
            if (poi.tag == "Projectile")
            {
                // if it is sleeping (not moving)
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view (slingshot view)
                    poi = null;
                    // in the next update
                    return;
                }
            }
        }

        // Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Retain a destination.z of camZ
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        // Set the orthographicSize of the Camera to keep Ground in view
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}

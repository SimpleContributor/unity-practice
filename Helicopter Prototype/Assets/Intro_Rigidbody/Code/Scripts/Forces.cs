using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndiePixel;

public class Forces : IP_BaseRB_Controller
{
    #region Variables
    public float maxSpeed = 1f;
    public Vector3 movementDirection = new Vector3(0f, 0f, 1f);
    #endregion


    #region Custom Methods
    protected override void HandlePhysics()
    {
        rb.AddForce(movementDirection * maxSpeed);
    }
    #endregion
}

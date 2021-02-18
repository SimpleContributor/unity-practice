﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCode;


namespace MyCode
{
    public class Forces : Base_RBController
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

}

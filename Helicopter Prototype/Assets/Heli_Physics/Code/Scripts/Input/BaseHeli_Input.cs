using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    // Base class that has basic functionality for other inputs to call upon
    // This class has minimum responsibility. First we declare a 0f value for the VERT and HORZ floats.
    // All inheriting classes will run Update() which runs this.method HandleInputs() which
    // sets the axis inputs from the user on VERT and HORZ

    // this => KeyboardHeli_Input => XboxHeli_input
    public class BaseHeli_Input : MonoBehaviour
    {
        #region Variables
        [Header("Base Input Properties")]
        protected float vertical = 0f;
        protected float horizontal = 0f;
        #endregion


        #region Builtin Methods
        // Update is called once per frame
        void Update()
        {
            HandleInputs();
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleInputs()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        
        #endregion
    }

}


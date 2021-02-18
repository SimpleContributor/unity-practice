using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCode;


namespace MyCode
{
    [RequireComponent(typeof(Input_Controller), typeof(KeyboardHeli_Input), typeof(XboxHeli_Input))]
    public class Heli_Controller : Base_RBController
    {
        #region Variables
        //[Header("Controller Properties")]
        Input_Controller input;
        #endregion


        #region Custom Methods
        protected override void HandlePhysics()
        {
            input = GetComponent<Input_Controller>();
            if (input)
            {
                HandleEngines();
                HandleCharacteristics();
            }
        }


        protected virtual void HandleEngines()
        {

        }

        protected virtual void HandleCharacteristics()
        {

        }
        #endregion


        #region Helicopter Control Methods
        #endregion
    }

}

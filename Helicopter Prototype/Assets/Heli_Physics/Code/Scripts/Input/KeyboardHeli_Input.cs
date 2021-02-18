using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{

    // BaseHeli_Input => this => XboxHeli_input
    public class KeyboardHeli_Input : BaseHeli_Input
    {
        #region Variables
        [Header("Heli Keyboard Inputs")]
        #endregion

        #region Properties
        protected float throttleInput = 0f;
        public float ThrottleInput
        {
            get { return throttleInput; }
        }

        protected float collectiveInput = 0f;
        public float CollectiveInput
        {
            get { return collectiveInput; }
        }

        protected Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        protected float pedalInput = 0f;
        public float PedalInput
        {
            get { return pedalInput; }
        }
        #endregion

        #region Builtin Methods
        #endregion


        #region Custom Methods
        protected override void HandleInputs()
        {
            base.HandleInputs();

            HandleThrottle();
            HandleCollective();
            HandleCyclic();
            HandlePedal();

            // Clamp all of the input values between -1f and 1f after the values are calculated
            ClampInputs();
        }

        protected virtual void HandleThrottle()
        {
            throttleInput = Input.GetAxis("Throttle");
        }
        protected virtual void HandleCollective()
        {
            collectiveInput = Input.GetAxis("Collective");
        }

        protected virtual void HandleCyclic()
        {
            cyclicInput.y = vertical;
            cyclicInput.x = horizontal;
        }

        protected virtual void HandlePedal()
        {
            pedalInput = Input.GetAxis("Pedal");
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);
        }
        #endregion
    }

}

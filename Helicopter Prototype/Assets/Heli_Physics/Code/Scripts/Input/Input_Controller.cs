using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public enum InputType
    {
        Keyboard,
        Xbox,
        Mobile,
    }

    [RequireComponent(typeof(KeyboardHeli_Input), typeof(XboxHeli_Input))]
    public class Input_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;

        KeyboardHeli_Input keyInput;
        XboxHeli_Input xboxInput;
        #endregion


        #region Properties
        float throttleInput;
        public float ThrottleInput
        {
            get { return throttleInput; }
        }

        float collectiveInput;
        public float CollectiveInput
        {
            get { return collectiveInput; }
        }

        Vector2 cyclicInput;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        float pedalInput;
        public float PedalInput
        {
            get { return pedalInput; }
        }
        #endregion


        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            keyInput = GetComponent<KeyboardHeli_Input>();
            xboxInput = GetComponent<XboxHeli_Input>();

            if (keyInput && xboxInput)
            {
                SetInputType(inputType);
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch(inputType)
            {
                case InputType.Keyboard:
                    throttleInput = keyInput.ThrottleInput;
                    collectiveInput = keyInput.CollectiveInput;
                    cyclicInput = keyInput.CyclicInput;
                    pedalInput = keyInput.PedalInput;
                    break;

                case InputType.Xbox:
                    throttleInput = xboxInput.ThrottleInput;
                    collectiveInput = xboxInput.CollectiveInput;
                    cyclicInput = xboxInput.CyclicInput;
                    pedalInput = xboxInput.PedalInput;
                    break;

                default:
                    break;
            }
        }
        #endregion


        #region Custom Methods
        void SetInputType(InputType type)
        {
            if (type == InputType.Keyboard)
            {
                keyInput.enabled = true;
                xboxInput.enabled = false;
            }

            if (type == InputType.Xbox)
            {
                xboxInput.enabled = true;
                keyInput.enabled = false;
            }
        }
        #endregion
    }

}

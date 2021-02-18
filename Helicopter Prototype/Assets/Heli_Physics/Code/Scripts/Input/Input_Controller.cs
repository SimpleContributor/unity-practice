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

    public class Input_Controller : MonoBehaviour
    {
        #region Variables
        public InputType inputType = InputType.Keyboard;

        [Header("Input Components")]
        public KeyboardHeli_Input keyInput;
        public XboxHeli_Input xboxInput;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            SetInputType(inputType);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        #endregion


        #region Custom Methods
        void SetInputType(InputType type)
        {
            if (keyInput && xboxInput)
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
        }
        #endregion
    }

}

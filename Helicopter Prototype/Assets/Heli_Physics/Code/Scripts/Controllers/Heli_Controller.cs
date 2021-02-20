using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCode;


namespace MyCode
{
    // Base_RBController => this

    [RequireComponent(typeof(Input_Controller), typeof(KeyboardHeli_Input), typeof(XboxHeli_Input))]
    public class Heli_Controller : Base_RBController
    {
        #region Variables
        [Header("Helicopter Properties")]
        public List<Heli_Engine> engines = new List<Heli_Engine>();

        public Rotor_Controller rotorController;
        

        Input_Controller input;
        #endregion


        #region Custom Methods
        protected override void HandlePhysics()
        {
            input = GetComponent<Input_Controller>();
            if (input)
            {
                HandleEngines();
                HandleRotors();
                HandleCharacteristics();
            }
        }


        protected virtual void HandleEngines()
        {
            for (int i = 0; i < engines.Count; i++)
            {

                engines[i].UpdateEngine(input.StickyThrottle);
                float finalPower = engines[i].CurrentHP;
                
            }
        }

        protected virtual void HandleRotors()
        {
            if (rotorController && engines.Count > 0)
            {
                rotorController.UpdateRotors(input, engines[0].CurrentRPM);
            }
        }

        protected virtual void HandleCharacteristics()
        {

        }
        #endregion
    }

}

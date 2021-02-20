using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyCode
{
    public interface IHeli_Rotor
    {
        void UpdateRotor(float degPerSec, Input_Controller input);
    }

}

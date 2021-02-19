using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace MyCode
{
    public class Heli_Menus
    {
        [MenuItem("Vehicles/Setup New Helicopter")]
        public static void BuildNewHelicopter()
        {
            // Create a new helicopter setup
            GameObject currentHeli = new GameObject("New_Heli", typeof(Heli_Controller));

            // Create the COG object for the heli
            GameObject currentCOG = new GameObject("COG");
            currentCOG.transform.SetParent(currentHeli.transform);

            // Assign the COG to the currentHeli
            Heli_Controller currentController = currentHeli.GetComponent<Heli_Controller>();
            currentController.cog = currentCOG.transform;

            // Create Groups
            GameObject audioGRP = new GameObject("Audio_GRP");
            GameObject graphicsGRP = new GameObject("Graphics_GRP");
            GameObject colGRP = new GameObject("Collision_GRP");

            audioGRP.transform.SetParent(currentHeli.transform);
            graphicsGRP.transform.SetParent(currentHeli.transform);
            colGRP.transform.SetParent(currentHeli.transform);

            // Select the new helicopter in the hierarchy
            Selection.activeGameObject = currentHeli;
        }
    }

}

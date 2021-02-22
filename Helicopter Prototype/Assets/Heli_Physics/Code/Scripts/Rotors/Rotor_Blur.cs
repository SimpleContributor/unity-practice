using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Rotor_Blur : MonoBehaviour, IHeli_Rotor
    {
        public float maxDegPerSec = 323f;
        public List<GameObject> blades = new List<GameObject>();
        public GameObject blurGeo;

        public List<Texture2D> blurTextures = new List<Texture2D>();
        public Material blurMat;

        public void UpdateRotor(float degPerSec, Input_Controller input)
        {
            // Debug.Log("Update Rotor from Rotor Blur");

            float normalizedDegPerSec = Mathf.InverseLerp(0f, maxDegPerSec, degPerSec);

            int blurTextID = Mathf.FloorToInt(normalizedDegPerSec * blurTextures.Count) - 1;
            blurTextID = Mathf.Clamp(blurTextID, 0, blurTextures.Count - 1);
            // Debug.Log(blurTextID);

            if (blurMat && blurTextures.Count > 0)
            {
                blurMat.SetTexture("_MainTex", blurTextures[blurTextID]);
            }

            if (blurTextID > 2 && blades.Count > 0)
            {
                HandleGeoBladeViz(false);
            } 
            else
            {
                HandleGeoBladeViz(true);
            }
        }

        void HandleGeoBladeViz(bool viz)
        {
            foreach (var blade in blades)
            {
                blade.SetActive(viz);
            }
        }
    }

}

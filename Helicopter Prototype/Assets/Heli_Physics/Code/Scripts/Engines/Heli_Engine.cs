using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyCode
{
    public class Heli_Engine : MonoBehaviour
    {
        public float maxHP = 140f;
        public float maxRPM = 2700f;
        public float powerDelay = 2f;
        public AnimationCurve powerCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        // Current HorsePower (read only)
        // Current RPM (read only)
        #region Properties
        float currentHP;
        public float CurrentHP
        {
            get { return currentHP; }
        }

        float currentRPM;
        public float CurrentRPM
        {
            get { return currentRPM; }
        }
        #endregion


        // UpdateEngine(float throttleInput) sets the desired HorsePower and RPM to throttle Input * max
        // Lerp the current with the desired at a fixed timerate * delay. This delay will be the time before 
        // the
        public void UpdateEngine(float throttleInput)
        {
            // Calculate horsepower
            float wantedHP = powerCurve.Evaluate(throttleInput) * maxHP;
            currentHP = Mathf.Lerp(currentHP, wantedHP, Time.deltaTime * powerDelay);
            // Debug.Log(currentHP);

            // Calculate RPM's
            float wantedRPM = throttleInput * maxRPM;
            currentRPM = Mathf.Lerp(currentRPM, wantedRPM, Time.deltaTime);
            // Debug.Log(currentRPM);
        }
    }
}

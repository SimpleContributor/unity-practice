using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace MyCode
{
    // The type we want to override
    [CustomEditor(typeof(XboxHeli_Input))]
    public class XboxHeli_InputEditor : Editor
    {
        #region Variables
        XboxHeli_Input targetInput;
        #endregion


        #region Builtin Methods
        private void OnEnable()
        {
            targetInput = (XboxHeli_Input)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawDebugUI();

            // Force to update constantly
            Repaint();
        }
        #endregion

        #region Custom Methods
        void DrawDebugUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Throttle: " + targetInput.ThrottleInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Collective: " + targetInput.CollectiveInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Cyclic: " + targetInput.CyclicInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pedal: " + targetInput.PedalInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        #endregion 
    }

}

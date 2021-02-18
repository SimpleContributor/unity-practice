using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCam : MonoBehaviour
{
    #region Variables
    [Header("UI Properties")]
    public float intensity = 5f;

    CinemachineVirtualCamera myVirtualCam;
    #endregion


    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        myVirtualCam = GetComponent<CinemachineVirtualCamera>();
    }
    #endregion


    #region Custom Methods
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin noise = myVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = 1;
    }
    #endregion
}

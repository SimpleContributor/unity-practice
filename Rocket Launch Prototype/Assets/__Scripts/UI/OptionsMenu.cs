using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    #region Variables
    [Header("UI Objects")]
    public Slider volumeSlider;
    #endregion


    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SliderVolumeLevel"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("SliderVolumeLevel");
        }

        AudioListener.volume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeSlider.value;

        if (volumeSlider.value != PlayerPrefs.GetFloat("SliderVolumeLevel"))
        {
            PlayerPrefs.SetFloat("SliderVolumeLevel", volumeSlider.value);
        }
    }
    #endregion
}

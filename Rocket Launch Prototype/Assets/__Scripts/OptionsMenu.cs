using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;

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
}

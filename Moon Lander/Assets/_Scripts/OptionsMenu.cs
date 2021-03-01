using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlide;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SliderVolumeLevel"))
        {
            volumeSlide.value = PlayerPrefs.GetFloat("SliderVolumeLevel");
        }

        AudioListener.volume = volumeSlide.value;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeSlide.value;

        if (volumeSlide.value != PlayerPrefs.GetFloat("SliderVolumeLevel"))
        {
            PlayerPrefs.SetFloat("SliderVolumeLevel", volumeSlide.value);
        }
    }
}

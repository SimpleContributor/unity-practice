using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlide;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SliderVolumeLevel"))
        {
            volumeSlide.value = PlayerPrefs.GetFloat("SliderVolumeLevel");
        }

        PlayerPrefs.SetFloat("SliderVolumeLevel", volumeSlide.value);
    }
}

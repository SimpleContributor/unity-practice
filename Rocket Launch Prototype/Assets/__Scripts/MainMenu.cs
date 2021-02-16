﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;

    GameController gameController;


    private void Start()
    {
        gameController = GetComponent<GameController>();
        if (PlayerPrefs.HasKey("SliderVolumeLevel"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("SliderVolumeLevel");
        }

        PlayerPrefs.SetFloat("SliderVolumeLevel", volumeSlider.value);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //gameController.NextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}

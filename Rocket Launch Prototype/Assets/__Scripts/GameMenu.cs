using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Slider gameMenuSlider;
    public GameObject gameMenu;
    public GameObject healthUI;

    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        gameMenuSlider.value = PlayerPrefs.GetFloat("SliderVolumeLevel", gameMenuSlider.value);
        AudioListener.volume = gameMenuSlider.value;
        //gameMenuSlider.value = optionsMenu.volumeSlider.value;
        gameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuSlider.value != PlayerPrefs.GetFloat("SliderVolumeLevel"))
        {
            PlayerPrefs.SetFloat("SliderVolumeLevel", gameMenuSlider.value);
        }

        AudioListener.volume = gameMenuSlider.value;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        gameMenu.SetActive(true);
        healthUI.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        gameMenu.SetActive(false);
        healthUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

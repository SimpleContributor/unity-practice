using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    #region Variables
    [Header("UI Objects")]
    public Slider gameMenuSlider;

    [Header("Game Objects")]
    public GameObject gameMenu;
    public GameObject healthUI;
    #endregion


    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
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
    #endregion



    #region Custom Methods
    public void PauseGame()
    {
        Time.timeScale = 0;
        gameMenu.SetActive(true);
        healthUI.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(false);
        healthUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}

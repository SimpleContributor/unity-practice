using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (Time.timeScale > 0f)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        } 
        else
        {
            float timeLerp = Mathf.Lerp(0f, 1f, 1f);
            Time.timeScale = timeLerp;
            pauseMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.Pause();
        }
    }

    public void ResetLevel()
    {
        StartCoroutine(LoadSameScene());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextScene());
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void StartGame()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx + 1;

        if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIdx = 0;
        }

        SceneManager.LoadScene(nextSceneIdx);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx + 1;

        if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIdx = 0;
        }

        SceneManager.LoadScene(nextSceneIdx);
    }

    IEnumerator LoadSameScene()
    {
        yield return new WaitForSeconds(2f);

        int sceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIdx);
    }
}

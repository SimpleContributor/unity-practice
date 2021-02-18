using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Variables
    [Header("GC Cheats")]
    public bool invincible = false;

    [Header("GC Objects")]
    public GameMenu gameMenu;
    #endregion



    #region Builtin Methods
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invincible = !invincible;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                gameMenu.PauseGame();
            }
            else
            {
                gameMenu.ResumeGame();
            }
        }
    }
    #endregion



    #region Custom Methods
    public void ResetGame()
    {
        StartCoroutine(LoadSameLevel());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }
    #endregion



    #region Coroutines
    private IEnumerator LoadSameLevel()
    {
        yield return new WaitForSeconds(2f);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex);
        int nextSceneIndex = sceneIndex + 1;
        Debug.Log(nextSceneIndex);
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    #endregion 
}

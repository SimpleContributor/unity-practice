using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool invincible = false;


    public void ResetGame()
    {
        StartCoroutine(LoadSameLevel());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            invincible = !invincible;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
    }
}

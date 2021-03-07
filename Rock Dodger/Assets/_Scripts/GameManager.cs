using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI inGameScoreText;

    public GameObject menu;
    public GameObject inGameScore;

    public int highScore;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }

        PlayerPrefs.SetInt("HighScore", highScore);
        highScoreText.text = $"High Score: {highScore}";

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Score();
        HighScore(); 
        
        if (Input.GetKeyDown(KeyCode.Space) && menu.activeInHierarchy)
        {
            inGameScore.SetActive(true);
            menu.SetActive(false);
            Restart();
        }
    }

    void Score()
    {
        scoreText.text = $"Score: {score}";
        inGameScoreText.text = $"{score}";
    }

    void HighScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = $"High Score: {highScore}";
    }

    public void DeathMenu()
    {
        inGameScore.SetActive(false);
        menu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = Mathf.Lerp(0f, 1f, 1.5f);
    }

    
}

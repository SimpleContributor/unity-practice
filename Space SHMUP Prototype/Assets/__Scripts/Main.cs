using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemySpawnPadding = 1.5f;

    public bool ___________________________;

    public float enemySpawnRate;

    private void Awake()
    {
        S = this;
        // Set Utils.camBounds
        Utils.SetCameraBounds(this.GetComponent<Camera>());
        // 0.5 enemies/second = enemySpawnRate of 2
        enemySpawnRate = 1f / enemySpawnPerSecond;
        // Invoke call SpawnEnemy() once after a 2s delay
        Invoke("SpawnEnemy", enemySpawnRate);
    }

    public void SpawnEnemy()
    {
        // Pick a random enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate(prefabEnemies[ndx]) as GameObject;
        // Position the Enemy above the screen with a random x position
        Vector3 pos = Vector3.zero;
        float xMin = Utils.CamBounds.min.x + enemySpawnPadding;
        float xMax = Utils.CamBounds.max.x - enemySpawnPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = Utils.CamBounds.max.y + enemySpawnPadding;
        go.transform.position = pos;
        // Call SpawnEnemy() again in a couple of seconds
        Invoke("SpawnEnemy", enemySpawnRate);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        // Application.LoadLevel("_Scene_0");
        SceneManager.LoadScene("_Scene_0");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockGO;

    public float spawnTimer = 1f;

    float screenEdge;
    float screenHeight;

    float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        screenEdge = Camera.main.aspect * Camera.main.orthographicSize;
        screenHeight = screenEdge / 9 * 16;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawner();
    }

    void Spawner()
    {
        if (count < spawnTimer)
        {
            count += Time.deltaTime;

        }
        else
        {
            //Vector3 randomPos = new Vector3(Random.Range(-screenEdge, screenEdge), transform.position.y, transform.position.z);
            //float randomScale = Random.Range(0.5f, 3f);
            //GameObject newRock = (GameObject)Instantiate(rockGO);
            //newRock.transform.parent = transform;
            //newRock.transform.position = randomPos;

            float randomScale = Random.Range(0.5f, 3f);
            Vector2 randomPos = new Vector2(Random.Range(-screenEdge, screenEdge), screenHeight + randomScale);
            float randomRot = Random.Range(-15f, 15f);

            GameObject newRock = (GameObject)Instantiate(rockGO, randomPos, Quaternion.Euler(0f, 0f, randomRot));
            newRock.transform.parent = transform;
            newRock.transform.localScale = new Vector2(randomScale, randomScale);
            count = 0;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        Vector3 randomPos = new Vector3(Random.Range(-screenEdge, screenEdge), transform.position.y, transform.position.z);
        GameObject newRock = (GameObject)Instantiate(rockGO);
        newRock.transform.parent = transform;
        newRock.transform.position = randomPos;
    }
}

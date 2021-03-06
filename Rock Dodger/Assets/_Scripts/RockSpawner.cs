using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockGO;
    Rock rock;

    public float spawnTimer = 1f;
    public float maxRockSpeed = 40f;

    float screenEdge;
    float screenHeight;

    float count = 0f;
    float timer = 0f;
    float difficultyTimer = 5f;


    // Start is called before the first frame update
    void Start()
    {
        screenEdge = Camera.main.aspect * Camera.main.orthographicSize;
        screenHeight = screenEdge / 9 * 16;

        rock = rockGO.GetComponent<Rock>();

        rock.rockSpeed = 10f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > difficultyTimer)
        {
            IncreaseDiff();
        }
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

    void IncreaseDiff()
    {
        // Each variable is independent, so an adjustment to one needs to be equal in ratio to the other
        if (rock.rockSpeed < maxRockSpeed && spawnTimer > 0.2f)
        {
            rock.rockSpeed += 5f;
            spawnTimer -= 0.2f;
            timer = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner3 : MonoBehaviour   
{
    public GameObject cubePrefabVar;
    // holds all the cubes
    public List<GameObject> gameObjectList;
    // scale down the cubes 
    public float scalingFactor = 0.95f;
    public int numCubes = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize the List<GameObject>
        gameObjectList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        numCubes++;         //1
        GameObject gObj = Instantiate(cubePrefabVar) as GameObject;         //2

        gObj.name = "Cube " + numCubes;             //3
        Color c = new Color(Random.value, Random.value, Random.value);              //4
        gObj.GetComponent<Renderer>().material.color = c;
        gObj.transform.position = Random.insideUnitSphere;              //5

        gameObjectList.Add(gObj);

        // store the cubes that are no longer visible in a list
        List<GameObject> removeList = new List<GameObject>();               //6

        // iterate through cubes in the list
        foreach (GameObject goTemp in gameObjectList)               //7
        {
            float scale = goTemp.transform.localScale.x;                //8
            scale *= scalingFactor;
            goTemp.transform.localScale = Vector3.one * scale;

            if (scale <= 0.1f)                 //9
            {
                removeList.Add(goTemp);
            }
        }

        foreach (GameObject goTemp in removeList)               //7
        {
            // remove cube from list
            gameObjectList.Remove(goTemp);              //10
            // destroy the cube
            Destroy(goTemp);
        }
    }
}

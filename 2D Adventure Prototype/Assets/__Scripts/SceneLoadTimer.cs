using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTimer : MonoBehaviour
{
    public int SceneID = 0;
    public float TimeDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", TimeDelay);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneID);
    }
}

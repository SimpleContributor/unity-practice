using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerReset : MonoBehaviour
{
    public float ResetTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Reset", ResetTime);
    }

    private void Reset()
    {
        PlayerController.Reset();
        SceneManager.LoadScene(1);
    }
}

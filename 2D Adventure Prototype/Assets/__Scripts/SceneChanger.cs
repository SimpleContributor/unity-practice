using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int SceneDestination = 0;

    public bool FadeOnStart = true;

    public Vector3 TargetPosition = Vector3.zero;

    public static Vector3 LastTarget = Vector3.zero;

    private int FadeInTrigger = Animator.StringToHash("FadeIn");
    private int FadeOutTrigger = Animator.StringToHash("FadeOut");

    private Animator FadeAnimator = null;

    private void Awake()
    {
        FadeAnimator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (FadeOnStart && FadeAnimator != null)
        {
            FadeAnimator.SetTrigger(FadeInTrigger);
        }
    }

    public void SceneChange()
    {
        SceneChanger.LastTarget = TargetPosition;
        SceneManager.LoadScene(SceneDestination);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PlayerController.PlayerInstance.CanControl = false;

        FadeAnimator.SetTrigger(FadeOutTrigger);
    }
}

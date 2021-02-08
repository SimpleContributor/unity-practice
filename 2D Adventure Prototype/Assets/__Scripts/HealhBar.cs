using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhBar : MonoBehaviour
{
    public float MaxSpeed = 10f;
    private RectTransform ThisTransform = null;

    private void Awake()
    {
        ThisTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.PlayerInstance != null)
        {
            ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(PlayerController.Health, 0, 200), ThisTransform.sizeDelta.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float HealthUpdate = 0f;
        if (PlayerController.PlayerInstance != null)
        {
            HealthUpdate = Mathf.MoveTowards(ThisTransform.rect.width, PlayerController.Health, MaxSpeed);
        }
        ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(HealthUpdate, 0, 200), ThisTransform.sizeDelta.y);
    }
}

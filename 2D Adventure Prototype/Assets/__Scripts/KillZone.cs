using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public float Damage = 9000f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (PlayerController.PlayerInstance != null)
        {
            PlayerController.Health -= Damage * Time.deltaTime;
        }
    }
}

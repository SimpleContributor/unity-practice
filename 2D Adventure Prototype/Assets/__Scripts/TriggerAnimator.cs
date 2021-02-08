using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimator : MonoBehaviour
{
    public Animator TriggerAnim = null;

    public string AnimBoolean = string.Empty;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        TriggerAnim.SetBool(AnimBoolean, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        TriggerAnim.SetBool(AnimBoolean, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDanger : MonoBehaviour
{
    public Vector3 moveEnd;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var moveStart = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, moveStart, moveEnd, 3.0f));
            yield return StartCoroutine(MoveObject(transform, moveEnd, moveStart, 3.0f));
        }
    }

    
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        other.rigidbody.constraints = RigidbodyConstraints.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDanger : MonoBehaviour
{
    #region Variables
    public Vector3 moveEnd;
    #endregion



    #region Builtin Methods
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
    #endregion



    #region Custom Methods
    public void Movement()
    {
        Vector3 tempPos = transform.position;

    }
    #endregion



    #region Coroutines
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
    #endregion



    #region Collisions
    private void OnCollisionEnter(Collision other)
    {

        // Allows ship to be moved along z-axis by moving obstacles. This kills the ship instantly
        other.rigidbody.constraints = RigidbodyConstraints.None;
    }
    #endregion
}

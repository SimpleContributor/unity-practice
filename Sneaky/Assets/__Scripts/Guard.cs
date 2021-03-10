using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;
    public float moveSpeed = 5f;
    public float waitTime = 1f;
    public float turnSpeed = 90f;

    Vector3[] waypoints;

    private void Start()
    {
        // determine the length of the array
        waypoints = new Vector3[pathHolder.childCount];

        // Set array values to the position of the waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        // Snaps the guard to the starting position of the path
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        // Turn to look at the next position before running the infinite while loop
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);
            // Once we get to the new position run some stuff...
            if (transform.position == targetWaypoint)
            {
                // This will make sure the index gets set back to 0 after the last waypoint
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                // Stop at the new waypoint for waitTime before turning to face the new one
                yield return new WaitForSeconds(waitTime);
                // Turn to face the new waypoint before moving again
                yield return StartCoroutine(FacePath(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator FacePath(Vector3 lookTarget)
    {
        // Direction of the new target
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        // Angle of the new target
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        // rotate while the angle between the look dir and the target dir is greater than 0.05 degrees
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    // Makes each waypoint visible since they are just emptyGOs.
    private void OnDrawGizmos()
    {
        Vector3 startPos = pathHolder.GetChild(0).position;
        Vector3 prevPos = startPos;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(prevPos, waypoint.position);
            prevPos = waypoint.position;
        }

        Gizmos.DrawLine(prevPos, startPos);
    }
}

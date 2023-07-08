using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VillagerMove : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    public GameObject haulingObj;
    public Transform haulPosition;

    void Start()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        if (agent == null)
        {
            try
            {
                agent = this.gameObject.GetComponent<NavMeshAgent>();
            }
            catch
            {

            }
        }
        if (haulPosition == null)
        {
            haulPosition = this.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        haulingThis();
    }
    public void moveToPoint(Vector3 destinationPoint)
    {
        //Debug.LogWarning(CalculatePathDistance(destinationPoint));
        agent.SetDestination(destinationPoint);
    }
    public bool anyPathRemaining()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckPathToDestination(Vector3 destinationPoint)
    {
        NavMeshPath path = new NavMeshPath();

        // Calculate the path to the destination
        if (agent.CalculatePath(destinationPoint, path))
        {
            // Check if the path is valid
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                return true;
            }
        }

        return false;
    }
    public float CalculatePathDistance(Vector3 destinationPoint)
    {
        if (!CheckPathToDestination(destinationPoint))
        {
            return float.PositiveInfinity;
        }
        NavMeshPath path = new NavMeshPath();

        // Calculate the path to the destination
        agent.CalculatePath(destinationPoint, path);

        // Get the distance of the calculated path
        float pathDistance = GetPathDistance(path);

        return pathDistance;
    }
    private float GetPathDistance(NavMeshPath path)
    {
        float distance = 0f;

        // Iterate through each corner of the path and accumulate distances
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }

    public void haulThis(GameObject objToBeHauled, bool doOrNot)
    {
        if (doOrNot)
        {
            haulingObj = objToBeHauled;
            //haulingThis();
        }
        else
        {
            haulingObj = null;
        }
        
    }
    public void haulingThis()
    {

        if (haulingObj != null)
        {
            haulingObj.transform.position = haulPosition.position;
        }

    }
    public void newCommand()
    {
        agent.ResetPath();
        haulingObj = null;
    }
    private void PlaceOnNearestNavMesh()
    {
        NavMeshHit hit;

        // Sample the nearest point on the NavMesh to the current position
        if (NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            // Move the GameObject to the nearest point on the NavMesh
            transform.position = hit.position;
            agent.Warp(hit.position);
        }
    }
}

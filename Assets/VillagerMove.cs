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
}

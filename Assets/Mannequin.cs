using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mannequin : MonoBehaviour
{
    public NavMeshAgent a770;
    public bool isAlive, isSeen;
    public Transform target;
    public void checkForStart(int targetCheck)
    {
        if(targetCheck <= 16)
        {
            isAlive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive && !isSeen)
        {
            a770.speed = 3;
            if(a770.destination == null)
            {
                a770.SetDestination(target.position);
            }
        }
        else if (isSeen)
        {
            a770.speed = 0;
        }
    }

    private void OnBecameInvisible()
    {
        isSeen = false;
        a770.isStopped = false;
    }
    private void OnBecameVisible()
    {
        isSeen = true;
        a770.isStopped = true;
        a770.ResetPath();
    }
}

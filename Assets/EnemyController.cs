using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] goals;

    private void Start()
    {
        goals = GameObject.FindGameObjectsWithTag("Goal");
        SetDest(goals);
    }

    private void Update()
    {
        if (!GetComponent<NavMeshAgent>().pathPending)
        {
            if(GetComponent<NavMeshAgent>().remainingDistance <= GetComponent<NavMeshAgent>().stoppingDistance)
            {
                if (GetComponent<NavMeshAgent>().hasPath || GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                {
                    SetDest(goals);
                    Debug.Log("Stopped");
                }
            }
        }
    }

    public void SetDest(GameObject[] goals)
    {
        GetComponent<NavMeshAgent>().SetDestination(goals[Random.Range(0, goals.Length)].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mannequin"))
        {
            other.GetComponent<Mannequin>().checkForStart(Random.Range(1, 21));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    public void SetCivilianDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }
}

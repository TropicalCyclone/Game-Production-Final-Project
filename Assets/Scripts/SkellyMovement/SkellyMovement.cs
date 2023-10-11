using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkellyMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    public GameObject skelly;
    NavMeshAgent agent;
    PlayerFOV playerFOV;
    
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerFOV = GetComponent<PlayerFOV>();
    }

    public void Update()
    {
        if(playerFOV.lookingAtEnemy == false)
        {
            agent.SetDestination(Player.position);
            skelly.transform.LookAt(Player.position);
            agent.isStopped = false;
        }
        else
        {
            agent.SetDestination(skelly.transform.position);
            agent.isStopped = true;
        }
    }
}

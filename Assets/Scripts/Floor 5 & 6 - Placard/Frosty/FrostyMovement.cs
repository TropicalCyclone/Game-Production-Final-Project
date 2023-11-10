using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostyRoam : MonoBehaviour
{
    // To easily change the modifiers of frosty for its passive/aggressive form
    [Header("NavMeshAgent Settings")]
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private float acceleration = 8f;

    [Header("Roaming Settings")]
    [SerializeField] private float roamRange = 10f;
    [SerializeField] private float minRoamFrequency = 1f; // Minimum time between roams
    [SerializeField] private float maxRoamFrequency = 3f; // Maximum time between roams
    private float idleTimer = 0f;
    private float nextRoamTime = 0f;


    [Header("Chase Settings")]
    [SerializeField] private float maxChaseTime = 5f; // The maximum time to chase the player after they go out of sight
    private float currentChaseTime = 0f;
    private bool isChasing = false;

    [Header("Last Player Position Settings")]
    [SerializeField] private float moveToLastPlayerTime = 20f; // Time to move towards the last player position
    private float currentMoveToLastPlayerTime = 0f;
    private bool isMovingToLastPlayer = false;

    GameObject player;
    private NavMeshAgent agent;
    private Vector3 currentDestination;
    private FieldOfView fieldOfView;

    private enum EnemyState
    {
        Roaming,
        Chasing
    }

    private EnemyState currentState;

    private void Start()
    {   
        fieldOfView = GetComponent<FieldOfView>();
        currentState = EnemyState.Roaming;
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        SetRandomDestination();
        CalculateNextRoamTime();
    }

    private void Update()
    {
        if (fieldOfView.canSeePlayer)
        {
            currentState = EnemyState.Chasing;
            Chase();
            currentChaseTime = 0f;
            isChasing = true;
        }
        else if (isChasing)
        {
            currentChaseTime += Time.deltaTime;
            Debug.Log("Escape Time: " + currentChaseTime);

            if (currentChaseTime >= maxChaseTime)
            {
                currentState = EnemyState.Roaming;
                SetRandomDestination();
                CalculateNextRoamTime();
                isChasing = false;
            }
            Chase();
        }
        else
        {
            Patrol();
        }

        // Check if it's time to move towards the last player position
        if (isMovingToLastPlayer)
        {
            currentMoveToLastPlayerTime += Time.deltaTime;

            if (currentMoveToLastPlayerTime >= moveToLastPlayerTime)
            {
                currentState = EnemyState.Roaming;
                SetDestinationToLastPlayerPosition();
                CalculateNextRoamTime();
                isMovingToLastPlayer = false;
                currentMoveToLastPlayerTime = 0f;
            }
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    private void Patrol()
    {
        if (ReachedDestination())
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= nextRoamTime)
            {
                // Check if it's time to move towards the last player position
                if (!isMovingToLastPlayer)
                {
                    isMovingToLastPlayer = true;
                }
                else
                {
                    SetRandomDestination();
                    CalculateNextRoamTime();
                    idleTimer = 0f;
                }
            }
        }
    }

    private bool ReachedDestination()
    {
        // Check if the AI has reached its destination within a small threshold
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    private void SetRandomDestination()
    {
        // Generate a random destination within the roam range
        Vector3 randomDirection = Random.insideUnitSphere * roamRange;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRange, 1);
        currentDestination = hit.position;

        // Set the AI's destination to the random point
        agent.SetDestination(currentDestination);
    }

    private void CalculateNextRoamTime()
    {
        nextRoamTime = Random.Range(minRoamFrequency, maxRoamFrequency);
        Debug.Log("Time Left Before Roam: " + nextRoamTime);
    }

    // Move towards the LastPlayerPosition (Waypoints)
     private void SetDestinationToLastPlayerPosition()
    {
        PlayerPosition playerPosition = player.GetComponent<PlayerPosition>();
        if (playerPosition != null)
        {
            agent.SetDestination(playerPosition.GetLastPosition());
        }
    }
}
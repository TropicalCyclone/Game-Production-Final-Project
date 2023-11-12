using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacardSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private List<Transform> availableSpawnPoints = new List<Transform>();
    public GameObject[] objectsToMove; // Game Objects to move to spawn points

    void Start()
    {
        if (objectsToMove.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No objects to move or no spawn points available.");
            return;
        }

        if (objectsToMove.Length > spawnPoints.Length)
        {
            Debug.LogWarning("More objects to move than available spawn points. Some objects will not be moved.");
        }

        // Shuffle the list of spawn points to randomize their order.
        ShuffleSpawnPoints();

        // Move objects to spawn points, making sure not to exceed the number of available spawn points.
        for (int i = 0; i < Mathf.Min(objectsToMove.Length, spawnPoints.Length); i++)
        {
            MoveObjectToSpawnPoint(objectsToMove[i], spawnPoints[i]);
        }
    }

    void MoveObjectToSpawnPoint(GameObject objectToMove, Transform spawnPoint)
    {
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion spawnRotation = spawnPoint.rotation;

        objectToMove.transform.position = spawnPosition;
        objectToMove.transform.rotation = spawnRotation;

        // Remove the used spawn point from the available spawn points list.
        availableSpawnPoints.Remove(spawnPoint);
    }

    void ShuffleSpawnPoints()
    {
        availableSpawnPoints.AddRange(spawnPoints);

        for (int i = spawnPoints.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }
}
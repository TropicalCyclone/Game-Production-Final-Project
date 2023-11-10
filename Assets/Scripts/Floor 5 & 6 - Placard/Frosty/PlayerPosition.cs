using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private Vector3 lastPosition;

    // Method to set the last position
    public void SetLastPosition(Vector3 position)
    {
        lastPosition = position;
        Debug.Log("Player's last position set to: " + lastPosition);
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }
}

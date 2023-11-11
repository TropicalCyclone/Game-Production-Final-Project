using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorParentScript : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float height;
    [SerializeField] private Transform tp;
    private bool TeleportTrigger;
    public void TeleportPlayer()
    {
        if (!TeleportTrigger)
        {
            player.transform.position = tp.position;
            TeleportTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Found Player");
            player = other.gameObject;  
        }
    }
}

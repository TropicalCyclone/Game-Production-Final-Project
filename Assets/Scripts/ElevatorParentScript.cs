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
            player.transform.position = new Vector3(player.transform.position.x, tp.position.y,player.transform.position.z);
            TeleportTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;  
        }
    }
}

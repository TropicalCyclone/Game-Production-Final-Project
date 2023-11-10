using System.Collections;
using UnityEngine;

public class PlayerLastPosition : MonoBehaviour
{
    private bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            StartCoroutine(UpdatePlayerPosition(other.GetComponent<PlayerPosition>()));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    IEnumerator UpdatePlayerPosition(PlayerPosition playerController)
    {
        yield return new WaitForSeconds(10f);

        if (playerInside)
        {
            playerController.SetLastPosition(transform.position);
            Debug.Log("Player's last position set to: " + transform.position);
        }
    }
}
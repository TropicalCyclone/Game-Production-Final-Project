using System.Collections;
using UnityEngine;

public class PlayerLastPosition : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
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
        Debug.Log("Wait Time: " + waitTime);
        yield return new WaitForSeconds(waitTime);

        if (playerInside)
        {
            playerController.SetLastPosition(transform.position);
            Debug.Log("Player's last position set to: " + transform.position);
        }
    }
}

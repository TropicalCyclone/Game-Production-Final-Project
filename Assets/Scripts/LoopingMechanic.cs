using UnityEngine;

public class LoopingMechanic : MonoBehaviour
{
    public Transform teleportTarget;
    
    public void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player")) {
               Vector3 offset = other.transform.position - transform.position;
               other.transform.position = teleportTarget.position + offset;
          }    
     }
}

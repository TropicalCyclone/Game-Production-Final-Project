using UnityEngine;

public class LoopingMechanic : MonoBehaviour
{
     // The Comments are Jairus' suggestion regarding Looping

    public Transform teleportTarget;
     // public Transform[] teleportTargets;   
    
    public void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player")) {
               Vector3 offset = other.transform.position - transform.position;
               other.transform.position = teleportTarget.position + offset;

               // Vector3 offset = other.transform.position - transform.position;
               // int randomIndex = Random.Range(0, teleportTargets.Length);
               // other.transform.position = teleportTargets[randomIndex].position + offset;
          }    
     }
}
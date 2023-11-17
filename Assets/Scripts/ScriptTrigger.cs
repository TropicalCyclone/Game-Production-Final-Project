using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent FloorSevenEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FloorSevenEnter.Invoke();
        }
    }
}

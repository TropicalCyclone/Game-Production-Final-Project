using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCounter : MonoBehaviour
{

    [SerializeField] int maxCount;
    private int _loopCount = -1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _loopCount += 1;
            Debug.Log("Loop amount: " + _loopCount);
        }
    }
}

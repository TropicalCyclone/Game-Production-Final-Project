using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoopCounter : MonoBehaviour
{
    [SerializeField] public List<NumCountEvent> events = new List<NumCountEvent>();

    private int _loopCount = -1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _loopCount += 1;
            for(int i = 0; i < events.Count; i++)
            {
                if (events[i].LoopCount == _loopCount)
                {
                    events[i].ReachNum.Invoke();
                }
            }
        }
    }
}

[System.Serializable]
public class NumCountEvent
{
    [SerializeField] private string _name;
    [SerializeField] private int _loopCount;

    [SerializeField] private UnityEvent _reachNum;
    public UnityEvent ReachNum { get { return _reachNum; } }

    public int LoopCount
    {
        get { return _loopCount; }
    }
}
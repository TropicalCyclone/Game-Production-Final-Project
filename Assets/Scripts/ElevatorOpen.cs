using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ElevatorOpen : MonoBehaviour
{
    [SerializeField] private UnityEvent _reachNum;
    [SerializeField] private Animator Leftdoor;
    [SerializeField] private Animator Rightdoor;

    private bool _flashlightEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ElevatorDoorOpen()
    {
        Leftdoor.Play("DoorLOpen");
        Rightdoor.Play("DoorROpen");
    }

    public void ElevatorDoorClose()
    {
        Leftdoor.Play("DoorLClose");
        Rightdoor.Play("DoorRClose");
    }

    public void FlashlightOpen()
    {
        if (!_flashlightEvent)
        {
            _reachNum.Invoke();
            ElevatorDoorClose();
            _flashlightEvent = true;
        }
    }
}

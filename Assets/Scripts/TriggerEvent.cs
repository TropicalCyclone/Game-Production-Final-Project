using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public enum Modes { Once, NTimes, Infinite }

    public Modes Mode = Modes.Once;
    public uint MaxTriggerCount = 1;
    public UnityEvent OnTriggerEvent;

    private bool isTriggered;
    private uint triggerCount;

    private void Trigger()
    {
        if (!isTriggered)
        {
            switch (Mode)
            {
                case Modes.Once:
                    isTriggered = true;
                    OnTriggerEvent.Invoke();
                    break;
                case Modes.NTimes:
                    triggerCount++;
                    if (triggerCount >= MaxTriggerCount){
                        isTriggered = true;
                        OnTriggerEvent.Invoke();
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If needed, you can add logic here when the player exits the trigger zone
        }
    }
}

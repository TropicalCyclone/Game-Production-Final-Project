using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterLooper : MonoBehaviour
{
    [SerializeField] private float teleportHeight = 7.5f;

    [SerializeField] private TeleporterLooper otherLooper;

    private bool _hasTeleported;

    public bool HasTeleported { get { return _hasTeleported; } set { _hasTeleported = value; } }
    public bool didTeleport()
    {
        if(_hasTeleported)
            return true;
        else
            return false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!otherLooper.didTeleport() || otherLooper == null)
            {
                Vector3 playerPos = other.transform.position;
                other.gameObject.transform.position = new Vector3(playerPos.x, playerPos.y + teleportHeight, playerPos.z);
                _hasTeleported = true;
                if (otherLooper != null)
                {
                    otherLooper.HasTeleported = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (otherLooper != null)
            {
                if (otherLooper.didTeleport())
                {
                    _hasTeleported = false;
                    otherLooper.HasTeleported = false;
                }
            }
        }
    }
}
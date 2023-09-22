using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightShake : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private Light flashlight;
    private float batteryCapacity;
    private float BatteryRemaining;
    [SerializeField]
    private float batteryDrainSpeed;
    [SerializeField]
    private float batteryChargingSpeed;
    private bool isCharging;

    private Vector3 lastpos;
    private Vector3 velocity;
    
    private float timeSinceLastShake;
    [SerializeField]
    private float MinShakeInterval;
    [SerializeField]
    private float ShakeDetectionThreshold;
    private float sqrShakeDetectionThreshold;
    
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        velocity = (transform.transform.position - lastpos)/Time.deltaTime;
        lastpos = transform.position;
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        flashlight = GetComponentInChildren<Light>();
        batteryCapacity = flashlight.intensity;
    }

    private void Update()
    {
        
      
        Debug.Log(velocity.sqrMagnitude);
        if (velocity.sqrMagnitude >= ShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            RechargeBattery();
            isCharging = true;

        }
        else { isCharging = false; }
        
        
            if(flashlight.intensity > 0)
            {
                flashlight.intensity -= batteryDrainSpeed*Time.deltaTime;
            }
       
    }
    public void RechargeBattery()
    {
        if (flashlight.intensity < batteryCapacity)
            flashlight.intensity += batteryChargingSpeed;
    }
}

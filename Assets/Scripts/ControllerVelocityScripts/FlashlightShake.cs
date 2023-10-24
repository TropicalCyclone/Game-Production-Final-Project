using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private HealthBarBehaviour healthBarBehaviour;
    Rigidbody rb;
    [SerializeField]
    private Light flashlight;
    [SerializeField]
    private SpriteRenderer Light;

    [Header("Flashlight Battery")]
    [SerializeField]
    private float batteryCapacity;
    [SerializeField]
    private float batteryDrainSpeed;
    [SerializeField]
    private float batteryChargingSpeed;
    private Vector3 lastpos;
    private Vector3 velocity;
    private bool isOn;
    
    [Header("Flashlight Shake")]
    [SerializeField]
    private float MinShakeInterval;
    [SerializeField]
    private float ShakeDetectionThreshold;
    private float timeSinceLastShake;
    private float sqrShakeDetectionThreshold;
    
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        velocity = (transform.transform.position - lastpos)/Time.deltaTime;
        lastpos = transform.position;
    }
    void Awake()
    {
        
        //rb = GetComponent<Rigidbody>();
        if(!flashlight)
        flashlight = GetComponentInChildren<Light>();

        batteryCapacity = flashlight.intensity;
        if(!healthBarBehaviour)
        healthBarBehaviour = GetComponent<HealthBarBehaviour>();
    }

    private void Update()
    {
        if (isOn)
        {
            //Debug.Log(velocity.sqrMagnitude);
            if (velocity.sqrMagnitude >= ShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
            {
                RechargeBattery();
                timeSinceLastShake = Time.unscaledTime;

            }


            if (flashlight.intensity > 0)
            {
                flashlight.intensity -= batteryDrainSpeed * Time.deltaTime;
                UpdateUI(batteryCapacity, flashlight.intensity);
            }
        }
    }
    public void RechargeBattery()
    {
        if (flashlight.intensity < batteryCapacity)
            flashlight.intensity += batteryChargingSpeed;
    }

   public void UpdateUI(float maxBattery,float Currentbattery)
    {
        healthBarBehaviour.SetHealth(Currentbattery,maxBattery);
        SetMaterialColor();
    }

    void SetMaterialColor()
    {
        Color lightColor = Light.color;
        lightColor.a = Mathf.InverseLerp(0, batteryCapacity, flashlight.intensity);
        Light.material.SetColor("_Color",lightColor);
    }

    public void StartFlashlight()
    {
        isOn = true;
    }
}

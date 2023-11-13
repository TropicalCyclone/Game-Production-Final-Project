using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FlashlightShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private HealthBarBehaviour healthBarBehaviour;
    [SerializeField]
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
    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    private bool runOnce;
    [Header("Events")]
    [SerializeField] private UnityEvent OnShakeStart;
    [SerializeField] private UnityEvent OnShakeStop;

    // Start is called before the first frame update

    
    void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        //rb = GetComponent<Rigidbody>();
        if(!flashlight)
        flashlight = GetComponentInChildren<Light>();

        batteryCapacity = flashlight.intensity;
        if(!healthBarBehaviour)
        healthBarBehaviour = GetComponent<HealthBarBehaviour>();
    }
    private void FixedUpdate()
    {

        
        
    }

    private void Update()
    {
        velocity = (transform.position - lastpos) / Time.deltaTime;
        if (isOn)
        {
            if (IsShaking())
            {
                if (!runOnce)
                {
                    OnShakeStart.Invoke();
                }
                runOnce = false;
                RechargeBattery();
                timeSinceLastShake = Time.deltaTime;
            }
            else
            {
                if (!runOnce)
                {
                    OnShakeStop.Invoke();
                    runOnce = true;
                }
            }
                if (flashlight.intensity > 0)
                {
                    flashlight.intensity -= batteryDrainSpeed * Time.deltaTime;
                    UpdateUI(batteryCapacity, flashlight.intensity);
                }
            lastpos = transform.position;

            /*
            //Debug.Log(velocity.sqrMagnitude);
            if (velocity.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
            {
                OnShakeStart.Invoke();
                runOnce = false;
                RechargeBattery();
                timeSinceLastShake = Time.unscaledTime;
            }
            else
            {
                if (flashlight.intensity > 0)
                {
                    flashlight.intensity -= batteryDrainSpeed * Time.deltaTime;
                    UpdateUI(batteryCapacity, flashlight.intensity);
                }
                if (!runOnce)
                {
                    OnShakeStop.Invoke();
                    runOnce = true;
                }
            }
            */
        }
    }
    private bool IsShaking()
    {
        
        float delta = Time.time - timeSinceLastShake;
        float speed = velocity.magnitude / delta;
        Debug.Log(speed);
        if (speed > ShakeDetectionThreshold)
        {
            timeSinceLastShake = Time.time + MinShakeInterval;
            return true;
        }
        return false;
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

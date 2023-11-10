using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    private Color _lightColor;
    // Start is called before the first frame update
    void Start()
    {
        _lightColor = RenderSettings.ambientLight;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blink()
    {
        StartCoroutine(iBlink(0.5f, 0.2f,3));
    }

    public void SetLights(float intensity)
    {
        RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r * intensity, RenderSettings.ambientLight.g * intensity, RenderSettings.ambientLight.b * intensity);
    }

    public void LightsOff()
    {
        SetLights(0.1f);
    }

    IEnumerator iBlink(float time, float intensity, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SetLights(intensity);
            yield return new WaitForSeconds(time);
            RenderSettings.ambientLight = _lightColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

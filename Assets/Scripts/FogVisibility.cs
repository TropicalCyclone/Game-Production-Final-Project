using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogVisibility : MonoBehaviour
{
    // Single Particle System

    // private new ParticleSystem particleSystem;

    // public void Start()
    // {
    //     particleSystem = GetComponentInChildren<ParticleSystem>();
    //     particleSystem.Stop();
    // }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("ParticleZone"))
    //     {
    //         particleSystem.Play();
    //     }
    // }

    // public void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("ParticleZone"))
    //     {
    //         particleSystem.Stop();
    //     }
    // }


    // Multiple Particle System
    private ParticleSystem[] particleSystems; 

    public void Start() 
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems) 
        {
            ps.Stop();
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("ParticleZone")) 
        {
            foreach (ParticleSystem ps in particleSystems) 
            {
                ps.Play();
            }
        }
    }

    public void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("ParticleZone")) 
        {
            foreach (ParticleSystem ps in particleSystems) 
            {
                ps.Stop();
            }
        }
    }
}
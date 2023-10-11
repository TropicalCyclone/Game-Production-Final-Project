using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public Camera cameraMain;
    Plane[] cameraFrustum;
    Collider colliderMain;

    public bool lookingAtEnemy = false;

    private void Start() 
    {
        colliderMain = GetComponent<Collider>();
    }

    private void Update()
    {
        var bounds = colliderMain.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cameraMain);
        if(GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            lookingAtEnemy = true;
        }
        else 
        {
            lookingAtEnemy = false;
        }
    }
}

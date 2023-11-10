using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor56Manager : MonoBehaviour
{
    [Header("Level Progression")]
    public GameObject[] LevelObjectHider;

    public void HideAllObjects()
    {
        foreach (GameObject HideObject in LevelObjectHider)
        {
            if (HideObject != null)
            {
                HideObject.SetActive(false);
            }
        }
    }
}

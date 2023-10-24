using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetUIPosition();
    }

    void SetUIPosition()
    {
        Transform vHeadPos = Camera.main.transform;
        Vector3 vRot = Camera.main.transform.eulerAngles;
        vRot.z = 0;
        vRot.x = 0;
        this.transform.eulerAngles = vRot;
    }
}

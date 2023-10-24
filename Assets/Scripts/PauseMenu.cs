using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool mPaused = false;
    [SerializeField] private InputActionReference menuButton;
    [SerializeField] private Canvas pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        menuButton.action.started += Press;
    }
    private void OnDestroy()
    {
        menuButton.action.started -= Press;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPaused)
        {
            
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
        }
    }

    void SetUIPosition()
    {
        Transform vHeadPos = Camera.main.transform;
        Vector3 vRot = Camera.main.transform.eulerAngles;
        vRot.z = 0;
        vRot.x = 0;
        this.transform.eulerAngles = vRot;
    }
    private void Press(InputAction.CallbackContext context)
    {
        SetUIPosition();
        Debug.Log("Pressed");
        mPaused = !mPaused;
    }
}

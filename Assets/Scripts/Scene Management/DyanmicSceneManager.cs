using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DyanmicSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static DyanmicSceneManager instance { get; private set; }  
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Load("Stairwell");
        Load("Floor-1");
        Load("Floor-3");
        Load("Floor-4");
        Load("Floor-5");
        Load("Floor-6");
        Load("Floor-7");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Load(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    public void Unload(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}

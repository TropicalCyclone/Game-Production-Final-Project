using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public List<string> loadName;
    public List<string> unloadName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (string name in loadName)
            {
                DynamicSceneManager.instance.Load(name);

            }
            foreach (string name in unloadName)
            {
                UnloadScene(name);
            }
        }
    }

    private void UnloadScene(string name)
    {
        DynamicSceneManager.instance.Unload(name);
    }
}

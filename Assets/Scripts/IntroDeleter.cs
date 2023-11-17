using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDeleter : MonoBehaviour
{
    [SerializeField] private ElevatorOpen elevator;
    [SerializeField] private List<GameObject> DeleteList;

    public void DeleteAllObjects()
    {
         foreach(GameObject obj in DeleteList)
        {
            Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(NextFloorStart());
        }
    }

    IEnumerator NextFloorStart()
    {
        elevator.ElevatorDoorClose();
        yield return new WaitForSeconds(1);
        DeleteAllObjects();
        yield return null;
    }
}

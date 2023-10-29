using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private FadeScreen fader;
    [SerializeField] private float Delay = 1;
    [SerializeField] private string mSceneName = "Main Menu";
    public void RestartGame()
    {
        StartCoroutine(FadeOutandRestart());
    }
    public void ExitGame()
    {
        
        fader.FadeOut();
        SceneManager.LoadScene(mSceneName);
    }

    IEnumerator FadeOutandRestart()
    {
        menu.SetActive(false);
        fader.FadeOut();
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

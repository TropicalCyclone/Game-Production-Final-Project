using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private FadeScreen fader;
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
        fader.FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

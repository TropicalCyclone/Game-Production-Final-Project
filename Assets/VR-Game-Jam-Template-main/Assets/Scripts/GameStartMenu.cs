using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject about;


    [Header("CanvasAnimations")]
    [SerializeField]
    private Animator MainMenu;
    [SerializeField]
    private Animator Transition_Menu;

    [Header("Main Menu Buttons")]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button optionButton;
    [SerializeField]
    private Button aboutButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private List<Button> returnButtons;


    private bool Button;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        startButton.onClick.AddListener(StartGame);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void Back()
    {
            MainMenu.SetBool("Activate", false);
            Transition_Menu.SetBool("Activate", false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        MainMenu.SetBool("Activate", false);
        Transition_Menu.SetBool("Activate", false);
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }
    public void EnableOption()
    {
        MainMenu.SetBool("Activate", true);
        Transition_Menu.SetBool("Activate", true);
        options.SetActive(true);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        MainMenu.SetBool("Activate", true);
        Transition_Menu.SetBool("Activate", true);
        options.SetActive(false);
        about.SetActive(true);
    }
}

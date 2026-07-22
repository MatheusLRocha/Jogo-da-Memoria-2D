using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private  GameObject gameModePanel;
    [SerializeField] private  GameObject usernamePanel;
    public TMP_InputField inputField;
    [SerializeField] private  GameObject scorePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject creditsPanel;

    Animator anim;
    Animator animUsername;
    Animator animScore;
    Animator animTutorial;
    Animator animCredits;

    public void Awake()
    {
        SetAnimators();

        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
        usernamePanel.SetActive(false);
        scorePanel.SetActive(false);
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    private void SetAnimators()
    {
        anim = gameModePanel.GetComponent<Animator>();
        animUsername = usernamePanel.GetComponent<Animator>();
        animScore = scorePanel.GetComponent<Animator>();
        animTutorial = tutorialPanel.GetComponent<Animator>();
        animCredits = creditsPanel.GetComponent<Animator>();
    }

    public void StartStandardGameMode()
    {
        SceneManager.LoadScene(1);
    }

    public void EnterUsername()
    {
        usernamePanel.SetActive(true);
    }

    public void StartCompetitiveGameMode()
    {
        username = inputField;
        Debug.Log("O nome do usuário é:" + username);
        SceneManager.LoadScene(2);
    }

    public void OpenGameMode()
    {
        mainMenuPanel.SetActive(false);
        gameModePanel.SetActive(true);
    }

    public void CloseGameMode()
    {
        StartCoroutine(CloseAnimationPanelMode(anim, gameModePanel));
    }

    public void OpenScore()
    {
        mainMenuPanel.SetActive(false);
        scorePanel.SetActive(true);
    }
    public void CloseScore()
    {
        StartCoroutine(CloseAnimationPanelMode(animScore, scorePanel));
    }
    public void OpenTutorial()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void SkipTutorial()
    {
        StartCoroutine(CloseAnimationPanelMode(animCredits, creditsPanel));
    }
    public void CloseTutorial()
    {
        StartCoroutine(CloseAnimationPanelMode(animTutorial, tutorialPanel));
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        StartCoroutine(CloseAnimationPanelMode(animCredits, creditsPanel));
    }
    
    public System.Collections.IEnumerator CloseAnimationPanelMode(Animator animator, GameObject panel)
    {
        animator.SetBool("Closer", true);
        yield return new WaitForSeconds(0.4f);
        mainMenuPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
        panel.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0.0f);
    }
    
    public void ExitGame()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
using System.Linq;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private  GameObject gameModePanel;
    [SerializeField] private  GameObject usernamePanel;
    [SerializeField] public GameObject wrongAnswer;
    public TMP_InputField inputField;
    public string username;
    [SerializeField] private  GameObject scorePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject creditsPanel;

    Animator anim;
    Animator animWrong;
    Animator animScore;
    Animator animTutorial;
    Animator animCredits;
    public bool skipper;

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
        animWrong = wrongAnswer.GetComponent<Animator>();
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
        if (inputField.text.Length >= 1 && inputField.text != " ")
        {
            username = inputField.text;
            Debug.Log("O nome do usuário é: " + username);
            SceneManager.LoadScene(2);
        }
        else
        {
            StartCoroutine(Wronger());
        }    
    }

    public System.Collections.IEnumerator Wronger()
    {
        animWrong.SetBool("Wronger", true);
        yield return new WaitForSeconds(0.4f);
        animWrong.SetBool("Wronger", false);
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
        animTutorial.SetBool("Faster", true);
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
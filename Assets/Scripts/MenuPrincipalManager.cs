using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private  GameObject painelModoJogo;
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private GameObject painelCreditos;
    Animator modoAnim;
    Animator tutorialAnim;
    Animator creditosAnim;
    public void Awake()
    {
        modoAnim = painelModoJogo.GetComponent<Animator>();
        tutorialAnim = painelTutorial.GetComponent<Animator>();
        creditosAnim = painelCreditos.GetComponent<Animator>();
        painelMenuInicial.SetActive(true);
        painelModoJogo.SetActive(false);
        painelCreditos.SetActive(false);
        painelTutorial.SetActive(false);
    }
    public void Padrao()
    {
        SceneManager.LoadScene(1);
    }

    public void Extra()
    {
        SceneManager.LoadScene(1);
    }

    public void AbrirModoJogo()
    {
        painelMenuInicial.SetActive(false);
        painelModoJogo.SetActive(true);
    }
    public void FecharModoJogo()
    {
        StartCoroutine(fecharModoJogo());
    }
    public System.Collections.IEnumerator fecharModoJogo()
    {
        modoAnim.SetBool("Closer", true);
        yield return new WaitForSeconds(0.4f);
        painelMenuInicial.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        painelModoJogo.SetActive(false);
        painelModoJogo.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0.0f);
    }

    public void AbrirTutorial()
    {
        painelMenuInicial.SetActive(false);
        painelTutorial.SetActive(true);
    }
    public void PularTutorial()
    {
        tutorialAnim.SetBool("Faster", true);
    }
    public void FecharTutorial()
    {
        StartCoroutine(fecharTutorial());
    }
    public System.Collections.IEnumerator fecharTutorial()
    {
        tutorialAnim.SetBool("Closer", true);
        yield return new WaitForSeconds(0.4f);
        painelMenuInicial.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        painelTutorial.SetActive(false);
        painelTutorial.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0.0f);
    }
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
    }
    public void FecharCreditos()
    {
        StartCoroutine(fecharCreditos());
    }
    public System.Collections.IEnumerator fecharCreditos()
    {
        creditosAnim.SetBool("Closer", true);
        yield return new WaitForSeconds(0.4f);
        painelMenuInicial.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        painelCreditos.SetActive(false);
        painelCreditos.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0.0f);
    }
    public void SairDoJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}

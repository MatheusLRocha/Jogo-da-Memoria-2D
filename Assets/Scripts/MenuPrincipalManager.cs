using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private  GameObject painelModoJogo;
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private GameObject painelCreditos;
    public void Awake()
    {
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
        painelMenuInicial.SetActive(true);
        painelModoJogo.SetActive(false);
    }

    public void AbrirTutorial()
    {
        painelMenuInicial.SetActive(false);
        painelTutorial.SetActive(true);
    }

    public void FecharTutorial()
    {
        painelMenuInicial.SetActive(true);
        painelTutorial.SetActive(false);
    }
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
    }

    public void Fecharcreditos()
    {
        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void SairDoJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}

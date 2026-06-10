using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoModoDeJogo;
    [SerializeField] private  GameObject painelModoJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelCreditos;
    public void Padrao()
    {
        SceneManager.LoadScene(1);
    }

    public void Extra()
    {
        SceneManager.LoadScene(nomeDoModoDeJogo);
    }

    public void AbrirModoJogo()
    {
        painelMenuInicial.SetActive(false);

        painelCreditos.SetActive(false);

        painelModoJogo.SetActive(true);
        
    }

    public void FecharModoJogo()
    {
        painelMenuInicial.SetActive(true);
        painelCreditos.SetActive(false);
        painelModoJogo.SetActive(false);
    }

    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
        painelModoJogo.SetActive(false);
    }

    public void Fecharcreditos()
    {
        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);
        painelModoJogo.SetActive(false);
    }
    public void SairDoJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}

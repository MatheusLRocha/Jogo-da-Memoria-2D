using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Variável do componente Animator para modificar as animações da carta
    Animator anim;

    // Cria uma classificação dos tipos de carta e adiciona cada um em cada carta
    public enum CardType
    {
        Administracao,
        CienciasContabeis,
        Logistica,
        GestaoFinanceira,
        Marketing,
        RecursosHumanos,
        GestaoComercial,
        ProcessosGerenciais,
        CienciasComputacao,
        AnaliseDesenvolvimentoSistemas,
        SitemasInformacao,
        JogosDigitais,
        InteligenciaArtificialAplicada,
    }

// Cria estados da carta para modificar as animações
    public enum CardState
    {
        Idle,
        Selected,
        Matched,
    }

    public CardType cardType;
    public CardState cardState;

    
    void Start()
    {
        // Pega o componente Animator do objeto
        anim = GetComponent<Animator>();
    }

    // Função que verifica e faz as mudanças de animação na carta
    public void ChangeState(CardState newState)
    {
        // Limpa os estados antigos para usar os novos
        if (cardState == CardState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (cardState == CardState.Selected)
        {
            anim.SetBool("isSelected", false);
        }

        // Variável recebe o novo estado
        cardState = newState;

        // Atualiza os estados
        if (cardState == CardState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (cardState == CardState.Selected)
        {
            anim.SetBool("isSelected", true);
        }
    }
}

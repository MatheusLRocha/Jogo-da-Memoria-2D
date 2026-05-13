using System.Diagnostics;
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
        Dismatched
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
        anim.SetBool("isIdle", false);
        anim.SetBool("isSelected", false);
        anim.SetBool("isMatched", false);
        anim.SetBool("isDismatched", false);

        // Variável recebe o novo estado
        cardState = newState;

        // Atualiza os estados
        switch (cardState) 
        {
            case CardState.Idle:
                anim.SetBool("isIdle", true);
                break;
            
            case CardState.Selected:
                anim.SetBool("isSelected", true);
                break;

            case CardState.Matched:
                anim.SetBool("isMatched", true);
                break;

            case CardState.Dismatched:
                anim.SetBool("isDismatched", true);
                break;
        }
    }
}

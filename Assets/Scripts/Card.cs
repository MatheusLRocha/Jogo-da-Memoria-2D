using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    /// Coisas para mexer amanhã (sprite muito pequeno e outros problemas)
    //public Sprite frente;
    //public Sprite verso;

    //private SpriteRenderer sr;
    //private Image img;
    //private bool mostrandoFrente = false;


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
    public int playerID;
    
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
    public void CardContent()
    {
    if (cardType == CardType.Administracao)
        {
            if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.CienciasContabeis)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.Logistica)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.GestaoFinanceira)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.Marketing)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.RecursosHumanos)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.GestaoComercial)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.ProcessosGerenciais)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.CienciasComputacao)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.AnaliseDesenvolvimentoSistemas)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.SitemasInformacao)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.JogosDigitais)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    else if(cardType == CardType.InteligenciaArtificialAplicada)
        { 
        if (playerID == 1)
            {
                // Texto administração
            }
            else
            {
                // Imagem Administração
            }
            // Fazer a Instantiate randomizada já internamente
        }
    }
}


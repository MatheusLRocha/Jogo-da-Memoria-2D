using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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

    // Lista de sprites das cartas
    [SerializeField] public List<Sprite> Sprites;

    // Variável que mostra qual o sprite da carta em específico
    public Sprite thisSprite;

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
    
    // Propriedade que obtém o playerID do componente PlayerControl pai
    public int playerID;
    void Start()
    {
        // Pega o componente Animator do objeto
        anim = GetComponent<Animator>();
        StartCoroutine(StartShowing());
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

    // (Incompleto) Função de selecionar o sprite específico da carta
    public void CardContent()
    {
        if (playerID == 1)
        {
            thisSprite = Sprites[0];
        }
        else
        {
            thisSprite = Sprites[0];
        }

    }

    // Mostra as cartas no inicio
    private IEnumerator StartShowing()
    {
        Sprite waiter;
        waiter = GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = thisSprite;
        yield return new WaitForSeconds(2.2f);
        GetComponent<SpriteRenderer>().sprite = waiter;
    }
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


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
    SpriteRenderer spriteRenderer;

    // Lista de sprites das cartas
    [SerializeField] public List<Sprite> Sprites;

    // Variável que mostra qual o sprite da carta em específico
    public Sprite thisSprite;
    public Sprite backSprite;
    public bool SpriteChecker = false;
    private bool contentSet = false;

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

    void Awake()
    {
        // Captura o SpriteRenderer e salva o sprite original do prefab o mais cedo possível
        spriteRenderer = GetComponent<SpriteRenderer>();
        backSprite = spriteRenderer.sprite;
    }

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

    // Função que seleciona o sprite específico da carta baseado no cardType
    public void CardContent()
    {
        if (!SpriteChecker) return;
        
        int spriteIndex = (int)cardType;
        
        if (spriteIndex >= 0 && spriteIndex < Sprites.Count)
        {
            thisSprite = Sprites[spriteIndex];
            contentSet = true;
        }
        else
        {
            Debug.LogWarning($"Sprite index {spriteIndex} out of range for card type {cardType}");
        }
    }

    // Mostra as cartas no inicio
    private IEnumerator StartShowing()
    {
        yield return new WaitForSeconds(0.2f);
        
        // Aguarda CardContent() ser chamado para usar o sprite correto
        while (!contentSet)
        {
            yield return null;
        }
        
        spriteRenderer.sprite = thisSprite;
        yield return new WaitForSeconds(10f);
        spriteRenderer.sprite = backSprite;
    }
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Card : MonoBehaviour
{
    Animator anim;
    
    SpriteRenderer spriteRenderer;

    [SerializeField] public List<Sprite> Sprites;

    public Sprite actualSprite;
    //Variável de backup do sprite antigo
    public Sprite backupOldSprite;

    public bool SpriteChecker = false;

    private bool contentSet = false;

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

    void Awake()
    {
        // Captura o SpriteRenderer e salva o sprite original do prefab o mais cedo possível
        spriteRenderer = GetComponent<SpriteRenderer>();
        backupOldSprite = spriteRenderer.sprite;
    }

    void Start()
    {
        // Pega o componente Animator do objeto
        anim = GetComponent<Animator>();
        StartCoroutine(StartShowing());
    }


    public void HandleCardState(CardState newState)
    {
        ClearOldCardStates();
        ChangeState(newState);
    }

    public void Select()
    {
        ChangeState(CardState.Selected);
    }

    public void Match()
    {
        ChangeState(CardState.Matched);
    }

    public void Dismatch()
    {
        ChangeState(CardState.Dismatched);
    }

    public void Idle()
    {
        ChangeState(CardState.Idle);
    }

    public void RevealCard()
    {
        spriteRenderer.sprite = actualSprite;
    }

    public void HideCard()
    {
        spriteRenderer.sprite = backupOldSprite;
    }

    private void ClearOldCardStates()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isSelected", false);
        anim.SetBool("isMatched", false);
        anim.SetBool("isDismatched", false);
    }

    private void ChangeState(CardState newState)
    {
        ClearOldCardStates();

        cardState = newState;

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

        actualSprite = Sprites[spriteIndex];
        contentSet = true;
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
        
        spriteRenderer.sprite = actualSprite;
        yield return new WaitForSeconds(10f);
        spriteRenderer.sprite = backupOldSprite;
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Card : MonoBehaviour
{
    Animator anim;
    
    SpriteRenderer spriteRenderer;

    [SerializeField] public List<Sprite> Sprites;

    public Sprite actualSprite;
    
    public Sprite backupOldSprite;

    public bool SpriteChecker = false;

    //private bool contentSet = false;

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
    private int scene;

    void Awake()
    {
        // Captura o SpriteRenderer e salva o sprite original do prefab o mais cedo possível
        spriteRenderer = GetComponent<SpriteRenderer>();
        backupOldSprite = spriteRenderer.sprite;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene().buildIndex;
        if (scene ==2)
            anim.SetBool("IsComp",true);
        StartShowing();
    }

    public void Match()
    {
        ChangeAnimation(CardState.Matched);
        ChangeSprite(true);
    }

    public void Dismatch()
    {
        ChangeAnimation(CardState.Dismatched);
        ChangeSprite(false);
    }

    public void ChangeSprite(bool isMatched)
    {
        if (isMatched)
        {
            RevealCard();
        } 
        else
        {
            RevealCard();
            float waitForHide = scene == 2 ? 1.27f : 3.25f;
            DOVirtual.DelayedCall(waitForHide, () => {
                HideCard();
                anim.SetBool("isDismatched", false);
                });
        }
    }

    
    public void ChangeAnimation(CardState newState)
    {
        ClearOldCardStates();
        ChangeState(newState);
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
        //contentSet = true;
    }


    // Mostra as cartas no inicio
    private void StartShowing()
    {
        float waitForOpen = scene == 2 ? 1.15f : 1.35f;
        float waitForStart = scene == 2 ? 6.05f : 11.25f;
        DOTween.SetTweensCapacity(200, 150);
        DOVirtual.DelayedCall(waitForOpen, () => 
        {
            spriteRenderer.sprite = actualSprite;
        });
        DOVirtual.DelayedCall(waitForStart, () =>
        {
            spriteRenderer.sprite = backupOldSprite;
        });
    }
}
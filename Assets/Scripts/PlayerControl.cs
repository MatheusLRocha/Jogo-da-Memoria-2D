using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    
    // Apenas cria um cabeçalho no Unity para melhor organização
    [Header("Configurações do jogador")]

    // Cria um campo público para escolher quem é o jogador 1 e quem é o jogador 2
    [SerializeField] public int playerID;

    [SerializeField] public List<Transform> posicoes;

    // Campo para selecionar onde as cartas vão nascer
    [SerializeField] public Transform pai;

    [SerializeField] public GameObject CardPrefab;

    // Cria uma lista de objetos para receber todas as cartas do jogo
    [SerializeField] List<GameObject> cards;

    // Gera um index atual para transitar entre as cartas
    public int currentIndex = 0;
    private bool youCanMoveNow = false;

    List<int> choosen = new List<int>();

    private int UP = -4;
    private int DOWN = 4;
    private int LEFT = -1;
    private int RIGHT = 1;

    void Start()
    {
        InstantiateCards();
        currentIndex = 0;
        // Mostra que a carta inicial do baralho de cada jogador já começa selecionada no início do jogo
        cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.5f, 1.5f, 0.0f);
        cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(cards[currentIndex].GetComponent<Transform>().localPosition.x, cards[currentIndex].GetComponent<Transform>().localPosition.y, -1f);            
        StartCoroutine(StartShowing());
    }   
    
    void InstantiateCards()
    {
        // Grande loop que randomiza as cartas ao mesmo tempo que cria elas
        for (int i = posicoes.Count - 1; i > -1; i--)
        {
            cards[i] = Instantiate(CardPrefab, posicoes[i].position, Quaternion.identity, pai);
            Card card = cards[i].GetComponent<Card>();
            
            // Garante acesso do playerID para a carta da mão específica
            card.playerID = playerID;

            // Escolhe aleatoriamente um tipo de carta a partir do enum Card.CardType
            int typeCount = Enum.GetValues(typeof(Card.CardType)).Length;
            bool antiRepeat = true;
            while (antiRepeat)
            {
                int possibleType = UnityEngine.Random.Range(0, typeCount);
                bool alreadyChosen = false;

                for (int l = 0; l < choosen.Count; l++)
                {
                    if (possibleType == choosen[l])
                    {
                        alreadyChosen = true;
                        break;
                    }
                }

                if (!alreadyChosen)
                {
                    card.cardType = (Card.CardType)possibleType;
                    choosen.Add(possibleType);
                    antiRepeat = false;
                }
            }
            card.SpriteChecker = true;
            card.CardContent();
        }
    }



    void Update()
    {
        // Verifica a cada frame se o jogador se movimentou
        HandlePlayerControls();
    }

    void SetWASDControls()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame) ChangeCard(LEFT);
        if (Keyboard.current.dKey.wasPressedThisFrame) ChangeCard(RIGHT);
        if (Keyboard.current.wKey.wasPressedThisFrame) ChangeCard(UP);
        if (Keyboard.current.sKey.wasPressedThisFrame) ChangeCard(DOWN);
    }

    void SetArrowControls()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) ChangeCard(LEFT);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) ChangeCard(RIGHT);
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) ChangeCard(UP);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) ChangeCard(DOWN);
    }

    void SetPlayerMovement()
    {
        if (VerifyPlayer())
        {
            SetWASDControls();
        }
        else
        {
            SetArrowControls();
        }
    }

    void HandlePlayerMovement()
    {
        SetPlayerMovement();
    }

    void SetPlayerSelectionCard()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            // Pega o componente Card(Script) da carta atual e muda seu estado para executar a animação
            Card card = cards[currentIndex].GetComponent<Card>();
            card.ChangeState(Card.CardState.Selected);

            // Acessa o script do GameManager com o ID do jogador e a carta que foi selecionada por ele
            GameManager.instance.VerificarTipos(playerID, cards[currentIndex].GetComponent<Card>());
            cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.3f, 1.3f, 0.0f);
            cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(cards[currentIndex].GetComponent<Transform>().localPosition.x, cards[currentIndex].GetComponent<Transform>().localPosition.y, 0f);  
            ChangeCard(1);
            StartCoroutine(StopMovimentation());
        }
    }

    void HandlePlayerSelectionCard()
    {
        SetPlayerSelectionCard();
    }

    bool VerifyPlayer()
    {
        return playerID == 1;
    }

    void HandlePlayerControls()
    {
        if (WindowManager.instance.isWindowActive == false && youCanMoveNow)
        {
            HandlePlayerMovement();
            HandlePlayerSelectionCard();
        }
    }

    public void ChangeCard(int direction)
    {
        // Limpa a seleção da carta anterior e pula se a carta já tiver sido a correta
        if (cards[currentIndex].GetComponent<Card>().cardState != Card.CardState.Matched)
        {
            cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.3f, 1.3f, 0.0f);
            cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(cards[currentIndex].GetComponent<Transform>().localPosition.x, cards[currentIndex].GetComponent<Transform>().localPosition.y, 0f);            
            
            // Pega o componente Card da carta antiga e limpa seu estado para o modo parado
            Card card = cards[currentIndex].GetComponent<Card>();
            card.ChangeState(Card.CardState.Idle);

            int newIndex = currentIndex + direction;

            if (newIndex > cards.Count - 1)
            {
                newIndex = 0;
            }
            else if (newIndex < 0)
            {
                newIndex = cards.Count - 1;
            }
        
            currentIndex = newIndex;
        }

        // Verifica os índices dos objetos que ja deram match
        while (cards[currentIndex].GetComponent<Card>().cardState == Card.CardState.Matched)
        {
            // Equanto estiver percorrendo pelo objeto matched, muda o índice até o próximo valor existente não matched
            currentIndex += direction;

            if (currentIndex > cards.Count - 1)
            {
                currentIndex = 0;
            }
            else if (currentIndex < 0)
            {
                currentIndex = cards.Count - 1;
            }
        }
        

        // Seleciona a próxima carta
        cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.5f, 1.5f, 0.0f);
        cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(cards[currentIndex].GetComponent<Transform>().localPosition.x, cards[currentIndex].GetComponent<Transform>().localPosition.y, -1f);            
    }
    private System.Collections.IEnumerator StartShowing()
    {
        yield return new WaitForSeconds(10.5f);
        youCanMoveNow = true;
    }

    private System.Collections.IEnumerator StopMovimentation()
    {
        youCanMoveNow = false;
        yield return new WaitForSeconds(5f);
        youCanMoveNow = true;
    }
}

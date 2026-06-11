using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEditor.PackageManager;
using System.Numerics;


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
            cards[i] = Instantiate(CardPrefab, posicoes[i].position, UnityEngine.Quaternion.identity, pai);
            Card card = cards[i].GetComponent<Card>();
            
            // Garante acesso do playerID para a carta da mão específica
            card.playerID = playerID;

            // Escolhe aleatoriamente um tipo de carta a partir do enum Card.CardType
            int typeCount = Enum.GetValues(typeof(Card.CardType)).Length;
            bool antiRepeat = true;
            while (antiRepeat)
            {
                int j = UnityEngine.Random.Range(0, typeCount);
                bool alreadyChosen = false;

                for (int l = 0; l < choosen.Count; l++)
                {
                    if (j == choosen[l])
                    {
                        alreadyChosen = true;
                        break;
                    }
                }

                if (!alreadyChosen)
                {
                    card.cardType = (Card.CardType)j;
                    choosen.Add(j);
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
        VerifyPlayer();
    }

    void VerifyPlayer()
    {
        if (WindowManager.instance.isWindowActive == false && youCanMoveNow)
        {
            if (playerID == 1)
            {
                // Verifica se o jogador está se movimentando para a direita ou para a esquerda
                if (Keyboard.current.dKey.wasPressedThisFrame)
                    ChangeCard(1);
                else if (Keyboard.current.aKey.wasPressedThisFrame)
                    ChangeCard(-1);
                else if (Keyboard.current.wKey.wasPressedThisFrame)
                    ChangeCard(-4);
                else if (Keyboard.current.sKey.wasPressedThisFrame)
                    ChangeCard(4); 
                    
            }
            else if (playerID == 2)
            {
                if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                    ChangeCard(1);
                else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                    ChangeCard(-1);
                else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                    ChangeCard(-4);
                else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                    ChangeCard(4);
            }

            // Verifica se algum dos jogadores apertou o botão para selecionar as cartas
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                // Pega o componente Card(Script) da carta atual e muda seu estado para executar a animação
                Card card = cards[currentIndex].GetComponent<Card>();
                card.ChangeState(Card.CardState.Selected);

                // Acessa o script do GameManager com o ID do jogador e a carta que foi selecionada por ele
                GameManager.instance.VerificarTipos(playerID, cards[currentIndex].GetComponent<Card>());
                cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.2f, 1.2f, 0.0f);
                cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(cards[currentIndex].GetComponent<Transform>().localPosition.x, cards[currentIndex].GetComponent<Transform>().localPosition.y, 0f);  
                ChangeCard(1);
            }
        }
        else{
            return;
        }
    }

    public void ChangeCard(int direction)
    {
        // Limpa a seleção da carta anterior e pula se a carta já tiver sido a correta
        if (cards[currentIndex].GetComponent<Card>().cardState != Card.CardState.Matched)
        {
            cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(1.2f, 1.2f, 0.0f);
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
        yield return new WaitForSeconds(2.6f);
        youCanMoveNow = true;
    }
}

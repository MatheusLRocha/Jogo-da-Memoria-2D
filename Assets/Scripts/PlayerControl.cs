using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    // Apenas cria um cabeçalho no Unity para melhor organização
    [Header("Configurações do jogador")]

    // Cria um campo público para escolher quem é o jogador 1 e quem é o jogador 2
    [SerializeField] int playerID;

    // Cria uma lista de objetos para receber todas as cartas do jogo
    [SerializeField] List<GameObject> cards;

    // Gera um index atual para transitar entre as cartas
    int currentIndex = 0;

    void Start()
    {
        // Mostra que a carta inicial do baralho de cada jogador já começa selecionada no início do jogo
        cards[currentIndex].GetComponent<SpriteRenderer>().color = Color.red;
    }

    void Update()
    {
        VerifyPlayer();
    }

    void VerifyPlayer()
    {
        if (playerID == 1)
        {
            // Verifica se o jogador está se movimentando para a direita ou para a esquerda
            if (Keyboard.current.dKey.wasPressedThisFrame)
                ChangeCard(1);
            else if (Keyboard.current.aKey.wasPressedThisFrame)
                ChangeCard(-1);
                
        }
        else if (playerID == 2)
        {
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                ChangeCard(1);
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                ChangeCard(-1);
        }

        // Verifica se algum dos jogadores apertou o botão para selecionar as cartas
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            // Pega o componente Card(Script) da carta atual e muda seu estado para executar a animação
            Card card = cards[currentIndex].GetComponent<Card>();
            card.ChangeState(Card.CardState.Selected);

            // Acessa o script do GameManager com o ID do jogador e a carta que foi selecionada por ele
            GameManager.instance.VerificarTipos(playerID, cards[currentIndex].GetComponent<Card>());
        }
    }

    public void ChangeCard(int direction)
    {
        // Limpa a seleção da carta anterior e pula se a carta já tiver sido destruída
        if (cards[currentIndex].GetComponent<Card>().cardState != Card.CardState.Matched)
        {
            cards[currentIndex].GetComponent<SpriteRenderer>().color = Color.white;
        
        

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

        // Verifica os índices dos objetos destruídos
        while (cards[currentIndex].GetComponent<Card>().cardState == Card.CardState.Matched)
        {
            // Equanto estiver percorrendo pelo objeto destruído, muda o índice até o próximo valor existente
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
        cards[currentIndex].GetComponent<SpriteRenderer>().color = Color.red;

        
    }
}


using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.SceneManagement;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Gerencia questões como pontuação, verificação de valores semelhantes, entre outros

    public static GameManager instance; // Torna o script globalmente acessível para outros scripts

    // Função Awake executa antes da Start()
    void Awake()
    {
        // Verifica se já existe uma cópia da instância ou não
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Cria as variáveis para acessar os cards de cada jogador
    public Card cardJogador1;
    public Card cardJogador2;

    // Cria variável para mexer no texto
    public TextMeshProUGUI scoreText;
    int currentPoints = 0;

    // Verifica frame por frame se os pontos chegaram no limite
    void Update()
    {
        //EndGame(currentPoints);
    }

    // Função verifica a compatibilidade entre a carta do jogador 1 de do jogador 2 para pontuação
    public void VerificarTipos(int id, Card card)
    {
        if (id == 1) cardJogador1 = card;
        if (id == 2) cardJogador2 = card;

        // Verifica se ambos os jogadores selecionaram uma carta
        if (cardJogador1 != null && cardJogador2 != null)
        {
            // Armazena a referencia de cada um antes que sejam limpos
            Card card1 = cardJogador1;
            Card card2 = cardJogador2;

            // Se o tipo do card dos dois jogadores forem iguais, eles pontuam, caso contrário, perdem um ponto
            if (card1.cardType == card2.cardType)
            {
                // Pega o respectivo índice da carta matched para pegar seus dados e inserir na janela
                WindowManager.instance.matchedTypeNumber = (int)card1.cardType;

                Debug.Log("Os tipos são iguais " + card1.cardType + " " + card2.cardType);
                UpdatePoints(1);

                //Muda o estado das cartas para Matched e depois muda de cor                 
                StartCoroutine(TrocarAnimacao(card1, card2, Card.CardState.Matched)); 
            }
            else
            {
                Debug.Log("Tipos diferentes! " + card1.cardType + " " + card2.cardType);
                UpdatePoints(-1);

                StartCoroutine(TrocarAnimacao(card1, card2, Card.CardState.Dismatched));
            }

            // Limpa as cartas para a próxima jogada
            cardJogador1 = null;
            cardJogador2 = null;
        }
    }

    // Verifica a pontuação para finalizar o jogo
    //void EndGame(int points)
    //{
    //    if (points == 5)
    //     {
    //        SceneManager.LoadScene("SampleScene");
    //    } 
    // }


    // Atualiza a pontuação e seu respectivo texto
    void UpdatePoints(int value)
    {
        currentPoints += value;
        scoreText.text = currentPoints + "/13";
    }



    //Coroutine serve como  um "temporizador" para trocar e limpar a animação
    IEnumerator TrocarAnimacao(Card card1, Card card2, Card.CardState animacao)
    {
        // Wait a frame to ensure animator state change is registered
        yield return null;
        //Inicia a animação das cartas
        card1.ChangeState(animacao);
        card2.ChangeState(animacao);
        
        if (animacao == Card.CardState.Matched)
        {
            
            yield return new WaitForSeconds(0.17f);
            //Divide o tempo para colocar o sprite da carta virada e depois ativa a tela
            card1.GetComponent<SpriteRenderer>().sprite = card1.thisSprite;
            card2.GetComponent<SpriteRenderer>().sprite = card2.thisSprite;

            yield return new WaitForSeconds(0.9f);
            
            // Faz com que no gerenciador de janelas afirme que o match foi feito
            WindowManager.instance.hasMatched = true;
            
            
        }
        else if (animacao == Card.CardState.Dismatched)
        {
            // Wait for the dismatched animation to play
            yield return new WaitForSeconds(0.18f);

            // Divide o tempo para mostrar a carta de trás e, depois, voltar à original
            card1.GetComponent<SpriteRenderer>().sprite = card1.thisSprite;
            card2.GetComponent<SpriteRenderer>().sprite = card2.thisSprite;

            yield return new WaitForSeconds(1.40f);

            card1.GetComponent<SpriteRenderer>().sprite = card1.backSprite;
            card2.GetComponent<SpriteRenderer>().sprite = card2.backSprite;

            yield return new WaitForSeconds(0.6f);

            // Reset cards back to Idle state
            card1.ChangeState(Card.CardState.Idle);
            card2.ChangeState(Card.CardState.Idle);
        }
    }
}

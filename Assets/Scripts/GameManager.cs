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

            // Se o tipo do card dos dois jogadores forem iguais, eles pontuam, caso contrário, perdem um ponto
            if (cardJogador1.cardType == cardJogador2.cardType)
            {
                WindowManager.instance.matchedTypeNumber = (int)cardJogador1.cardType;
                Debug.Log("Os tipos são iguais " + cardJogador1.cardType + " " + cardJogador2.cardType);
                UpdatePoints(1);
                //Muda o estado das cartas para Matched e depois muda de cor                 
                StartCoroutine(TrocarAnimacao(Card.CardState.Matched)); 
            }
            else
            {
                Debug.Log("Tipos diferentes! " + cardJogador1.cardType + " " + cardJogador2.cardType);
                UpdatePoints(-1);

                cardJogador1.ChangeState(Card.CardState.Dismatched);
                cardJogador2.ChangeState(Card.CardState.Dismatched);
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

    void UpdatePoints(int value)
    {
        currentPoints += value;
        scoreText.text = currentPoints + "/5";
    }



    //Coroutine serve como  um "temporizador" para trocar e limpar a animação

    IEnumerator TrocarAnimacao(Card.CardState animacao)
    {
        //yield return new WaitForSeconds(1f);
        cardJogador1.ChangeState(animacao);
        cardJogador2.ChangeState(animacao);
        if (animacao == Card.CardState.Matched)
        {
            cardJogador1.GetComponent<SpriteRenderer>().color = Color.blue;
            cardJogador2.GetComponent<SpriteRenderer>().color = Color.blue; 
        }
        
        WindowManager.instance.hasMatched = true;
        // yield return serve para "pausar" a coroutine por um certo tempo e depois continuar o resto de sua execução se tiver após ele
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Coroutine feita após 1.5s");


        // Verifica se as cartas não estão nulas para aplicar as animações
        //if (cardJogador1 != null) cardJogador1.ChangeState(Card.CardState.Idle);
        //if (cardJogador2 != null) cardJogador2.ChangeState(Card.CardState.Idle);

        // Limpa os valores para a próxima rodada após executar todas as animações
        cardJogador1 = null;
        cardJogador2 = null;
    }
}

using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.SceneManagement;
using System.Collections;

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
    Card cardJogador1;
    Card cardJogador2;

    // Cria variável para mexer no texto
    public TextMeshProUGUI scoreText;
    int currentPoints = 0;

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
                Debug.Log("Os tipos são iguais " + cardJogador1.cardType + " " + cardJogador2.cardType);
                UpdatePoints(1);
                //Muda o estado das cartas para Matched e depois destroi os objetos
                cardJogador1.ChangeState(Card.CardState.Matched);
                cardJogador2.ChangeState(Card.CardState.Matched);
                cardJogador1.GetComponent<SpriteRenderer>().color = Color.blue;
                cardJogador2.GetComponent<SpriteRenderer>().color = Color.blue;                  
                StartCoroutine(TrocarAnimacao(Card.CardState.Matched));                   
                //Destroy(cardJogador1.gameObject);
                //Destroy(cardJogador2.gameObject);
            }
            else
            {
                StartCoroutine(TrocarAnimacao(Card.CardState.Dismatched));
                Debug.Log("Tipos diferentes! " + cardJogador1.cardType + " " + cardJogador2.cardType);
                UpdatePoints(-1);
            }

            
        }
    }

    void UpdatePoints(int value)
    {
        currentPoints += value;
        scoreText.text = currentPoints + "/5"; 
    }


    // Coroutine que serve como um "temporizador" para trocar e limpar a animação
    IEnumerator TrocarAnimacao(Card.CardState animacao)
    {
        yield return new WaitForSeconds(1f);

        cardJogador1.ChangeState(animacao);
        cardJogador2.ChangeState(animacao);

        // yield return serve para "pausar" a coroutine por um certo tempo e depois continuar o resto de sua execução se tiver após ele
        yield return new WaitForSeconds(1.5f); 

        Debug.Log("Coroutine feita após 1.5s");

        // Verifica se as cartas não estão nulas para aplicar as animações
        if (cardJogador1 != null) cardJogador1.ChangeState(Card.CardState.Idle);
        if (cardJogador2 != null) cardJogador2.ChangeState(Card.CardState.Idle);

        // Limpa os valores para a próxima rodada após executar todas as animações
        cardJogador1 = null;
        cardJogador2 = null;
    }
}

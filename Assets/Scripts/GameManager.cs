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
                //Muda o estado das cartas para Matched
                cardJogador1.GetComponent<SpriteRenderer>().color = Color.blue;
                cardJogador2.GetComponent<SpriteRenderer>().color = Color.blue;                  
                
                cardJogador1.ChangeState(Card.CardState.Matched);
                cardJogador2.ChangeState(Card.CardState.Matched);                   
            }
            else
            {
                cardJogador1.ChangeState(Card.CardState.Dismatched);
                cardJogador1.ChangeState(Card.CardState.Dismatched);
                Debug.Log("Tipos diferentes! " + cardJogador1.cardType + " " + cardJogador2.cardType);
                UpdatePoints(-1);
            }

            cardJogador1 = null;
            cardJogador2 = null;
        }
    }

    void UpdatePoints(int value)
    {
        currentPoints += value;
        scoreText.text = currentPoints + "/5"; 
    }    
}

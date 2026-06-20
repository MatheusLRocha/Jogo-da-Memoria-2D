using System;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] public List<GameObject> cards;
    
    List<int> choosen = new List<int>();

    void Start()
    {
        InstantiateCards();
        RandomizeCards();    
    }   
    
    public void InstantiateCards()
    {
        // Grande loop que randomiza as cartas ao mesmo tempo que cria elas
        for (int i = posicoes.Count - 1; i > -1; i--)
        {
            cards[i] = Instantiate(CardPrefab, posicoes[i].position, Quaternion.identity, pai);
        }
    }

    void RandomizeCards()
    {
        // Escolhe aleatoriamente um tipo de carta a partir do enum Card.CardType
        for (int i = posicoes.Count - 1; i > -1; i--)
        {
            Card card = cards[i].GetComponent<Card>();
            card.playerID = playerID;

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
}
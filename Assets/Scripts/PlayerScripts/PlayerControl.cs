using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Configurações do jogador")]

    [SerializeField] public int playerID;

    [SerializeField] public List<Transform> posicoes;

    // Campo para selecionar onde as cartas vão nascer
    [SerializeField] public Transform parent;

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
        for (int i = posicoes.Count - 1; i > -1; i--)
        {
            cards[i] = Instantiate(CardPrefab, posicoes[i].position, Quaternion.identity, parent);
        }
    }

    void RandomizeCards()
    {
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
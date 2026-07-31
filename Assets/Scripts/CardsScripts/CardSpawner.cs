using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] public Transform cardsParent;

    [SerializeField] public List<Transform> cardPositions;

    [SerializeField] public GameObject CardPrefab;

    [SerializeField] public List<GameObject> cards;

    void Start()
    {
        InstantiateCards();
    }

    void InstantiateCards()
    {
        for (int i = cardPositions.Count - 1; i > -1; i--)
        {
            cards[i] = Instantiate(CardPrefab, cardPositions[i].position, Quaternion.identity, cardsParent);
        }
    }
}

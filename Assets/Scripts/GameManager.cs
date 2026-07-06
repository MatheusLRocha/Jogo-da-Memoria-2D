using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Torna o script globalmente acessível para outros scripts

    

    public Card cardPlayer1;
    public Card cardPlayer2;

    public TextMeshProUGUI scoreText;
    int currentPoints = 0;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //EndGame(currentPoints);
    }

    public void VerifyCardTypes(int id, Card card)
    {
        SetPlayersCards(id, card);

        if (!IsAllPlayersSelectedCards()) return;

        if (IsMatchedCards())
            HandleMatchedCards();
        else
            HandleDismatchedCards();
    }

    private void SetPlayersCards(int id, Card card)
    {
        if (id == 1) cardPlayer1 = card;
        if (id == 2) cardPlayer2 = card;
    }

    private bool IsAllPlayersSelectedCards()
    {
        return cardPlayer1 != null && cardPlayer2 != null;
    }

    private bool IsMatchedCards()
    {
        return cardPlayer1.cardType == cardPlayer2.cardType;
    }

    private void HandleMatchedCards()
    {
        SetMatchedCardTypes();
        UpdatePoints(1);
        HandleCardActions(cardPlayer1, cardPlayer2, Card.CardState.Matched);
    }

    private void SetMatchedCardTypes()
    {
        WindowManager.instance.matchedTypeNumber = (int)cardPlayer1.cardType;
    }

    private void HandleDismatchedCards()
    {
        UpdatePoints(-1);
        HandleCardActions(cardPlayer1, cardPlayer2, Card.CardState.Dismatched);
    }

    void UpdatePoints(int value)
    {
        currentPoints += value;
        scoreText.text = currentPoints + "/13";
    }

    void HandleCardActions(Card card1, Card card2, Card.CardState animation)
    {
        DOVirtual.DelayedCall(0.1f, () =>
        {
            ChangeCardAnimation(card1, card2, animation);
            ChangeCardSprites(card1, card2);
            ClearCards();
        });
    }

    private void ClearCards()
    {
        cardPlayer1 = null;
        cardPlayer2 = null;
    }


    void ChangeCardSprites(Card card1, Card card2)
    {

        if (IsMatchedCards())
        {
            card1.GetComponent<SpriteRenderer>().sprite = card1.actualSprite;
            card2.GetComponent<SpriteRenderer>().sprite = card2.actualSprite;

            DOVirtual.DelayedCall(2f, () => WindowManager.instance.hasMatched = true);
        }
        else
        {
            card1.GetComponent<SpriteRenderer>().sprite = card1.actualSprite;
            card2.GetComponent<SpriteRenderer>().sprite = card2.actualSprite;

            DOVirtual.DelayedCall(5f, () => {
                card1.GetComponent<SpriteRenderer>().sprite = card1.backupOldSprite;
                card2.GetComponent<SpriteRenderer>().sprite = card2.backupOldSprite;
            });

            
            DOVirtual.DelayedCall(0.6f, () =>
            {
                ChangeCardAnimation(card1, card2, Card.CardState.Idle);
            });
        } 
    }

    void ChangeCardAnimation(Card card1, Card card2, Card.CardState animation)
    {
        card1.HandleCardState(animation);
        card2.HandleCardState(animation);
    }
}
using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Torna o script globalmente acessível para outros scripts

    public Card cardPlayer1;
    public Card cardPlayer2;

    [SerializeField] public PointsManager pointsManager;
    [SerializeField] public CompManager timeManager;

    private int scene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        scene = SceneManager.GetActiveScene().buildIndex;
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
        if (scene == 2)
        {
            pointsManager.SetPoints(+(10000f/timeManager.time));
        }
        HandleCardActions(cardPlayer1, cardPlayer2, Card.CardState.Matched);
    }

    private void HandleDismatchedCards()
    {
        HandleCardActions(cardPlayer1, cardPlayer2, Card.CardState.Dismatched);
    }


    private void SetMatchedCardTypes()
    {
        if (scene != 2)
        WindowManager.instance.matchedTypeNumber = (int)cardPlayer1.cardType;
    }

    
    void HandleCardActions(Card card1, Card card2, Card.CardState animation)
    {
        DOVirtual.DelayedCall(0.1f, () =>
        {
            HandleCardAnimations(card1, card2, animation);
            HandleCardSprites(card1, card2);
            HandleWindowCard();
            ClearCards();
        });
    }

    private void ClearCards()
    {
        cardPlayer1 = null;
        cardPlayer2 = null;
    }

    void HandleCardSprites(Card card1, Card card2)
    {
        card1.ChangeSprite(IsMatchedCards());
        card2.ChangeSprite(IsMatchedCards());
    }

    void HandleWindowCard()
    {
        if (IsMatchedCards())
            DOVirtual.DelayedCall(2f, () => WindowManager.instance.hasMatched = true);
    }

    void HandleCardAnimations(Card card1, Card card2, Card.CardState animation)
    {
        card1.ChangeAnimation(animation);
        card2.ChangeAnimation(animation);
    }
}
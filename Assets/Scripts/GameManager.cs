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

    void Update()
    {
        //EndGame();
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
    //   pointsManager.SetPoints(-1);
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
            card1.RevealCard();
            card2.RevealCard();

            DOVirtual.DelayedCall(2f, () => WindowManager.instance.hasMatched = true);
        }
        else
        {
            card1.RevealCard();
            card2.RevealCard();

            DOVirtual.DelayedCall(5f, () => {
                card1.HideCard();
                card2.HideCard();
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

    //void EndGame()
    //{
    //    if (pointsManager.GetPoints() < 0)
    //    {
    //        Debug.Log("Fim de jogo");
    //    }
    //}
}
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
    [SerializeField] public TimeManager timeManager;

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
            pointsManager.AddPoints(timeManager.GetTime());
        }

        cardPlayer1.Match();
        cardPlayer2.Match();

        HandleWindowCard();

        ClearCards();
    }

    private void HandleDismatchedCards()
    {

        cardPlayer1.Dismatch();
        cardPlayer2.Dismatch();

        ClearCards();
    }


    private void SetMatchedCardTypes()
    {
        if (scene != 2)
            WindowManager.instance.matchedTypeNumber = (int)cardPlayer1.cardType;
    }

    private void ClearCards()
    {
        cardPlayer1 = null;
        cardPlayer2 = null;
    }

    void HandleWindowCard()
    {
        if (IsMatchedCards())
            if (scene != 2)
                DOVirtual.DelayedCall(2f, () => WindowManager.instance.hasMatched = true);
    }
}
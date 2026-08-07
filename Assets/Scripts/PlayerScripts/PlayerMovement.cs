using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int playerID;

    [SerializeField] private PlayerControl playerControl;

    private int currentIndex;

    private bool youCanMoveNow = false;
    private bool isSelectionPending = false;

    private int UP = -4;
    private int DOWN = 4;
    private int LEFT = -1;
    private int RIGHT = 1;
    private int scene;

    IPlayerInput input;

    void Start()
    {
        currentIndex = 0;
        scene = SceneManager.GetActiveScene().buildIndex;
        
        ChangeCardScale(1.5f, 1.5f, -1f);
        StartShowing();
    }

    void Update()
    {
        HandlePlayerControls();
    }

    void HandlePlayerControls()
    {
        if (scene != 2)
        {
            if (WindowManager.instance.isWindowActive == false && youCanMoveNow)
            {
                HandlePlayerMovement();
                HandlePlayerSelectionCard();
            }
        }
        else
        {
            if (WindowManagerScore.instance.isWindowActive == false &&youCanMoveNow)
            {
                HandlePlayerMovement();
                HandlePlayerSelectionCard();
            }        
        }    
    }
    
    void HandlePlayerMovement()
    {
        input = VerifyPlayer() ? new WASDInput() : new ArrowInput();

        if (input.Left()) HandleCardMovement(LEFT);
        if (input.Right()) HandleCardMovement(RIGHT);
        if (input.Up()) HandleCardMovement(UP);
        if (input.Down()) HandleCardMovement(DOWN);
    }

    bool VerifyPlayer()
    {
        return playerID == 1;
    }  

    void HandlePlayerSelectionCard()
    {
        if (input.Confirm())
        {
            isSelectionPending = true;
            Card card = playerControl.cards[currentIndex].GetComponent<Card>();
            card.ChangeAnimation(Card.CardState.Selected);

            // Acessa o script do GameManager com o ID do jogador e a carta que foi selecionada por ele
            GameManager.instance.VerifyCardTypes(playerID, playerControl.cards[currentIndex].GetComponent<Card>());
            
            ChangeCardScale(1.3f, 1.3f, 0f);
            
            if (GameManager.instance.finaleActivator != 13)
                HandleCardMovement(RIGHT);

                DOVirtual.DelayedCall(0.2f, () =>
                {
                    isSelectionPending = false;

                    Card currentCard = playerControl.cards[currentIndex].GetComponent<Card>();

                    if (currentCard.cardState != Card.CardState.Matched && currentCard.cardState != Card.CardState.Dismatched)
                    {
                        currentCard.ChangeAnimation(Card.CardState.Idle);
                    }
            else 
                Debug.Log("Acabou");
                return;
            });

            StopMovimentation();
        }
    }

    public void HandleCardMovement(int direction)
    {
        if (!IsMatchedCards())
        {
            ChangeCardScale(1.3f, 1.3f, 0);
            if (!isSelectionPending)
            {
                ChangeCardStateToIdle();
            }
            MoveToNextCard(direction);
        }

        FindNextDismatchedCard(direction);
            
        ChangeCardScale(1.5f, 1.5f, -1f);
    }

    private void MoveToNextCard(int direction)
    {
        int newIndex = currentIndex + direction;

        if (newIndex > playerControl.cards.Count - 1)
        {
            newIndex = 0;
        }
        else if (newIndex < 0)
        {
            newIndex = playerControl.cards.Count - 1;
        }

        currentIndex = newIndex;
    }

    private void ChangeCardScale(float x, float y, float displacement)
    {
        if (playerControl == null || playerControl.cards == null || playerControl.cards.Count == 0 || currentIndex < 0 || currentIndex >= playerControl.cards.Count)
        {
            return;
        }

        Transform cardTransform = playerControl.cards[currentIndex].transform;
        cardTransform.localScale = new Vector3(x, y, 0.0f);
        cardTransform.localPosition = new Vector3(cardTransform.localPosition.x, cardTransform.localPosition.y, displacement);
    }

    public void ChangeCardStateToIdle()
    {
        Card card = playerControl.cards[currentIndex].GetComponent<Card>();
        card.ChangeAnimation(Card.CardState.Idle);
    }

    private void FindNextDismatchedCard(int direction)
    {
        while (IsMatchedCards())
        {
            currentIndex += direction;

            if (currentIndex > playerControl.cards.Count - 1)
            {
                currentIndex = 0;
            }
            else if (currentIndex < 0)
            {
                currentIndex = playerControl.cards.Count - 1;
            }
        }
    }

    public bool IsMatchedCards()
    {
        return playerControl.cards[currentIndex].GetComponent<Card>().cardState == Card.CardState.Matched;
    }

    private void StartShowing()
    {
        float waitTime = scene == 2 ? 6.0f : 11.0f;
        DOVirtual.DelayedCall(waitTime, () => youCanMoveNow = true).SetTarget(this);
    }

    private void StopMovimentation()
    {
        youCanMoveNow = false;
        float waitForStop = scene == 2 ? 1.5f : 3.28f;
        DOVirtual.DelayedCall(waitForStop, () => youCanMoveNow = true).SetTarget(this);
    }

    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
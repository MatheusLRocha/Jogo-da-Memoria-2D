using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScore : MonoBehaviour
{
    [SerializeField] private int playerID;

    [SerializeField] private PlayerControl playerControl;

    private int currentIndex;

    private bool youCanMoveNow = false;

    private int UP = -4;
    private int DOWN = 4;
    private int LEFT = -1;
    private int RIGHT = 1;

    IPlayerInput input;

    void Start()
    {
        currentIndex = 0;


        ChangeCardScale(1.5f, 1.5f, -1); 

        StartCoroutine(StartShowing());
    }

    void Update()
    {
        HandlePlayerControls();
    }

    void HandlePlayerControls()
    {
        if (youCanMoveNow)
        {
            HandlePlayerMovement();
            HandlePlayerSelectionCard();
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
            Card card = playerControl.cards[currentIndex].GetComponent<Card>();
            card.HandleCardState(Card.CardState.Selected);

            // Acessa o script do GameManager com o ID do jogador e a carta que foi selecionada por ele
            GameManager.instance.VerifyCardTypes(playerID, playerControl.cards[currentIndex].GetComponent<Card>());

            ChangeCardScale(1.3f, 1.3f, 0f);
            
            HandleCardMovement(RIGHT);
            StartCoroutine(StopMovimentation());
        }
    }

    public void HandleCardMovement(int direction)
    {
        if (!IsMatchedCards())
        {
            ChangeCardScale(1.3f, 1.3f, 0);
            ChangeCardStateToIdle();
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
        playerControl.cards[currentIndex].GetComponent<Transform>().localScale = new UnityEngine.Vector3(x, y, 0.0f);
        playerControl.cards[currentIndex].GetComponent<Transform>().localPosition = new UnityEngine.Vector3(playerControl.cards[currentIndex].GetComponent<Transform>().localPosition.x, playerControl.cards[currentIndex].GetComponent<Transform>().localPosition.y, displacement);   
    }

    private void ChangeCardStateToIdle()
    {
        Card card = playerControl.cards[currentIndex].GetComponent<Card>();
        card.HandleCardState(Card.CardState.Idle);
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

    private bool IsMatchedCards()
    {
        return playerControl.cards[currentIndex].GetComponent<Card>().cardState == Card.CardState.Matched;
    }

    private System.Collections.IEnumerator StartShowing()
    {
        yield return new WaitForSeconds(10.5f);
        youCanMoveNow = true;
    }

    private System.Collections.IEnumerator StopMovimentation()
    {
        youCanMoveNow = false;
        yield return new WaitForSeconds(5f);
        youCanMoveNow = true;
    }
}
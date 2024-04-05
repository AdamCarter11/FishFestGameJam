using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FirstSelect,
    ResolutionOne,
    SecondSelect,
    ResolutionTwo,
    ResetHand
}
public class gamemanager : MonoBehaviour
{
    [HideInInspector] public GameState currentState;
    [Tooltip("if we want to force a tie after this many rounds")] [SerializeField] int maxRounds = 999;
    [SerializeField] Player playerOne, playerTwo;
    int currRound = 0;

    string playerOneAction = "", playerTwoAction = "";
    bool gameOver = false;

    private void Update()
    {
        if (!gameOver)
        {
            StateLogic();
        }
    }

    private void StateLogic()
    {
        switch (currentState)
        {
            case GameState.FirstSelect:
                if (playerOneAction != "" && playerTwoAction != "")
                {
                    CheckOutCome();
                    // make sure to set exhausted cards
                    playerOne.AddToDisabled(playerOneAction);
                    playerTwo.AddToDisabled(playerTwoAction);
                    playerOneAction = "";
                    playerTwoAction = "";

                    print("resolved FIRST select");

                    currentState = GameState.ResolutionOne;
                }
                break;
            case GameState.ResolutionOne:
                CheckHealth();

                print("resolved FIRST resolution");

                currentState = GameState.SecondSelect;
                break;
            case GameState.SecondSelect:
                if (playerOneAction != "" && playerTwoAction != "")
                {
                    CheckOutCome();
                    // make sure to set exhausted cards
                    playerOne.AddToDisabled(playerOneAction);
                    playerTwo.AddToDisabled(playerTwoAction);
                    playerOneAction = "";
                    playerTwoAction = "";

                    print("resolved SECOND select");

                    currentState = GameState.ResolutionTwo;
                }
                break;
            case GameState.ResolutionTwo:
                CheckHealth();

                print("resolved SECOND resolution");

                currentState = GameState.ResetHand;
                break;
            case GameState.ResetHand:
                playerOne.GenerateOptionsHelper();
                playerTwo.GenerateOptionsHelper();

                print("resolved reset hand");

                currentState = GameState.FirstSelect;
                break;
        }
    }

    public void ResetGame()
    {
        currRound = 0;
        playerOne.ResetPlayer();
        playerTwo.ResetPlayer();
    }

    public void SetAction(string newAction, int whichChar)
    {
        if (whichChar == 0)
            playerOneAction = newAction;
        if (whichChar == 1)
            playerTwoAction = newAction;

        //print("1: " + playerOneAction + " 2: " + playerTwoAction);
    }

    #region State checks
    private void CheckHealth()
    {
        if (playerOne.GetHealth() <= 0 && playerTwo.GetHealth() <= 0)
        {
            // both players lose
            print("TIE");
            gameOver = true;
        }
        else if (playerOne.GetHealth() <= 0)
        {
            // player two wins
            print("player two wins");
            gameOver = true;
        }
        else if (playerTwo.GetHealth() <= 0)
        {
            // player one wins
            print("player one wins");
            gameOver = true;
        }
    }
    private void CheckOutCome()
    {
        if (playerOneAction == "quickAttack" && playerTwoAction == "quickAttack")
        {
            playerOne.ChangeHealth(1);
            playerTwo.ChangeHealth(1);
        }
        if (playerOneAction == "quickAttack" && playerTwoAction == "strongAttack")
        {
            playerOne.ChangeHealth(2);
            playerTwo.ChangeHealth(1);
        }
        if (playerOneAction == "strongAttack" && playerTwoAction == "quickAttack")
        {
            playerOne.ChangeHealth(1);
            playerTwo.ChangeHealth(2);
        }
        if (playerOneAction == "strongAttack" && playerTwoAction == "strongAttack")
        {
            playerOne.ChangeHealth(2);
            playerTwo.ChangeHealth(2);
        }
        if (playerOneAction == "quickAttack" && playerTwoAction == "counter")
        {
            playerOne.ChangeHealth(1);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "counter" && playerTwoAction == "quickAttack")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(1);
        }
        if (playerOneAction == "quickAttack" && playerTwoAction == "parry")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "parry" && playerTwoAction == "quickAttack")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "quickAttack" && playerTwoAction == "maneuver")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(1);
        }
        if (playerOneAction == "maneuver" && playerTwoAction == "quickAttack")
        {
            playerOne.ChangeHealth(1);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "strongAttack" && playerTwoAction == "counter")
        {
            playerOne.ChangeHealth(2);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "strongAttack" && playerTwoAction == "parry")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "strongAttack" && playerTwoAction == "maneuver")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(2);
        }
        if (playerOneAction == "counter" && playerTwoAction == "counter")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "counter" && playerTwoAction == "parry")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "counter" && playerTwoAction == "strongAttack")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(2);
        }
        if (playerOneAction == "counter" && playerTwoAction == "maneuver")
        {
            // STUN PERSON COUNTERING
        }
        if (playerOneAction == "maneuver" && playerTwoAction == "counter")
        {
            // STUN PERSON TWO COUNTERING
            currentState = GameState.ResolutionOne;
        }
        if (playerOneAction == "parry" && playerTwoAction == "parry")
        {
            playerOne.ChangeHealth(0);
            playerTwo.ChangeHealth(0);
        }
        if (playerOneAction == "parry" && playerTwoAction == "maneuver")
        {
            // PERSON TWO CARD DRAW
            playerOne.ChangeHealth(1);
            playerTwo.ChangeHealth(1);
        }
        if (playerOneAction == "parry" && playerTwoAction == "counter")
        {
            // NOTHING
        }
        if (playerOneAction == "parry" && playerTwoAction == "strongAttack")
        {
            // DISABLE PERSON ONE PARRY
        }
        if (playerOneAction == "maneuver" && playerTwoAction == "parry")
        {
            // DISABLE PERSON TWO PARRY
        }
        if (playerOneAction == "maneuver" && playerTwoAction == "maneuver")
        {
            // NOTHING
        }
        if (playerOneAction == "maneuver" && playerTwoAction == "strongAttack")
        {
            playerOne.ChangeHealth(2);
            playerTwo.ChangeHealth(0);
        }
    }
    #endregion
}

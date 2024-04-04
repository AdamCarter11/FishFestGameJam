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

    public void ResetGame()
    {
        currRound = 0;
        playerOne.ResetPlayer();
        playerTwo.ResetPlayer();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FirstSelect,
    WaitPlayers,
    ResolutionOne,
    CheckPlayerDeath,
    SecondSelect,
    TimeLimit,
    ResolutionTwo,
    ResetHand
}
public class gamemanager : MonoBehaviour
{
    [HideInInspector] public GameState currentState;

}

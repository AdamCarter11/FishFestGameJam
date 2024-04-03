using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FirstSelect,
    ResolutionOne,
    CheckPlayerDeath,
    SecondSelect,
    ResolutionTwo,
    ResetHand
}
public class gamemanager : MonoBehaviour
{
    [HideInInspector] public GameState currentState;

}

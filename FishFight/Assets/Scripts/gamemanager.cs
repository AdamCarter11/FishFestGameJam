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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] KeyCode actionOne, actionTwo, actionThree, actionFour;
    // [SerializeField] List<> options; // will hold a list of the SO type

    // last actions used (data type will be the SO)
    // currently selected action (data type will be the SO)

    private gamemanager gmRef;
    private int currHealth;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        gmRef = GameObject.FindGameObjectWithTag("GM").GetComponent<gamemanager>();
        currHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gmRef.currentState)
        {
            case GameState.FirstSelect:
                if (Input.GetKeyDown(actionOne))
                {
                    selected = true;
                    // currently selected action = the option in the first position
                }
                break;
        }
    }


    #region helper functions
    public int GetHealth()
    {
        return currHealth;
    }
    public void ResetPlayer()
    {
        currHealth = health;
        // set last actions used to null
    }
    #endregion
}

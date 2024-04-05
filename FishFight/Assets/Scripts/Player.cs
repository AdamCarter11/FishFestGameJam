using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int whichChar = 0;
    [SerializeField] int health = 5;
    [SerializeField] KeyCode actionOne, actionTwo, actionThree;
    [SerializeField] TextMeshProUGUI optionOneText, optionTwoText, optionThreeText;
    // [SerializeField] List<> options; // will hold a list of the SO type

    // last actions used (data type will be the SO)
    // currently selected action (data type will be the SO)

    private gamemanager gmRef;
    private int currHealth;
    private bool selected = false;

    List<string> actionList = new List<string>() { "quickAttack", "strongAttack", "counter", "parry", "maneuver"};
    List<string> optionsList = new List<string>();

    List<string> tempDisabled = new List<string> ();

    // Start is called before the first frame update
    void Start()
    {
        gmRef = GameObject.FindGameObjectWithTag("GM").GetComponent<gamemanager>();
        currHealth = health;
        GenerateOptionsHelper();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gmRef.currentState)
        {
            case GameState.FirstSelect:
                if (Input.GetKeyDown(actionOne))
                {
                    gmRef.SetAction(optionsList[0], whichChar);
                }
                if (Input.GetKeyDown(actionTwo))
                {
                    gmRef.SetAction(optionsList[1], whichChar);
                }
                if (Input.GetKeyDown(actionThree))
                {
                    gmRef.SetAction(optionsList[2], whichChar);
                }
                break;
            case GameState.SecondSelect:
                if (Input.GetKeyDown(actionOne) && optionsList[0] != tempDisabled[0])
                {
                    gmRef.SetAction(optionsList[0], whichChar);
                }
                if (Input.GetKeyDown(actionTwo) && optionsList[1] != tempDisabled[0])
                {
                    gmRef.SetAction(optionsList[1], whichChar);
                }
                if (Input.GetKeyDown(actionThree) && optionsList[2] != tempDisabled[0])
                {
                    gmRef.SetAction(optionsList[2], whichChar);
                }
                break;
        }
    }

    List<string> GetUniqueRandomActions(int count = -1)
    {
        if(count < 0)
            count = 3;

        List<string> uniqueActions = new List<string>();
        List<string> actionsLeft = new List<string>(actionList);
        if(tempDisabled.Count > 0)
        {
            int x = 0;
            for (int i = 0; i < actionsLeft.Count; ++i)
            {
                if (actionsLeft[i] == tempDisabled[x])
                {
                    actionsLeft.RemoveAt(i);
                    x++;
                    if (x >= tempDisabled.Count)
                        break;
                }
            }
            tempDisabled.Clear();
        }
        List<string> availableActions = new List<string>(actionsLeft);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, availableActions.Count);
            uniqueActions.Add(availableActions[index]);
            availableActions.RemoveAt(index);
        }

        optionOneText.gameObject.SetActive(true);
        optionTwoText.gameObject.SetActive(true);
        optionThreeText.gameObject.SetActive(true);

        optionOneText.text = uniqueActions[0];
        optionTwoText.text = uniqueActions[1];
        optionThreeText.text = uniqueActions[2];

        return uniqueActions;
    }


    #region helper functions
    public int GetHealth()
    {
        return currHealth;
    }
    public void ChangeHealth(int changeVal)
    {
        currHealth -= changeVal;
    }
    public void ResetPlayer()
    {
        currHealth = health;
        tempDisabled.Clear();
        GenerateOptionsHelper();
        // set last actions used to null
    }
    public void AddToDisabled(string disabledString)
    {
        tempDisabled.Add(disabledString);
        if (optionOneText.text == disabledString)
            optionOneText.gameObject.SetActive(false);
        if (optionTwoText.text == disabledString)
            optionTwoText.gameObject.SetActive(false);
        if (optionThreeText.text == disabledString)
            optionThreeText.gameObject.SetActive(false);
    }
    public void GenerateOptionsHelper()
    {
        optionsList = GetUniqueRandomActions();
    }
    #endregion
}

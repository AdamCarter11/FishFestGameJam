using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Brain : MonoBehaviour
{
    public int lives = 3;
    public bool gameOver;

    private float currentTime;
    public float totalTime = 180;
    [SerializeField] Text winText;
    [SerializeField] Text timer;
    [SerializeField] GameObject live1;
    [SerializeField] GameObject live2;
    [SerializeField] GameObject live3;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timer.text = string.Format("{00}:{1:00}", minutes, seconds);
            currentTime -= Time.deltaTime;
        } 
        

        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //load menu scene
            }
            
        }
        if(currentTime == 0)
        {
            gameOver = true;
            winText.text = "Fisherman Wins";
        }
    }

    public void loseLife()
    {
        lives--;
        if (lives == 2)
        {
            live3.SetActive(false);
        }
        if(lives == 1)
        {
            live2.SetActive(false);
        }
        if(lives == 0)
        {
            live1.SetActive(false);
            winText.text = "Fish Wins!";
            gameOver = true;
        }
    }

    public void HunterWin()
    {
        gameOver = true;
        winText.text = "Fisherman Wins";
    }
}

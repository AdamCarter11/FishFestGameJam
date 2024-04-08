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
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject live1;
    [SerializeField] GameObject live2;
    [SerializeField] GameObject live3;
    [SerializeField] GameObject escapeSprite;
    [SerializeField] GameObject escapeSprite2;

    [SerializeField] GameObject rockImg, foodImg, algeaImg;

    public bool rockFilled;
    public bool algeaFilled;
    public bool foodFilled;

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
        //Time.timeScale = 0;
    }

    public void Escape()
    {
        gameOver = true;
        winText.text = "Fish Wins";
        //Time.timeScale = 0;
    }

    public void FillFilter(int drop)
    {
        if (drop == 1)
        {
            rockFilled = true;
            print("rockFilled");
            rockImg.GetComponent<Image>().color = Color.white;
        }
        if (drop == 2)
        {
            algeaFilled = true;
            foodImg.GetComponent<Image>().color = Color.white;
        }
        if (drop == 3)
        {
            foodFilled = true;
            algeaImg.GetComponent<Image>().color = Color.white;
        }
        if (foodFilled && algeaFilled && rockFilled) 
        {
            escapeSprite.SetActive(true);
            escapeSprite2.SetActive(true);
        }
        else
        {
            escapeSprite2.SetActive(false);
            escapeSprite.SetActive(false);
        }
    }
}

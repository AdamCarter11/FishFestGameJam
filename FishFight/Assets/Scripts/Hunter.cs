using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private Vector3 mousePosition;
    public bool onFish;
    [SerializeField] Brain brain;
    [SerializeField] AudioSource click;
    [SerializeField] GameObject baitPrefab;
    [SerializeField] int baitsAmmo = 2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);

        if(Input.GetMouseButtonDown(0) && onFish && brain.gameOver == false) 
        {
            //Win
            print("win");
            brain.HunterWin();
            click.Play();
        }
        if(Input.GetMouseButtonDown(0) && !onFish && brain.gameOver == false)
        {
            //Lose Life
            print("-1");
            brain.loseLife();
            click.Play();

        }
        if(Input.GetMouseButtonDown(1) && brain.gameOver == false && baitsAmmo > 0)
        {
            Instantiate(baitPrefab, new Vector2(mousePosition.x, 5f), Quaternion.identity);
            baitsAmmo--;
        }
    }

    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onFish = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onFish = false;
        }
    }
    

}

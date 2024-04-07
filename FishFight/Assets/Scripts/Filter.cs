using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField] Brain brain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RockDrop"))
        {
            brain.FillFilter(1);
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("AlgeaDrop"))
        {
            brain.FillFilter(2);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("FoodDrop"))
        {
            brain.FillFilter(3);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("AIDrop"))
        {
            Destroy(collision.gameObject);
        }
    }
}

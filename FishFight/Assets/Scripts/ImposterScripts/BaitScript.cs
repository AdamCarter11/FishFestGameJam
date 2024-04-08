using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitScript : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
    private void Update()
    {
        if(transform.position.y <= 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}

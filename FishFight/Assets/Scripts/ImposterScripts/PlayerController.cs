using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed at which player moves
    public float acceleration = 5f; // Acceleration of player
    public float maxSpeed = 10f; // Maximum speed player can reach

    private Rigidbody2D rb;

    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movementInput * moveSpeed;
        Vector2 velocityChange = (targetVelocity - rb.velocity) * acceleration * Time.fixedDeltaTime;

        // Apply the velocity change
        rb.AddForce(velocityChange, ForceMode2D.Impulse);

        // Cap the velocity if it exceeds maximum speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}

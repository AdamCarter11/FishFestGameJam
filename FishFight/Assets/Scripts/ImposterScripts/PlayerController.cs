using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f; // Speed at which player moves
    [SerializeField] float acceleration = 5f; // Acceleration of player
    [SerializeField] float maxSpeed = 10f; // Maximum speed player can reach

    [Header("Pickup vars")]
    [SerializeField] float pickupDistance = 3;
    [Tooltip("time in seconds")] [SerializeField] float pickupTime = 2f;
    [SerializeField] GameObject holdPoint;
    [SerializeField] GameObject rockToSpawn;

    private Rigidbody2D rb;

    private Vector2 movementInput;

    Transform rockInScene;
    Coroutine rockCoroutine = null;
    GameObject spawnedRock;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rockInScene = GameObject.FindGameObjectWithTag("Rock").transform;
    }

    // Update is called once per frame
    void Update()
    {
        InputLogic();

        PickupLogic();

        DropLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    private void DropLogic()
    {
        if(spawnedRock && Input.GetKeyDown(KeyCode.E))
        {
            spawnedRock.transform.parent = null;
            spawnedRock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedRock = null;
        }
    }

    private void InputLogic()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void MoveLogic()
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

    private void PickupLogic()
    {
        if(Vector2.Distance(transform.position, rockInScene.position) <= pickupDistance)
        {
            if (rockCoroutine == null && spawnedRock == null)
            {
                rockCoroutine = StartCoroutine(RockPickUp());
            }
        }
        else
        {
            if(rockCoroutine != null)
            {
                StopCoroutine(rockCoroutine);
                rockCoroutine = null;
            }
        }
    }
    IEnumerator RockPickUp()
    {
        yield return new WaitForSeconds(pickupTime);
        // pickup rock
        spawnedRock = Instantiate(rockToSpawn, holdPoint.transform.position, Quaternion.identity);
        spawnedRock.transform.SetParent(transform);
    }
}

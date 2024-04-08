using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int swapAmmo = 1;
    [SerializeField] TextMeshProUGUI swapAmmoText;
    [SerializeField] float moveSpeed = 5f; // Speed at which player moves
    [SerializeField] float acceleration = 5f; // Acceleration of player
    [SerializeField] float maxSpeed = 10f; // Maximum speed player can reach

    [Header("Pickup vars")]
    [SerializeField] float pickupDistance = 3;
    [Tooltip("time in seconds")] [SerializeField] float pickupTime = 2f;
    [SerializeField] GameObject holdPoint;
    [SerializeField] GameObject rockToSpawn;
    [SerializeField] GameObject algeaToSpawn;
    [SerializeField] GameObject foodToSpawn;

    [SerializeField] Brain brain;

    private Rigidbody2D rb;

    private Vector2 movementInput;

    Transform rockInScene;
    Coroutine rockCoroutine = null;
    GameObject spawnedRock;

    Transform algeaInScene;
    Coroutine algeaCoroutine = null;
    GameObject spawnedAlgea;

    Transform foodInScene;
    Coroutine foodCoroutine = null;
    GameObject spawnedFood;

    [SerializeField] AudioSource pickUp;

    int fishType;
    FishSpawner fishSpawner;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (brain == null)
            brain = GameObject.FindGameObjectWithTag("GM").GetComponent<Brain>();
        rockInScene = GameObject.FindGameObjectWithTag("Rock").transform;
        algeaInScene = GameObject.FindGameObjectWithTag("Algea").transform;
        foodInScene = GameObject.FindGameObjectWithTag("Food").transform;
        if (pickUp == null)
            pickUp = GameObject.FindGameObjectWithTag("Pickup").GetComponent<AudioSource>();

        fishSpawner = GameObject.FindGameObjectWithTag("fishSpawner").GetComponent<FishSpawner>();
        swapAmmoText = GameObject.FindGameObjectWithTag("swapText").GetComponent<TextMeshProUGUI>();
        swapAmmoText.text = "Swaps: " + swapAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        InputLogic();

        PickupLogic();

        DropLogic();

        SwapLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    private void SwapLogic()
    {
        if (Input.GetKeyDown(KeyCode.Q) && swapAmmo > 0)
        {
            fishSpawner.SwapPosition(this.gameObject);
            swapAmmo--;
            swapAmmoText.text = "Swaps: " + swapAmmo;
            if(swapAmmo <= 0)
            {
                swapAmmoText.color = Color.red;
            }
        }
    }

    private void DropLogic()
    {
        if(spawnedRock && Input.GetKeyDown(KeyCode.E))
        {
            spawnedRock.transform.parent = null;
            spawnedRock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedRock.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedRock.GetComponent<Drops>().StartDestroyTime();
            spawnedRock = null;
        }

        if (spawnedAlgea && Input.GetKeyDown(KeyCode.E))
        {
            spawnedAlgea.transform.parent = null;
            spawnedAlgea.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedAlgea.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedAlgea.GetComponent<Drops>().StartDestroyTime();
            spawnedAlgea = null;
        }

        if (spawnedFood && Input.GetKeyDown(KeyCode.E))
        {
            spawnedFood.transform.parent = null;
            spawnedFood.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedFood.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedFood.GetComponent<Drops>().StartDestroyTime();
            spawnedFood = null;
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
            if (rockCoroutine == null && spawnedRock == null && spawnedAlgea == null && spawnedFood == null)
            {
                rockCoroutine = StartCoroutine(RockPickUp());
                pickUp.Play();
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

        if (Vector2.Distance(transform.position, algeaInScene.position) <= pickupDistance)
        {
            if (algeaCoroutine == null && spawnedAlgea == null && spawnedFood == null && spawnedAlgea == null)
            {
                algeaCoroutine = StartCoroutine(AlgeaPickUp());
                pickUp.Play();
            }
        }
        else
        {
            if (algeaCoroutine != null)
            {
                StopCoroutine(algeaCoroutine);
                algeaCoroutine = null;
            }
        }

        if (Vector2.Distance(transform.position, foodInScene.position) <= pickupDistance)
        {
            if (foodCoroutine == null  && spawnedRock == null && spawnedFood == null && spawnedAlgea == null)
            {
                foodCoroutine = StartCoroutine(FoodPickUp());
                pickUp.Play();
            }
        }
        else
        {
            if (foodCoroutine != null)
            {
                StopCoroutine(foodCoroutine);
                foodCoroutine = null;
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

    IEnumerator AlgeaPickUp()
    {
        yield return new WaitForSeconds(pickupTime);
        // pickup rock
        spawnedAlgea = Instantiate(algeaToSpawn, holdPoint.transform.position, Quaternion.identity);
        spawnedAlgea.transform.SetParent(transform);
    }

    IEnumerator FoodPickUp()
    {
        yield return new WaitForSeconds(pickupTime);
        // pickup rock
        spawnedFood = Instantiate(foodToSpawn, holdPoint.transform.position, Quaternion.identity);
        spawnedFood.transform.SetParent(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            brain.Escape();
        }
    }

    public int ReturnFishType()
    {
        return fishType;
    }
    public void SetFishType(int newType)
    {
        fishType = newType;
    }
}

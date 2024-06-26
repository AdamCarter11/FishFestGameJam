using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFish_Script : MonoBehaviour
{
    [SerializeField] int fishType;
    [SerializeField] float pointRange = 2;

    [Header("Pickup vars")]
    [SerializeField] float pickupDistance = 3;
    [Tooltip("time in seconds")][SerializeField] float pickupTime = 2f;
    [SerializeField] GameObject holdPoint;
    [SerializeField] GameObject rockToSpawn;
    [SerializeField] GameObject algeaToSpawn;
    [SerializeField] GameObject foodToSpawn;
    [SerializeField] float dropMin = 3f, dropMax = 6f;

    NavMeshAgent agent;
    bool isMoving = false;
    float waitTime;
    float timer;
    Vector3 lastRandomPoint;

    Transform rockInScene;
    Coroutine rockCoroutine = null;
    GameObject spawnedRock;

    Transform algeaInScene;
    Coroutine algeaCoroutine = null;
    GameObject spawnedAlgea;

    Transform foodInScene;
    Coroutine foodCoroutine = null;
    GameObject spawnedFood;
    GameObject bait;

    [SerializeField] AudioSource pickup;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.identity;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.rotation = Quaternion.identity;
        SetRandomDestination();
        timer = Time.time;
        lastRandomPoint = transform.position;
        rockInScene = GameObject.FindGameObjectWithTag("Rock").transform;
        if (pickup == null)
            pickup = GameObject.FindGameObjectWithTag("Pickup").GetComponent<AudioSource>();
    }
     
    void Update()
    {
        if (isMoving)
        {
            // Check if AI reached the destination
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // Start waiting
                if (Time.time - timer >= waitTime)
                {
                    // Reset timer and set new destination
                    SetRandomDestination();
                    timer = Time.time; // Update the timer with the current time
                }
            }
            else
            {
                if (Time.time - timer >= waitTime * 3f)
                {
                    // Reset timer and set new destination
                    SetRandomDestination();
                    timer = Time.time; // Update the timer with the current time
                }
            }
            Flip();
        }
        bait = GameObject.FindGameObjectWithTag("bait");
        if(bait != null)
        {
            agent.SetDestination(bait.transform.position);
        }
        PickupLogic();
    }

    private void Flip()
    {
        // Determine if moving left or right
        Vector3 currentPosition = transform.position;
        float movementDirection = currentPosition.x - lastRandomPoint.x;

        if (movementDirection > 0)
        {
            // Moving right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movementDirection < 0)
        {
            // Moving left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // Update the last position
        lastRandomPoint = currentPosition;
    }
    
    void SetRandomDestination()
    {
        Vector3 randomPoint;
        // Generate random destination on NavMesh
        //Vector3 randomPoint = RandomNavmeshPoint();

        waitTime = Random.Range(.5f, 2f);

        int totallyRandoPointChance = Random.Range(0, 10);
        if (totallyRandoPointChance < 2)
            randomPoint = RandomNavmeshPoint();
        else if (totallyRandoPointChance >= 2 && totallyRandoPointChance <= 9)
            randomPoint = RandomNavmeshPointWithinRange(lastRandomPoint, pointRange);
        else
        {
            if(rockInScene == null)
                rockInScene = GameObject.FindGameObjectWithTag("Rock").transform;
            randomPoint = rockInScene.position;
            waitTime = Random.Range(pickupTime / 2f, pickupTime * 1.1f);
        }

        //print("new point: " + randomPoint);
        agent.SetDestination(randomPoint);

        // Set random wait time
        

        lastRandomPoint = randomPoint;

        // Start moving
        isMoving = true;
    }

    Vector3 RandomNavmeshPoint()
    {
        float randoX = Random.Range(-8f, 8f);
        float randoY = Random.Range(-2.5f, 2.5f);
        return new Vector3(randoX, randoY, 0);
    }
    Vector3 RandomNavmeshPointWithinRange(Vector3 center, float range)
    {
        // Generate random direction within range
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }

    private void PickupLogic()
    {
        if (rockInScene == null)
            rockInScene = GameObject.FindGameObjectWithTag("Rock").transform;
        if (algeaInScene == null)
            algeaInScene = GameObject.FindGameObjectWithTag("Algea").transform;
        if (foodInScene == null)
            foodInScene = GameObject.FindGameObjectWithTag("Food").transform;
        if (Vector2.Distance(transform.position, rockInScene.position) <= pickupDistance)
        {
            if (rockCoroutine == null && spawnedRock == null)
            {
                rockCoroutine = StartCoroutine(RockPickUp());
                
            }
        }
        else
        {
            if (rockCoroutine != null)
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
            if (foodCoroutine == null && spawnedRock == null && spawnedFood == null && spawnedAlgea == null)
            {
                foodCoroutine = StartCoroutine(FoodPickUp());
                
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
        pickup.Play();
        StartCoroutine(RandomDropTime());
    }

    IEnumerator AlgeaPickUp()
    {
        yield return new WaitForSeconds(pickupTime);
        // pickup rock
        spawnedAlgea = Instantiate(algeaToSpawn, holdPoint.transform.position, Quaternion.identity);
        spawnedAlgea.transform.SetParent(transform);
        pickup.Play();
        StartCoroutine(RandomDropTime());
    }

    IEnumerator FoodPickUp()
    {
        yield return new WaitForSeconds(pickupTime);
        // pickup rock
        spawnedFood = Instantiate(foodToSpawn, holdPoint.transform.position, Quaternion.identity);
        spawnedFood.transform.SetParent(transform);
        pickup.Play();
        StartCoroutine(RandomDropTime());
    }
    IEnumerator RandomDropTime()
    {
        float rando = Random.Range(dropMin, dropMax);
        yield return new WaitForSeconds(rando);
        DropLogic();
    }
    private void DropLogic()
    {
        if (spawnedRock)
        {
            spawnedRock.transform.parent = null;
            spawnedRock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedRock.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedRock.GetComponent<Drops>().StartDestroyTime();
            spawnedRock = null;
        }

        if (spawnedAlgea)
        {
            spawnedAlgea.transform.parent = null;
            spawnedAlgea.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedAlgea.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedAlgea.GetComponent<Drops>().StartDestroyTime();
            spawnedAlgea = null;
        }

        if (spawnedFood)
        {
            spawnedFood.transform.parent = null;
            spawnedFood.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spawnedFood.GetComponent<BoxCollider2D>().isTrigger = false;
            spawnedFood.GetComponent<Drops>().StartDestroyTime();
            spawnedFood = null;
        }
    }
    public int ReturnFishType()
    {
        return fishType;
    }
}

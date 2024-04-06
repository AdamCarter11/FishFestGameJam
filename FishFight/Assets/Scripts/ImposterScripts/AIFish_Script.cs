using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFish_Script : MonoBehaviour
{
    [SerializeField] float pointRange = 2;
    NavMeshAgent agent;
    bool isMoving = false;
    float waitTime;
    float timer;
    Vector3 lastRandomPoint;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetRandomDestination();
        timer = Time.time;
        lastRandomPoint = transform.position;
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
        }
    }

    
    void SetRandomDestination()
    {
        Vector3 randomPoint;
        // Generate random destination on NavMesh
        //Vector3 randomPoint = RandomNavmeshPoint();

        int totallyRandoPointChance = Random.Range(0, 10);
        if(totallyRandoPointChance < 2)
            randomPoint = RandomNavmeshPoint();
        else
            randomPoint = RandomNavmeshPointWithinRange(lastRandomPoint, pointRange);

        print("new point: " + randomPoint);
        agent.SetDestination(randomPoint);

        // Set random wait time
        waitTime = Random.Range(.5f, 2f);

        lastRandomPoint = randomPoint;

        // Start moving
        isMoving = true;
    }

    Vector3 RandomNavmeshPoint()
    {
        float randoX = Random.Range(-9, 9);
        float randoY = Random.Range(-5, 5);
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
}

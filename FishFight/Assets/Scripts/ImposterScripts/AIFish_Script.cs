using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFish_Script : MonoBehaviour
{
    NavMeshAgent agent;
    bool isMoving = false;
    float waitTime;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetRandomDestination();
    }
     
    void Update()
    {
        if (isMoving)
        {
            // Check if AI reached the destination
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // Start waiting
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    // Reset timer and set new destination
                    timer = 0f;
                    SetRandomDestination();
                }
            }
        }
    }

    void SetRandomDestination()
    {
        // Generate random destination on NavMesh
        Vector3 randomPoint = RandomNavmeshPoint();
        print("new point: " + randomPoint);
        agent.SetDestination(randomPoint);

        // Set random wait time
        waitTime = Random.Range(.5f, 2f);

        // Start moving
        isMoving = true;
    }

    Vector3 RandomNavmeshPoint()
    {
        float randoX = Random.Range(-9, 9);
        float randoY = Random.Range(-5, 5);
        return new Vector3(randoX, randoY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> playerPrefabs;

    void Start()
    {
        float randoX = Random.Range(-5, 5);
        float randoY = Random.Range(-3.5f, 3.5f);
        Vector2 spawnPos = new Vector2(randoX, randoY);
        Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Count)], spawnPos, Quaternion.identity);
    }

}

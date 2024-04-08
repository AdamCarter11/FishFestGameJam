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
        int fishType = Random.Range(0, playerPrefabs.Count);
        GameObject tempPlayer = Instantiate(playerPrefabs[fishType], spawnPos, Quaternion.identity);
        tempPlayer.GetComponent<PlayerController>().SetFishType(fishType);
    }

}

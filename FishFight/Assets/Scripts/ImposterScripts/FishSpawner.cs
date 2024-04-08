using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] int fishToSpawn = 10;
    [SerializeField] List<GameObject> fishPrefabs;

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Start()
    {
        SpawnFish();
    }

    private void SpawnFish()
    {
        foreach(GameObject fishPref in fishPrefabs)
        {
            for(int i = 0; i < fishToSpawn; ++i)
            {
                float spawnX = Random.Range(-5, 5);
                float spawnY = Random.Range(-3.5f, 3.5f);
                float spawnZ = 0;
                Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
                GameObject tempFish = Instantiate(fishPref, spawnPos, Quaternion.identity);
                spawnedFish.Add(tempFish);
            }
        }
    }

    public void SwapPosition(GameObject playerFish)
    {
        foreach (GameObject fishPref in spawnedFish)
        {
            if(fishPref.GetComponent<AIFish_Script>().ReturnFishType() == playerFish.GetComponent<PlayerController>().ReturnFishType())
            {
                Vector3 newPos = playerFish.transform.position;
                playerFish.transform.position = fishPref.transform.position;
                fishPref.transform.position = newPos;
            }
        }
        
    }
}

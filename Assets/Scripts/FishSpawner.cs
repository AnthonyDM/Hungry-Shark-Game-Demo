using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish;
    public GameObject bigFish;
    public GameObject badFish;

    private float nextSpawnTime;
    private float spawnTimer;

    void Start()
    {
        SetNextSpawnTime();
    }

    private void OnEnable()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnFishy();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        spawnTimer = 0;
        nextSpawnTime = Random.value / 2;
    }

    void SpawnFishy()
    {
        float spawnType = Random.value;
        Vector3 spawnOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

        if (spawnType <= 0.1f)
        {
            Instantiate(badFish, transform.position + spawnOffset, badFish.transform.rotation);
        }
        else if (spawnType <= 0.2f)
        {
            Instantiate(bigFish, transform.position + spawnOffset, badFish.transform.rotation);
        }
        else
        {
            Instantiate(fish, transform.position + spawnOffset, badFish.transform.rotation);
        }

    }
}

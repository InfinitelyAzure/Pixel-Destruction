using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public List<GameObject> foodPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f;
    public int maxSpawned;

    [Header("Spawn Area")]
    public Transform spawnPoint;
    public float spawnRadius = 2f;


    private List<GameObject> spawned = new List<GameObject>();
    private List<GameObject> spawnPool = new List<GameObject>();

    private void Start()
    {
        RefillPool();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TrySpawn();
        }
    }

    void RefillPool()
    {
        spawnPool = new List<GameObject>(foodPrefab);

        for (int i = 0; i < spawnPool.Count; i++)
        {
            int rand = Random.Range(i, spawnPool.Count);
            (spawnPool[i], spawnPool[rand]) = (spawnPool[rand], spawnPool[i]);
        }
    }

    void TrySpawn()
    {
        //spawned.RemoveAll(obj => obj == null);

        if (spawned.Count >= maxSpawned)
            return;

        if (spawnPool.Count == 0)
            RefillPool();

        GameObject prefab = spawnPool[0];
        spawnPool.RemoveAt(0);

        GameObject obj = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity);

        spawned.Add(obj);
    }

    Vector3 GetSpawnPosition()
    {
        if (spawnPoint == null)
            spawnPoint = transform;

        return spawnPoint.position + (Vector3)Random.insideUnitCircle * spawnRadius;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    public int MaxAmount = 5;
    public GameObject CoinPrefab;
    public Transform SpawnArea;
    private float spawnAreaBoundX, spawnAreaBoundZ;

    private void Awake()
    {
        Mesh planeMesh = SpawnArea.GetComponent<MeshFilter>().mesh;
        spawnAreaBoundX = SpawnArea.transform.localScale.x * planeMesh.bounds.size.x / 2f;
        spawnAreaBoundZ = SpawnArea.transform.localScale.z * planeMesh.bounds.size.z / 2f;
        
        for(int i = 0; i < MaxAmount; i++)
        {
            SpawnCoin();
        }
    }

    public void Start()
    {
        Coin.CoinCollected += SpawnCoin;
    }

    public void SpawnCoin()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(GetRandomSpawnLocation(), out hit, 10.0f, NavMesh.AllAreas))
        {
            Vector3 result = hit.position;
            Instantiate(CoinPrefab, result, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnLocation()
    {
        // Get random spawn location within the given area
        float randomX = Random.Range(-spawnAreaBoundX, spawnAreaBoundX);
        float randomZ = Random.Range(-spawnAreaBoundZ, spawnAreaBoundZ);
        return new Vector3(randomX, 0f, randomZ);
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= SpawnCoin;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int _maxAmount = 7;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _spawnArea;
    private float _spawnAreaBoundX, _spawnAreaBoundZ;

    private void Awake()
    {
        // Get the valid area for coin to spawn
        Mesh planeMesh = _spawnArea.GetComponent<MeshFilter>().mesh;
        _spawnAreaBoundX = _spawnArea.transform.localScale.x * planeMesh.bounds.size.x / 2f;
        _spawnAreaBoundZ = _spawnArea.transform.localScale.z * planeMesh.bounds.size.z / 2f;
        
        // Spawn starting coin
        for(int i = 0; i < _maxAmount; i++)
        {
            SpawnCoin();
        }
    }

    public void Start()
    {
        // Add as observer when coin is collected
        Coin.CoinCollected += SpawnCoin;
    }

    public void SpawnCoin()
    {
        // Spawn coin only in a valid spot where nav mesh considers walkable
        NavMeshHit hit;
        if (NavMesh.SamplePosition(GetRandomSpawnLocation(), out hit, 10.0f, NavMesh.AllAreas))
        {
            Vector3 result = hit.position;
            Instantiate(_coinPrefab, result, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnLocation()
    {
        // Get random spawn location within the given area
        float randomX = Random.Range(-_spawnAreaBoundX, _spawnAreaBoundX);
        float randomZ = Random.Range(-_spawnAreaBoundZ, _spawnAreaBoundZ);
        return new Vector3(randomX, 0f, randomZ);
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= SpawnCoin;
    }
}

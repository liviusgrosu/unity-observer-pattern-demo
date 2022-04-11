using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestRandomPointScript : MonoBehaviour
{
    public Transform SpawnArea;
    public GameObject validSpawnPoint, initialSpawnPoint;
    private float spawnAreaBoundX, spawnAreaBoundZ;

    private void Awake()
    {
        Mesh planeMesh = SpawnArea.GetComponent<MeshFilter>().mesh;
        spawnAreaBoundX = SpawnArea.transform.localScale.x * planeMesh.bounds.size.x / 2f;
        spawnAreaBoundZ = SpawnArea.transform.localScale.z * planeMesh.bounds.size.z / 2f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(GetRandomSpawnLocation(), out hit, 1.0f, NavMesh.AllAreas))
            {
                Vector3 result = hit.position;
                Instantiate(validSpawnPoint, result, Quaternion.identity);
                //Debug.Break();
            }
        }
    }


    private Vector3 GetRandomSpawnLocation()
    {
        // Get random spawn location within the given area
        float randomX = Random.Range(-spawnAreaBoundX, spawnAreaBoundX);
        float randomZ = Random.Range(-spawnAreaBoundZ, spawnAreaBoundZ);
        Vector3 result = new Vector3(randomX, 0f, randomZ);
        Instantiate(initialSpawnPoint, result, Quaternion.identity);
        //Debug.Break();
        return result;
    }
}

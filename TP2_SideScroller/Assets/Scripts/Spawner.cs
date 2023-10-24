using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int numberOfStructToSpawn;
    [SerializeField]
    private List<GameObject> possibleStructToSpawn;
    [SerializeField]
    private List<GameObject> structToSpawn;

    [SerializeField]
    private float spawnDelay;

    void Start()
    {
        CreateSpawnList();
    }

    private void CreateSpawnList()
    {
        for (int i = 0; i < numberOfStructToSpawn; i++)
        {
            structToSpawn.Add(possibleStructToSpawn[Random.Range(0, possibleStructToSpawn.Count)]);
        }
        StartCoroutine(SpawnerCoroutine());
    }

    IEnumerator SpawnerCoroutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        
        for (int i = 0; i < structToSpawn.Count; i++)
        {
            Instantiate(structToSpawn[i], transform.position, structToSpawn[i].transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

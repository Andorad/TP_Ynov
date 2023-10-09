using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int minSpawnNumber, maxSpawnNumber, minSpawnTime, maxSpawnTime;
    [SerializeField]
    private GameObject asteroid;


   private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int numberOfAsteroid = Random.Range(minSpawnNumber, maxSpawnNumber);
        UIManager.instance.nbOfAsteroids += numberOfAsteroid;

        for (int i = 0; i < numberOfAsteroid; i++)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Instantiate(asteroid, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToDestroy;

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private bool needToSpawn;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
        if (needToSpawn)
        {
            SpawnGoldPoint();
        }
    }

    private void SpawnGoldPoint()
    {
        if(Random.Range(0, 10) == 0)
        {
            Instantiate(coin, spawnPoint.position, spawnPoint.rotation, gameObject.transform);
        }
    }

    void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
    }
}

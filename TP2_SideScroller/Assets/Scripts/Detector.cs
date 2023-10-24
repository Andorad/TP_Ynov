using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField]
    private int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Canvas").GetComponent<UIManager>().AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}

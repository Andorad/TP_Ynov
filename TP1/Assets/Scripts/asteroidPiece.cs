using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class asteroidPiece : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform visual;

    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        visual.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        Destroy(gameObject);
    }
}
